# LINQ performance comparison

This repository contains a performance comparison between LINQ and some other ways in C#.

## How to run

### DotNETSDKCompare

You need to install [.NET SDk 6 to 9](https://dotnet.microsoft.com/download/dotnet) , and [.NET Framework 4.8.1 Developer pack](https://dotnet.microsoft.com/download/dotnet-framework/net481) if on Windows.

(**Note**: To install .NET 9 SDK on Ubuntu 22.04, you need to [use bash install script to install the .NET 9 SDK](https://blog.dangl.me/archive/installing-net-9-alongside-older-versions-on-ubuntu-2204/))

#### Pure LINQ

On the **DotNETSDKCompare** folder, run the `run_purelinq.ps1` PowerShell script. 

It will run all the available (.NET 6~9, and .NET Framework v4.8.1 if on Windows) LINQ [Count()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.count), [All()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.all), [Any()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.any), [First()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.first), [Single()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.single), [Last()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.last) benchmarks written with [BenchmarkDotNet](http://benchmarkdotnet.org/).

On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around 15 minutes to finish these benchmarks.

#### Compare Collection's built-in Count, Length Property with LINQ Any()

On the **DotNETSDKCompare** folder, run the `run_CollectionPropVsLinqAny.ps1` PowerShell script.

It will run all the available (.NET 6~9, and .NET Framework v4.8.1 if on Windows) Check C# CLR built-iin Collections' Count or Length Property vs. LINQ [Any()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.any) (eq. use Collection.Count > 0 compare Linq.Any()) benchmarks written with [BenchmarkDotNet](http://benchmarkdotnet.org/).

The purpose of this benchmark is to prove the [CA1860: Avoid using 'Enumerable.Any()' extension method](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1860) rule.

On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around Almost 5 hours to finish these benchmarks.

#### LINQ OrderBy compare with other sorting methods

On the **DotNETSDKCompare** folder, run the `run_sort.ps1` PowerShell script.

It will run all the available (.NET 6~9, and .NET Framework v4.8.1 if on Windows) [LINQ OrderBy()](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.orderby) and [C# Array.Sort()](https://learn.microsoft.com/dotnet/api/system.array.sort) .NET API sorting methods with some home-made Bubble Sort, Quick Sort algorithms benchmarks written with [BenchmarkDotNet](http://benchmarkdotnet.org/).

On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around 40 minutes to finish this benchmarks.

### LINQvsManualCoding

You need to install [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) to run benchmarks.

#### Prime numbers generation with LINQ

On the **LINQvsManualCoding** folder, run the `run_prime.ps1` PowerShell script.

It will run 5 different implementations, some of them using LINQ and Parallel LINQ, C# Switch expression to produce prime number sequences.

On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around 40 minutes to finish this benchmarks.

#### LINQ Chunk function vs. Manual Coding

On the **LINQvsManualCoding** folder, the `run_chunk.ps1` PowerShell script can benchmark various ways to split a list(`List<T>`) or array(`T[]`) into chunks of sub arrays include the LINQ [`Chunk()`](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.chunk) method that added since .NET 6.

* To run List<T> chunking benchmarks:
    ```powershell
    ./run_chunk.ps1 -AdditionalParameters '--anyCategories List<T>'
    ```
    On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around 45 minutes to finish this benchmarks.

* To run Array chunking benchmarks:
    ```powershell
    ./run_chunk.ps1 -AdditionalParameters '--anyCategories Array'
    ```
    On a Intel Core i9-13900H CPU @ 2.50GHz, 64GB RAM, Windows 11 24h2 system, it will take around 40 minutes to finish this benchmarks.

You can also add  `-NetRuntimes @('net8.0','net9.0')` to run on both .NET 8 and .NET 9.0 CLR runtimes to see performance difference. 