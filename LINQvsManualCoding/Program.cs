// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var config = ManualConfig.Create(DefaultConfig.Instance)
.AddJob(Job.Default.WithPowerPlan(PowerPlan.UserPowerPlan).AsDefault());

// Use this to select benchmarks from the console:
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);