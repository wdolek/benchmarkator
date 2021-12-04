using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Collections.Immutable;
using System.Linq;

namespace Benchmarkator.Collections.Iteration;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ImmutableArrayIteration
{
    [Params(1, 2, 4, 8)]
    public int Length;

    private ImmutableArray<int> _data = default;

    [GlobalSetup]
    public void Setup()
    {
        _data = ImmutableArray.Create<int>(
            Enumerable
                .Range(0, Length)
                .ToArray());
    }

    [Benchmark(Baseline = true)]
    public int ForLoopFrom0ToN()
    {
        var arr = _data;
        var item = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            item = arr[i];
        }

        return item;
    }

    [Benchmark]
    public int ForLoopFrom0ToNCallMethod()
    {
        var arr = _data;
        return PerformLoop(arr);
    }

    [Benchmark]
    public int ForLoopFrom0ToNWithPrefixInc()
    {
        var arr = _data;
        var item = 0;
        for (var i = 0; i < arr.Length; ++i)
        {
            item = arr[i];
        }

        return item;
    }

    [Benchmark]
    public int ForLoopFromNTo0()
    {
        var arr = _data;
        var item = 0;
        for (var i = arr.Length - 1; i >= 0; i--)
        {
            item = arr[i];
        }

        return item;
    }

    [Benchmark]
    public int ForLoopFromNTo0WithPrefixDec()
    {
        var arr = _data;
        var item = 0;
        for (var i = arr.Length - 1; i >= 0; --i)
        {
            item = arr[i];
        }

        return item;
    }

    private int PerformLoop(ImmutableArray<int> arr)
    {
        var item = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            item = arr[i];
        }

        return item;
    }
}
