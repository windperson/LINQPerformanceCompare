#Requires -Version 7
param(
    [string]$BenchmarkFilter = 'LINQvsManualCoding.ProducePrimeNumbers.*',
    [string[]]$NetRuntimes = @('net9.0'),
    [string]$BuildFramwework = 'net9.0',
    [string]$AdditionalParameters = ''
)
$ErrorActionPreference = "Stop"
if ($AdditionalParameters -ne '')
{
    $run_command = "dotnet run --configuration Release --framework $BuildFramwework -- --runtimes $NetRuntimes --filter '$BenchmarkFilter' $AdditionalParameters"
}
else
{
    $run_command = "dotnet run --configuration Release --framework $BuildFramwework -- --runtimes $NetRuntimes --filter '$BenchmarkFilter'"
}
Write-Host "Running command:`n$run_command"
Invoke-Expression $run_command
exit