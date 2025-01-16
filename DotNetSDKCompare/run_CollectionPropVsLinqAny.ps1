#Requires -Version 7
param(
    [string]$BenchmarkFilter = 'DotNetSDKCompare.CollectionPropVsLinqAny.*',
    [string[]]$NetRuntimes = @('net8.0', 'net9.0', 'net7.0', 'net6.0'),
    [string]$BuildFramwework = 'net9.0',
    [string]$BenchmarkWindowsNetFramework = 'net481'
)
$ErrorActionPreference = "Stop"
if($IsWindows){
    $Runtimes = @($NetRuntimes; $BenchmarkWindowsNetFramework)
}
else{
    $Runtimes = $NetRuntimes
}
$run_command =  "dotnet run --configuration Release --framework $BuildFramwework -- --runtimes $Runtimes --filter '$BenchmarkFilter'"
Write-Host "Running command:`n$run_command"
Invoke-Expression $run_command
exit