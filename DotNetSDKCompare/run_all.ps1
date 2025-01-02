#Requires -Version 7
Set-PSDebug -Trace 1
$ErrorActionPreference = "Stop"
if($IsWindows){
    dotnet run --configuration Release --framework net9.0 -- --runtimes net8.0 net9.0 net7.0 net6.0 net481 --filter *
}
else{
    dotnet run --configuration Release --framework net9.0 -- --runtimes net8.0 net9.0 net7.0 net6.0 --filter *
}
Set-PSDebug -Trace 0