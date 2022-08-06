using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.ToCollection;

public class EnumerableToCollection
{
    private IEnumerable<int> _enumerable { get; set; } = null!;

    [Params(10, 100, 1_000, 10_000)]
    public int NumOfItems { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _enumerable = GenerateEnumerable(NumOfItems);
    }

    [Benchmark(Baseline = true)]
    public List<int> EnumerableToList() =>
        _enumerable.ToList();

    [Benchmark]
    public int[] EnumerableToArray() =>
        _enumerable.ToArray();

    private static IEnumerable<int> GenerateEnumerable(int numOfItems)
    {
        for (var i = 0; i < numOfItems; i++)
        {
            yield return i;
        }
    }
}
