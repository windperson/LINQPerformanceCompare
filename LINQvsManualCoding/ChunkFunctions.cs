using System.Collections.ObjectModel;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace LINQvsManualCoding;

[MemoryDiagnoser(displayGenColumns: true)]
[HideColumns("StdDev", "Median", "Job", "Ratio", "RatioSD", "Error", "Alloc Ratio")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByJob, BenchmarkLogicalGroupRule.ByCategory)]
[ReturnValueValidator(failOnError: true)]
public class ChunkFunctions
{
    [Params(500, 1_000, 10_000, 100_000, 1_000_000, Priority = -1)]
    // ReSharper disable UnassignedField.Global
    // ReSharper disable InconsistentNaming
    public int Source_Count;

    [Params(7, 10, 51, 100, 333)] public int Chunk_Size;
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnassignedField.Global

    private Person[] _people = null!;
    private List<Person> _peopleList = null!;

    [GlobalSetup]
    public void PrepareData()
    {
        var faker = new Bogus.Faker();
        _peopleList = new List<Person>(Source_Count);
        for (var i = 0; i < Source_Count; i++)
        {
            _peopleList.Add(new Person
            {
                Name = faker.Name.FullName(),
                Age = faker.Random.Int(1, 100)
            });
        }

        _people = _peopleList.ToArray();
    }

    #region Using LINQ

    [Benchmark(Description = "LINQ Chunk()")]
    [BenchmarkCategory("Array<T>")]
    public int ArrayUseLinqChunk()
    {
        var chunks = _people.Chunk(Chunk_Size);
        return chunks.Count();
    }

    [Benchmark(Description = "LINQ Chunk()")]
    [BenchmarkCategory("List<T>")]
    public int ListUseLinqChunk()
    {
        var chunks = _peopleList.Chunk(Chunk_Size);
        return chunks.Count();
    }

    #endregion

    #region Using Slice()

    [Benchmark(Description = "Memory<T>")]
    [BenchmarkCategory("Array<T>")]
    public int ArrayUseSlice()
    {
        var chunks = _people.AsMemory().Chunk(Chunk_Size);
        return chunks.Count();
    }

    [Benchmark(Description = "ReadOnlyMemory<T>")]
    [BenchmarkCategory("List<T>")]
    public int ListUseSlice()
    {
        var chunks = _peopleList.AsReadOnlyMemory().Chunk(Chunk_Size);
        return chunks.Count();
    }

    [Benchmark(Description = "ReadOnlyMemory<T> Parallel")]
    [BenchmarkCategory("List<T>")]
    public int ListParallelToArrayUseSlice()
    {
        var chunks = _peopleList.AsReadOnly().AsReadOnlyMemory().Chunk(Chunk_Size);
        return chunks.Count();
    }

    #endregion

    #region Manual Coding using Queue

    [Benchmark(Description = "Queue Implement")]
    [BenchmarkCategory("Array")]
    public int ArrayUseCustomChunk()
    {
        var chunks = _people.ChunkUsingQueue(Chunk_Size);
        return chunks.Count();
    }

    [Benchmark(Description = "Queue Implement")]
    [BenchmarkCategory("List")]
    public int ListUseCustomChunk()
    {
        var chunks = _peopleList.ChunkUsingQueue(Chunk_Size);
        return chunks.Count();
    }

    #endregion
}

public class Person
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public string Name { get; set; } = null!;

    public int Age { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}

public static class ReadOnlyMemoryOfTExtension
{
    // Modified version of https://github.com/dotnet/runtime/discussions/87210#discussioncomment-10157536
    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this List<T> list)
    {
        // Note: This may break when the internal implementation of List<T> changes,
        // and also it may crash upon List has been modified after getting the ReadOnlyMemory<T> return object.
        var items =
            (T[])list.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(list)!;

        return new ReadOnlyMemory<T>(items, 0, list.Count);
    }

    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this ReadOnlyCollection<T> source)
    {
        var items = source.AsParallel().ToArray();
        return new ReadOnlyMemory<T>(items);
    }
}

public static class ChunkExtension
{
    public static IEnumerable<T[]> Chunk<T>(this Memory<T> source, int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Chunk size must be greater than zero.", nameof(size));
        }

        for (var i = 0; i < source.Length; i += size)
        {
            yield return source.Span.Slice(i, Math.Min(size, source.Length - i)).ToArray();
        }
    }

    public static IEnumerable<T[]> Chunk<T>(this ReadOnlyMemory<T> source, int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Chunk size must be greater than zero.", nameof(size));
        }

        for (var i = 0; i < source.Length; i += size)
        {
            yield return source.Slice(i, Math.Min(size, source.Length - i)).ToArray();
        }
    }

    public static IEnumerable<T[]> ChunkUsingQueue<T>(this IEnumerable<T> source, int size)
    {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#else
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }
#endif
        if (size <= 0)
        {
            throw new ArgumentException("Chunk size must be greater than zero.", nameof(size));
        }

        var queue = new Queue<T>(source);
        while (queue.Count > 0)
        {
            var chunk = new T[Math.Min(size, queue.Count)];
            for (var i = 0; i < chunk.Length; i++)
            {
                chunk[i] = queue.Dequeue();
            }

            yield return chunk;
        }
    }
}