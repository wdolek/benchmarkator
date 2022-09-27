using System.Collections.Concurrent;
using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Add;

public class RedundantConcurrentDictionary
{
    private int[] _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = ValuesGenerator.Instance.GenerateUniqueValues<int>(128);
    }

    [Benchmark(Baseline = true)]
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
