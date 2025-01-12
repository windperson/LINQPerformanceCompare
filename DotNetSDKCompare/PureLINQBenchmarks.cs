using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace DotNetSDKCompare;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
public class PureLINQBenchmarks
{
    private const int _arraySize = 100_000;
    private readonly IEnumerable<int> _array = Enumerable.Range(1, _arraySize).ToArray();

    private Func<int, bool> _countLambda = null!;
    private Func<int, bool> _allLambda = null!;
    private Func<int, bool> _anyLambda = null!;
    private Func<int, bool> _firstLambda = null!;
    private Func<int, bool> _lastLambda = null!;
    private Func<int, bool> _singleLambda = null!;

    [GlobalSetup]
    public void SetupLinqConditions()
    {
        var random = new Random();

        var magicNumber = random.Next(1, _arraySize);

        _countLambda = i => i > magicNumber;
        _allLambda = i => i > magicNumber;
        _anyLambda = i => i % magicNumber == 0;
        _firstLambda = i => i % magicNumber > magicNumber / 2;
        _lastLambda = i => i % magicNumber <= magicNumber / 2;
        _singleLambda = i => i == magicNumber;
    }

    [Benchmark(Description = "LINQ Count()")]
    public int Count() => _array.Count(_countLambda);

    [Benchmark(Description = "LINQ All()")]
    public bool All() => _array.All(_allLambda);

    [Benchmark(Description = "LINQ Any()")]
    public bool Any() => _array.Any(_anyLambda);

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