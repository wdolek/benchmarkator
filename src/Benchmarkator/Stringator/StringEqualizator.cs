using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmarkator.Stringator;

public class StringEqualizator
{
    [ParamsSource(nameof(Values))]
    public (string A, string B) Strings { get; set; }

    [Benchmark(Baseline = true)]
    public bool OpEquality()
    {
        var a = Strings.A;
        var b = Strings.B;
        return a == b;
    }

    [Benchmark]
    public bool Equals()
    {
        var a = Strings.A;
        var b = Strings.B;
        return string.Equals(a, b);
    }

    [Benchmark]
    public bool EqualsOrdinal()
    {
        var a = Strings.A;
        var b = Strings.B;
        return string.Equals(a, b, StringComparison.Ordinal);
    }

    public static IEnumerable<(string A, string B)> Values()
    {
        // 0...9...A...Z...a...z
        const char minChar = '0';
        const char maxChar = 'z';

        Randomizer.Seed = new Random(8675309);
        var faker = new Faker();

        var shortString = faker.Random.String(8, minChar, maxChar);
        var longString = faker.Random.String(128, minChar, maxChar);

        // equal strings
        yield return (shortString, shortString);
        yield return (longString, longString);

        // unequal string (short value should be enough, keep beginning the same)
        var unequalA = shortString + faker.Random.String(8, minChar, maxChar);
        var unequalB = shortString + faker.Random.String(8, minChar, maxChar);
        yield return (unequalA, unequalB);
    }
}
