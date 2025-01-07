using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DotNetSDKCompare;

[MemoryDiagnoser]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
public class PureLINQBenchmarks
{
    private readonly IEnumerable<int> _array = Enumerable.Range(1, 10_000).ToArray();

    [Benchmark(Description = "LINQ Count()")]
    public int Count() => _array.Count(i => i > 0);

    [Benchmark(Description = "LINQ All()")]
    public bool All() => _array.All(i => i > 500);

    [Benchmark(Description = "LINQ Any()")]
    public bool Any() => _array.Any(i => i == 9_999);

    [Benchmark(Description = "LINQ First()")]
    public int First() => _array.First(i => i > 9_000);

    [Benchmark(Description = "LINQ Single()")]
    public int Single() => _array.Single(i => i == 9_999);

    [Benchmark(Description = "LINQ Last()")]
    public int Last() => _array.Last(i => i > 0);
}