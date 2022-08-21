using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Benchmarkator.Collections.Create;

[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class EmptyCollection
{
    private static readonly IReadOnlyCollection<int> EmptyList = Array.Empty<int>();
    private static readonly IReadOnlyDictionary<int, int> EmptyDictionary = new Dictionary<int, int>();

    [Benchmark(Baseline = true, Description = "Default capacity")]
    [BenchmarkCategory("List")]
    public IReadOnlyCollection<int> AlwaysInstantiateListWithDefaultCapacity() => new List<int>();

    [Benchmark(Description = "Capacity of 0")]
    [BenchmarkCategory("List")]
    public IReadOnlyCollection<int> AlwaysInstantiateListWithCapacity() => new List<int>(0);

    [Benchmark(Description = "Single pre-created instance")]
    [BenchmarkCategory("List")]
    public IReadOnlyCollection<int> ReturnInstantiatedList() => EmptyList;

    [Benchmark(Baseline = true, Description = "Default capacity")]
    [BenchmarkCategory("Dictionary")]
    public IReadOnlyDictionary<int, int> AlwaysInstantiateDictionaryWithDefaultCapacity() => new Dictionary<int, int>();

    [Benchmark(Description = "Capacity of 0")]
    [BenchmarkCategory("Dictionary")]
    public IReadOnlyDictionary<int, int> AlwaysInstantiateDictionaryWithCapacity() => new Dictionary<int, int>(0);

    [Benchmark(Description = "Single pre-created instance")]
    [BenchmarkCategory("Dictionary")]
    public IReadOnlyDictionary<int, int> ReturnInstantiatedDictionary() => EmptyDictionary;
}
