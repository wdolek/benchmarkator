using System.Net.Http.Headers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Bogus;

namespace Benchmarkator.Http.AuthValueParsator;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser]
public class ParseAuthHeaderValue
{
    private string _headerValue = null!;

    [GlobalSetup]
    public void Setup()
    {
        Randomizer.Seed = new Random(8675309);
        var faker = new Faker();

        _headerValue = $"Bearer {faker.Random.Hash(80)}";
    }

    [Benchmark]
    public AuthenticationHeaderValue Ctor()
    {
        var headerValue = _headerValue;
        var justToken = headerValue.Replace("Bearer ", "");

        return new AuthenticationHeaderValue("Bearer", justToken);
    }

    [Benchmark]
    public AuthenticationHeaderValue TryParse()
    {
        var headerValue = _headerValue;
        _ = AuthenticationHeaderValue.TryParse(headerValue, out var parsedHeaderValue);

        return parsedHeaderValue!;
    }
}
