using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Benchmarkator.Collections.Add;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class AddToConcurrentBag
{
    private List<int> _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = new List<int>(ValuesGenerator.Instance.GenerateUniqueValues<int>(128));
    }

    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<int> TryAddAfterInitialization()
    {
        var data = _data;
        var bag = new ConcurrentBag<int>();

        foreach (var value in data)
        {
            bag.Add(value);
        }

        return bag;
    }

    [Benchmark]
    public IReadOnlyCollection<int> TryAddAfterInitializationForEach()
    {
        var data = _data;
        var bag = new ConcurrentBag<int>();

        data.ForEach(bag.Add);

        return bag;
    }

    [Benchmark]
    public IReadOnlyCollection<int> AddOnInitialization()
    {
        var data = _data;
        var bag = new ConcurrentBag<int>(data);

        return bag;
    }
}
