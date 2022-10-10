using System.Collections.Concurrent;
using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Benchmarkator.Collections.Add;

[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class RedundantConcurrentCollection
{
    private int[] _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = ValuesGenerator.Instance.GenerateUniqueValues<int>(128);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("List")]
    public IReadOnlyCollection<int> List()
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
    public IReadOnlyCollection<int> ConcurrentBag()
    {
        var data = _data;
        var bag = new ConcurrentBag<int>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var value in data)
        {
            bag.Add(value);
        }

        return bag;
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Dictionary")]
    public IReadOnlyDictionary<int, int> Dictionary()
    {
        var local = _data;
        var dictionary = new Dictionary<int, int>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in local)
        {
            dictionary.TryAdd(item, item);
        }

        return dictionary;
    }

    [Benchmark]
    [BenchmarkCategory("Dictionary")]
    public IReadOnlyDictionary<int, int> ConcurrentDictionary()
    {
        var local = _data;
        var concurrentDictionary = new ConcurrentDictionary<int, int>();

        foreach (var item in local)
        {
            concurrentDictionary.TryAdd(item, item);
        }

        return concurrentDictionary;
    }
}
