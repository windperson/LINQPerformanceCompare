#Requires -Version 7
param(
    [string]$BenchmarkFilter = 'DotNetSDKCompare.SortingBenchmarks.*',
    [string[]]$NetRuntimes = @('net8.0', 'net9.0', 'net7.0', 'net6.0')
)
$ErrorActionPreference = "Stop"
if($IsWindows){
    $Runtimes = @($NetRuntimes; 'net481')
}
else{
    $Runtimes = $NetRuntimes
}
$run_command =  "dotnet run --configuration Release --framework net9.0 -- --runtimes $Runtimes --warmupCount 1 --filter $BenchmarkFilter"
Write-Host "Running command:`n$run_command"
Invoke-Expression $run_command
exit