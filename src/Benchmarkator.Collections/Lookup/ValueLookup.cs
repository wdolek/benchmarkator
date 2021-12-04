using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarkator.Collections.Lookup;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[ShortRunJob]
public class ValueLookup
{
    private int _existingIdFirst;
    private int _existingIdLast;
    private int _missingId;

    private ValueClass[] _array = null!;
    private List<ValueClass> _list = null!;
    private Dictionary<int, ValueClass> _dict = null!;

    [Params(4, 16, 64, 128, 512)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        Randomizer.Seed = new Random(123456);

        var valuesBuilder = new Faker<ValueClass>()
            .StrictMode(true)
            .RuleFor(v => v.Id, f => f.Random.Int(min: 1))
            .RuleFor(v => v.Name, f => f.Name.FirstName())
            .RuleFor(v => v.Index, f => f.IndexFaker);

        var source = valuesBuilder.Generate(Size);

        _existingIdFirst = source[0].Id;
        _existingIdLast = source[^1].Id;
        _missingId = 0;

        _array = source.ToArray();
        _list = new List<ValueClass>(source);
        _dict = new Dictionary<int, ValueClass>(
            source.Select(v => new KeyValuePair<int, ValueClass>(v.Id, v)));
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("First")]
    public ValueClass? ArrayLookupFirst()
    {
        var local = _array;
        for (var i = 0; i < local.Length; i++)
        {
            if (local[i].Id == _existingIdFirst)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Last")]
    public ValueClass? ArrayLookupLast()
    {
        var local = _array;
        for (var i = 0; i < local.Length; i++)
        {
            if (local[i].Id == _existingIdLast)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Missing")]
    public ValueClass? ArrayLookupMissing()
    {
        var local = _array;
        for (var i = 0; i < local.Length; i++)
        {
            if (local[i].Id == _missingId)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark]
    [BenchmarkCategory("First")]
    public ValueClass? ListLookupFirst()
    {
        var local = _list;
        for (var i = 0; i < local.Count; i++)
        {
            if (local[i].Id == _existingIdFirst)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark]
    [BenchmarkCategory("Last")]
    public ValueClass? ListLookupLast()
    {
        var local = _list;
        for (var i = 0; i < local.Count; i++)
        {
            if (local[i].Id == _existingIdLast)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark]
    [BenchmarkCategory("Missing")]
    public ValueClass? ListLookupMissing()
    {
        var local = _list;
        for (var i = 0; i < local.Count; i++)
        {
            if (local[i].Id == _missingId)
            {
                return local[i];
            }
        }

        return null;
    }

    [Benchmark]
    [BenchmarkCategory("First")]
    public ValueClass? DictLookupFirst() =>
        _dict.TryGetValue(_existingIdFirst, out var value)
            ? value
            : null;

    [Benchmark]
    [BenchmarkCategory("Last")]
    public ValueClass? DictLookupLast() =>
        _dict.TryGetValue(_existingIdLast, out var value)
            ? value
            : null;

    [Benchmark]
    [BenchmarkCategory("Missing")]
    public ValueClass? DictLookupMissing() =>
        _dict.TryGetValue(_missingId, out var value)
            ? value
            : null;

    public class ValueClass
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int Index { get; init; }
    }
}
