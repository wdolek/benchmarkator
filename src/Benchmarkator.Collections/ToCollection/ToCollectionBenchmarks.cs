using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarkator.Collections.ToCollection;

[MemoryDiagnoser]
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ToCollectionBenchmarks
{
    private IEnumerable<int> _enumerable { get; set; } = null!;
    private List<int> _collection = null!;

    [Params(4, 24, 128, 16_384)]
    public int NumOfItems { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _enumerable = GenerateEnumerable(NumOfItems);
        _collection = new List<int>(GenerateEnumerable(NumOfItems));
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Enumerable")]
    public List<int> EnumerableToList() =>
        _enumerable.ToList();

    [Benchmark]
    [BenchmarkCategory("Enumerable")]
    public int[] EnumerableToArray() =>
        _enumerable.ToArray();

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Collection")]
    public List<int> CollectionToList() =>
        _collection.ToList();

    [Benchmark]
    [BenchmarkCategory("Collection")]
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
