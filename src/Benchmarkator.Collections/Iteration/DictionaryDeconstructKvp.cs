using System.Collections.Generic;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Iteration;

public class DictionaryDeconstructKvp
{
    private Dictionary<int, int> _data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = ValuesGenerator.Instance.GenerateDictionary<int, int>(128);
    }

    [Benchmark(Baseline = true)]
    public int SimpleIteration()
    {
        var local = _data;
        var result = 0;

        foreach (var kvp in local)
        {
            result = kvp.Key + kvp.Value;
        }

        return result;
    }

    [Benchmark]
    public int DeconstructIteration()
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
