using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Stringator;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[CategoriesColumn]
[MemoryDiagnoser]
public class StringConcat
{
    // do not use `const` -> that will be concatinated at compile time!
    private static readonly string Part1 = "Foo";
    private static readonly string Part2 = "Bar";

    [Benchmark(Baseline = true)]
    public string StrPlus() => Part1 + Part2;

    [Benchmark]
    public string StrConcat() => string.Concat(Part1, Part2);

    [Benchmark]
    public string StrCreate() => string.Create(Part1.Length + Part2.Length, Part1, (span, state) =>
    {
        state.AsSpan().CopyTo(span);
        Part2.AsSpan().CopyTo(span.Slice(state.Length));
    });
}
