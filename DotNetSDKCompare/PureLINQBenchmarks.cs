using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DotNetSDKCompare;

[MemoryDiagnoser]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Gen0", "Alloc Ratio")]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net48)]
public class PureLINQBenchmarks
{
    private IEnumerable<int> _array = Enumerable.Range(1, 10_000).ToArray();

    [Benchmark]
    public int Count() => _array.Count(i => i > 0);

    [Benchmark]
    public bool All() => _array.All(i => i > 500);

    [Benchmark]
    public bool Any() => _array.Any(i => i == 9_999);

    [Benchmark]
    public int First() => _array.First(i => i > 9_000);

    [Benchmark]
    public int Single() => _array.Single(i => i == 9_999);

    [Benchmark]
    public int Last() => _array.Last(i => i > 0);
}