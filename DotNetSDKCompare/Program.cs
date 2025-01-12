using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace DotNetSDKCompare;

public class Program
{
    public static void Main(string[] args)
    {
        var config = ManualConfig.Create(DefaultConfig.Instance)
        .AddJob(Job.Default.WithPowerPlan(PowerPlan.UserPowerPlan).AsDefault());

        // Use this to select benchmarks from the console:
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }
}