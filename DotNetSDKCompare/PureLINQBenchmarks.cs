using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace DotNetSDKCompare;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
public class PureLINQBenchmarks
{
    private const int ArraySize = 100_000;
    private readonly IEnumerable<int> _array = Enumerable.Range(1, ArraySize).ToArray();

    private readonly Func<int, bool> _countLambda;
    private readonly Func<int, bool> _allLambda;
    private readonly Func<int, bool> _anyLambda;
    private readonly Func<int, bool> _firstLambda;
    private readonly Func<int, bool> _lastLambda;
    private readonly Func<int, bool> _singleLambda;

    public PureLINQBenchmarks()
    {
        const int magicNumber = ArraySize / 2;

        _countLambda = _anyLambda = i => i % magicNumber == 0;
        _allLambda = i => i > magicNumber;
        _firstLambda = i => i % magicNumber > magicNumber / 2;
        _lastLambda = i => i % magicNumber <= magicNumber / 2;
        _singleLambda = i => i == magicNumber;
    }

    [Benchmark(Description = "LINQ Count()")]
    public int Count() => _array.Count(_countLambda);

    [Benchmark(Description = "LINQ Any()")]
    public bool Any() => _array.Any(_anyLambda);

    [Benchmark(Description = "LINQ All()")]
    public bool All() => _array.All(_allLambda);

    [Benchmark(Description = "LINQ First()")]
    public int First() => _array.First(_firstLambda);

    [Benchmark(Description = "LINQ Last()")]
    public int Last() => _array.Last(_lastLambda);

    [Benchmark(Description = "LINQ Single()")]
    public int Single() => _array.Single(_singleLambda);

    [Benchmark(Description = "LINQ Max()")]
    public int Max() => _array.Max();

    [Benchmark(Description = "LINQ Min()")]
    public int Min() => _array.Min();
}