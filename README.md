# LINQ performance comparison

This repository contains a performance comparison between LINQ and some other ways in C#.

## How to run

### DotNETSDKCompare

You need to install [.NET SDk 6 to 9](https://dotnet.microsoft.com/download/dotnet) , and [.NET Framework 4.8.1 Developer pack](https://dotnet.microsoft.com/download/dotnet-framework/net481) if on Windows.

On the **DotNETSDKCompare** folder, run the `run_all.ps1` script. 

It will run all the available tests (.NET 6~9, .NET Framework v4.8.1 if on Windows) written with [BenchmarkDotNet](http://benchmarkdotnet.org/) and generate a report.
