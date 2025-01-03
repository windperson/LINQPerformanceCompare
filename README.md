# LINQ performance comparison

This repository contains a performance comparison between LINQ and some other ways in C#.

## How to run

### DotNETSDKCompare

You need to install [.NET SDk 6 to 9](https://dotnet.microsoft.com/download/dotnet) , and [.NET Framework 4.8.1 Developer pack](https://dotnet.microsoft.com/download/dotnet-framework/net481) if on Windows.

(**Note**: To install .NET 9 SDK on Ubuntu 22.04, you need to [use bash install script to install the .NET 9 SDK](https://blog.dangl.me/archive/installing-net-9-alongside-older-versions-on-ubuntu-2204/))


On the **DotNETSDKCompare** folder, run the `run_all.ps1` PowerShell script. 

It will run all the available tests (.NET 6~9, and .NET Framework v4.8.1 if on Windows) written with [BenchmarkDotNet](http://benchmarkdotnet.org/) and generate a report.
