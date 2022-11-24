using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.Add;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class AddToCollection
{
    private int[] _data = null!;

    [Params(4, 64, 128)]
    public int Size { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = ValuesGenerator.Instance.GenerateUniqueValues<int>(Size);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("List")]
    public List<int> AddToList()
    {
        var data = _data;
        var list = new List<int>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            list.Add(value);
        }

        return list;
    }

    [Benchmark]
    [BenchmarkCategory("List")]
    public List<int> AddToListWithCapacity()
    {
        var data = _data;
        var list = new List<int>(data.Length);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            list.Add(value);
        }

        return list;
    }

    [Benchmark]
    [BenchmarkCategory("List")]
    public LinkedList<int> AddToLinkedList()
    {
        var data = _data;
        var list = new LinkedList<int>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            list.AddLast(value);
        }

        return list;
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Dictionary")]
    public Dictionary<int, int> AddToDictionary()
    {
        var data = _data;
        var dict = new Dictionary<int, int>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            dict.Add(value, value);
        }

        return dict;
    }

    [Benchmark]
    [BenchmarkCategory("Dictionary")]
    public Dictionary<int, int> AddToDictionaryWithCapacity()
    {
        var data = _data;
        var dict = new Dictionary<int, int>(data.Length);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            dict.Add(value, value);
        }

        return dict;
    }
}
