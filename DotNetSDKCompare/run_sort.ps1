#Requires -Version 7
param(
    [string]$BenchmarkFilter = 'DotNetSDKCompare.SortingBenchmarks.*'
)
Set-PSDebug -Trace 1
$ErrorActionPreference = "Stop"
if($IsWindows){
    dotnet run --configuration Release --framework net9.0 -- --job Short --runtimes net8.0 net481 net9.0 net7.0 net6.0 --filter $BenchmarkFilter
}
else{
    dotnet run --configuration Release --framework net9.0 -- --job Short --runtimes net8.0 net9.0 net7.0 net6.0 --filter $BenchmarkFilter
}
Set-PSDebug -Trace 0