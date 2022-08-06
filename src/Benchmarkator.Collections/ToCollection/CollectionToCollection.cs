using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarkator.Collections.ToCollection;

[MemoryDiagnoser]
public class CollectionToCollection
{
    private List<int> _collection = null!;

    [Params(10, 100, 1_000, 10_000)]
    public int NumOfItems { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _collection = new List<int>(GenerateEnumerable(NumOfItems));
    }

    [Benchmark(Baseline = true)]
    public List<int> CollectionToList() =>
        _collection.ToList();

    [Benchmark]
    public int[] CollectionToArray() =>
        _collection.ToArray();

    private static IEnumerable<int> GenerateEnumerable(int numOfItems)
    {
        for (var i = 0; i < numOfItems; i++)
        {
            yield return i;
        }
    }
}
