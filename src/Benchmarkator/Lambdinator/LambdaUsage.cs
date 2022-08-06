using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Benchmarkator.Lambdinator;

[CategoriesColumn]
[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class LambdaUsage
{
    private const int Iterations = 1024;

    private IEnumerable<int> _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = Enumerable.Range(0, 32);
    }

    [Benchmark]
    [BenchmarkCategory("Inline")]
    public void InlineLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(i => i);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Inline")]
    public void InlineStaticLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(static i => i);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Static", "Member")]
    public void StaticMemberLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(i => StaticGimmeNumber(i));
        }
    }

    [Benchmark]
    [BenchmarkCategory("Static", "Member")]
    public void StaticMemberGroupLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(StaticGimmeNumber);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Instance", "Member")]
    public void InstanceMemberLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(i => InstanceGimmeNumber(i));
        }
    }

    [Benchmark]
    [BenchmarkCategory("Instance", "Member")]
    public void InstanceMemberGroupLambda()
    {
        for (var i = 0; i < Iterations; i++)
        {
            DoStuff(InstanceGimmeNumber);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int StaticGimmeNumber(int i) => i;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int InstanceGimmeNumber(int i) => i;

    private List<int> DoStuff(Func<int, int> f)
    {
        var local = _data;
        return local.Select(f).ToList();
    }
}
