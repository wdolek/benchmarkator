using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Create;

public class EmptyCollection
{
    private static readonly IReadOnlyCollection<int> Empty = Array.Empty<int>();

    [Benchmark(Baseline = true, Description = "Default capacity")]
    public IReadOnlyCollection<int> AlwaysInstantiateWithDefaultCapacity() => new List<int>();

    [Benchmark(Description = "Capacity of 0")]
    public IReadOnlyCollection<int> AlwaysInstantiateWithCapacity() => new List<int>(0);

    [Benchmark(Description = "Single pre-created instance")]
    public IReadOnlyCollection<int> ReturnInstantiated() => Empty;
}
