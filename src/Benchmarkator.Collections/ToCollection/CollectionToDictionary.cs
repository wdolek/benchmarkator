using System.Collections.Generic;
using System.Linq;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.ToCollection;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class CollectionToDictionary
{
    private List<int> _collection = null!;

    [Params(10, 100, 1_000, 10_000)]
    public int NumOfItems { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _collection = new List<int>(ValuesGenerator.Instance.GenerateUniqueValues<int>(NumOfItems));
    }

    [Benchmark(Baseline = true)]
    public Dictionary<int, int> ToDictionarySimple()
    {
        var local = _collection;

        var dict = new Dictionary<int, int>(local.Count);
        for (var i = 0; i < local.Count; i++)
        {
            var value = local[i];
            dict.Add(value, value + 1);
        }

        return dict;
    }

    [Benchmark]
    public Dictionary<int, int> ToDictionaryLinq()
    {
        var local = _collection;
        return local.ToDictionary(i => i, i => i + 1);
    }

    [Benchmark]
    public Dictionary<int, int> ToAggregateLinq()
    {
        var local = _collection;
        return local.Aggregate(
            new Dictionary<int, int>(),
            (dict, i) =>
            {
                dict.TryAdd(i, i + 1);
                return dict;
            });
    }
}
