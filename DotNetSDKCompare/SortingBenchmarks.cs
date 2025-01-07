using BenchmarkDotNet.Attributes;

namespace DotNetSDKCompare;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("StdDev", "Median", "Job", "RatioSD", "Error", "Alloc Ratio")]
[ExecutionValidator(failOnError: true)]
public class SortingBenchmarks
{
    // Input data for sorting algorithms
    [Params(100, 1_000, 10_000, 100_000, 1000_000)] // Defines the sizes of the arrays to be benchmarked
    // ReSharper disable once UnassignedField.Global
    public int ArraySize;

    private int[] _unsortedArray = [];
    private int[] _sortedArray = [];

    // Setup method runs before each benchmark method to initialize data
    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();
        _sortedArray = Enumerable.Range(0, ArraySize).ToArray();
        _unsortedArray = _sortedArray
            .Select(_ => random.Next(0, ArraySize-1))
            .ToArray();
    }

    // Bubble Sort algorithm benchmark
    [Benchmark(Description = "Bubble Sort")]
    public int[] Self_BubbleSort()
    {
        if (ArraySize >= 100_000)
        {
            throw new Exception(
                "Array size over 100_000 would be extremely slow on benchmark warmup for Bubble Sort. So skipping...");
        }

        var array = (int[])_unsortedArray.Clone(); // Clone to ensure a fresh copy
        for (var i = 0; i < array.Length - 1; i++)
        {
            for (var j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }

        return array;
    }

    // Quick Sort algorithm benchmark
    [Benchmark(Description = "Quick Sort")]
    public int[] Self_QuickSort()
    {
        var array = (int[])_unsortedArray.Clone(); // Clone to ensure a fresh copy
        QuickSortMethod(array, 0, array.Length - 1);
        return array;
    }

    #region Quick Sort Implementation

    // Helper method for Quick Sort
    private void QuickSortMethod(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(array, low, high);
            QuickSortMethod(array, low, pivot - 1);
            QuickSortMethod(array, pivot + 1, high);
        }
    }

    // Helper method to partition the array for Quick Sort
    private static int Partition(int[] array, int low, int high)
    {
        var pivot = array[high];
        var i = low - 1;
        for (var j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }

    #endregion

    // LINQ OrderBy benchmark
    [Benchmark(Description = "LINQ OrderBy")]
    public int[] LINQ_OrderBy()
    {
        var array = (int[])_unsortedArray.Clone(); // Clone to ensure a fresh copy
        return array.OrderBy(i => i).ToArray();
    }

    // C# Array.Sort() benchmark
    [Benchmark(Description = "Array.Sort()")]
    public int[] Array_Sort()
    {
        var array = (int[])_unsortedArray.Clone(); // Clone to ensure a fresh copy
        Array.Sort(array);
        return array;
    }
}