﻿#Requires -Version 7
param(
    [string]$BenchmarkFilter = 'LINQvsManualCoding.ProducePrimeNumbers.*',
    [string]$BuildFramwework = 'net9.0',
    [string]$AdditionalParameters = ''
)
$ErrorActionPreference = "Stop"
if ($AdditionalParameters -ne '') {
    $run_command = "dotnet run --configuration Release --framework $BuildFramwework -- --filter '$BenchmarkFilter' $AdditionalParameters"
}
else {
    $run_command = "dotnet run --configuration Release --framework $BuildFramwework -- --filter '$BenchmarkFilter'"

}
Write-Host "Running command:`n$run_command"
Invoke-Expression $run_command
exit