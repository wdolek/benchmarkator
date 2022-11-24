using System;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Stringator;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser]
public class CultureStringSlicator
{
    private static readonly Regex CulturePattern = new(@".+-(\w{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    [Params("to-to", "to-TO", "TO-TO")]
    public string Culture { get; set; } = null!;

    [Benchmark]
    public string NaiveSplit()
    {
        var localCulture = Culture;
        var country = localCulture.Split('-').Last();

        return $"country={country.ToUpperInvariant()}";
    }

    [Benchmark]
    public string Substring()
    {
        var localCulture = Culture;
        var country = localCulture.Substring(Culture.LastIndexOf('-') + 1);

        return $"country={country.ToUpperInvariant()}";
    }

    [Benchmark]
    public string SpanSplit()
    {
        var localCulture = Culture;
        var countrySlice = localCulture.AsSpan().Slice(Culture.LastIndexOf('-') + 1);

        Span<char> countryCode = stackalloc char[countrySlice.Length];
        countrySlice.ToUpperInvariant(countryCode);

        return $"country={countryCode}";
    }

    [Benchmark]
    public string Regex()
    {
        var localCulture = Culture;
        var match = CulturePattern.Match(localCulture);

        return $"country={match.Groups[1].Value.ToUpperInvariant()}";
    }
}
