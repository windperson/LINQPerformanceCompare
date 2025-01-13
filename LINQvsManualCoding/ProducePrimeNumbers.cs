using System.ComponentModel;
using BenchmarkDotNet.Attributes;

namespace LINQvsManualCoding;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
[ExecutionValidator(failOnError: true)]
public class ProducePrimeNumbers
{
    // Input data for sorting algorithms
    [Params(100, 1_000, 10_000, 100_000, 1000_000)] // Defines the sizes of the arrays to be benchmarked
    // ReSharper disable once UnassignedField.Global
    public int UpperLimit;

    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number == 2)
        {
            return true;
        }

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    [Benchmark(Description = "LINQ Where()")]
    public int[] UseLinq()
    {
        return Enumerable.Range(1, UpperLimit).Where(IsPrime).ToArray();
    }

    [Benchmark(Description = "Parallel LINQ")]
    public int[] UseLinqWithParallel()
    {
        return Enumerable.Range(1, UpperLimit).AsParallel().Where(IsPrime).ToArray();
    }

    [Benchmark(Description = "Use for loop")]
    public int[] UseManualCoding()
    {
        var primeNumbers = new List<int>();
        for (int i = 1; i <= UpperLimit; i++)
        {
            if (IsPrime(i))
            {
                primeNumbers.Add(i);
            }
        }

        return primeNumbers.ToArray();
    }

    [Benchmark(Description = "C# switch expression")]
    public int[] UseSwitchExpression()
    {
        return Enumerable.Range(1, UpperLimit).Where(n => n switch
        {
            1 => false,
            2 => true,
            var c when c % 2 == 0 => false,
            var c when Enumerable.Range(3, n / 2).Where((n, idx) => idx % 2 == 0).Any(c => n != c && n % c == 0) => false,
            _ => true
        }).ToArray();
    }
}
