using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Iteration;

public class CollectionIterationIndexerAccess
{
    private int[] _array = null!;
    private List<int> _list = null!;

    [GlobalSetup]
    public void Setup()
    {
        var value = ValuesGenerator.Instance.GenerateUniqueValues<int>(256);

        _array = value;
        _list = new List<int>(value);
    }

    [Benchmark(Baseline = true, Description = "Access array indexer")]
    public int IterateAndAccessArray()
    {
        var local = _array;
        var last = 0;

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < local.Length; i++)
        {
            last = local[i];
        }

        return last;
    }

    [Benchmark(Description = "Access List<> indexer")]
    public int IterateAndAccessList()
    {
        var local = _list;
        var last = 0;

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < local.Count; i++)
        {
            last = local[i];
        }

        return last;
    }
}
