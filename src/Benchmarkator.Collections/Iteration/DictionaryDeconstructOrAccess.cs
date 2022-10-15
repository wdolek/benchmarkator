using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Iteration;

public class DictionaryDeconstructOrAccess
{
    private Dictionary<int, int> _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = ValuesGenerator.Instance.GenerateDictionary<int, int>(128);
    }

    [Benchmark(Baseline = true)]
    public int Index()
    {
        var local = _data;
        var result = 0;

        foreach (var key in local.Keys)
        {
            result = key + local[key];
        }

        return result;
    }

    [Benchmark]
    public int Deconstruct()
    {
        var local = _data;
        var result = 0;

        foreach (var (key, value) in local)
        {
            result = key + value;
        }

        return result;
    }
}
