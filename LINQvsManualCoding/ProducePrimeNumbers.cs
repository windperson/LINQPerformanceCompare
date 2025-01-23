using System.ComponentModel;
using BenchmarkDotNet.Attributes;

namespace LINQvsManualCoding;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
[ExecutionValidator(failOnError: true)]
[ReturnValueValidator]
public class ProducePrimeNumbers
{
    // Defines the upper limit of generated prime numbers
    [Params(100, 1_000, 10_000, 100_000, 1000_000)]
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

        if (number % 2 == 0)
        {
            return false;
        }

        for (var i = 3; i + i <= number; i += 2)
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
    public int[] UseParallelLinq()
    {
        return Enumerable.Range(1, UpperLimit).AsParallel().Where(IsPrime).ToArray();
    }

    [Benchmark(Description = "Use for loop")]
    public int[] UseManualCoding()
    {
        var primeNumbers = new List<int>();
        for (var i = 1; i <= UpperLimit; i++)
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
            _ when n % 2 == 0 => false,
            _ when Enumerable.Range(3, n / 2).Where((_, idx) => idx % 2 == 0).Any(c => n != c && n % c == 0) => false,
            _ => true
        }).ToArray();
    }
}