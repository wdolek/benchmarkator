using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System;
using System.Runtime.CompilerServices;

namespace Benchmarkator.Lambdinator;

[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class LambdaUsage
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Inline")]
    public void InlineLambda()
    {
        DoStuff(i => i);
    }

    [Benchmark]
    [BenchmarkCategory("Inline")]
    public void InlineStaticLambda()
    {
        DoStuff(static i => i);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Member")]
    public void InstanceMemberLambda()
    {
        DoStuff(i => InstanceGimmeNumber(i));
    }

    [Benchmark]
    [BenchmarkCategory("Member")]
    public void InstanceMethodGroup()
    {
        DoStuff(InstanceGimmeNumber);
    }

    [Benchmark]
    [BenchmarkCategory("Member")]
    public void StaticMemberLambda()
    {
        DoStuff(i => StaticGimmeNumber(i));
    }

    [Benchmark]
    [BenchmarkCategory("Member")]
    public void StaticMethodGroup()
    {
        DoStuff(StaticGimmeNumber);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int StaticGimmeNumber(int i) => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int InstanceGimmeNumber(int i) => 0;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void DoStuff(Func<int, int> f)
    {
        // noop
    }
}
