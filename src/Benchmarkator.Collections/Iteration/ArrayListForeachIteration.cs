using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.Iteration;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ArrayListForeachIteration
{
    private int[] _array = null!;
    private List<int> _list = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var data = ValuesGenerator.Instance.GenerateUniqueValues<int>(256);

        _array = data;
        _list = new List<int>(data);
    }

    [Benchmark(Baseline = true)]
    public int Array()
    {
        var local = _array;
        var value = 0;

        foreach (var v in local)
        {
            value = v;
        }

        return value;
    }

    [Benchmark]
    public int ArrayAsEnumerable()
    {
        IEnumerable<int> local = _array;
        var value = 0;

        foreach (var v in local)
        {
            value = v;
        }

        return value;
    }

    [Benchmark]
    public int List()
    {
        var local = _list;
        var value = 0;

        foreach (var v in local)
        {
            value = v;
        }

        return value;
    }
}
