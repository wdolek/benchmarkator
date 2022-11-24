using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.Lookup;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser]
public class CodebookLookup
{
    private const string BulbzyBrand = "bulbzy";
    private const string LamplyBrand = "lamply";
    private const string MazoloBrand = "mazolo";
    private const string ZizipotBrand = "zizipot";

    private static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        [BulbzyBrand] = "one",
        [LamplyBrand] = "two",
        [MazoloBrand] = "three",
        [ZizipotBrand] = "four",
    };

    public string[] Brands { get; set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        Brands = new[]
        {
            BulbzyBrand,
            LamplyBrand,
            MazoloBrand,
            ZizipotBrand,
            BulbzyBrand.ToUpperInvariant(),
            LamplyBrand.ToUpperInvariant(),
            MazoloBrand.ToUpperInvariant(),
            ZizipotBrand.ToUpperInvariant(),
        };
    }

    [Benchmark]
    public string If()
    {
        string brandNumber = null!;
        foreach (var brand in Brands)
        {
            var lowerBrand = brand.ToLowerInvariant();
            if (lowerBrand == BulbzyBrand)
            {
                brandNumber = "one";
            }
            else if (lowerBrand == LamplyBrand)
            {
                brandNumber = "two";
            }
            else if (lowerBrand == MazoloBrand)
            {
                brandNumber = "three";
            }
            else if (lowerBrand == ZizipotBrand)
            {
                brandNumber = "four";
            }
            else
            {
                ThrowHelper();
            }
        }

        return brandNumber;
    }

    [Benchmark]
    public string IfStringComparison()
    {
        string brandNumber = null!;
        foreach (var brand in Brands)
        {
            if (string.Equals(brand, BulbzyBrand, StringComparison.OrdinalIgnoreCase))
            {
                brandNumber = "one";
            }
            else if (string.Equals(brand, LamplyBrand, StringComparison.OrdinalIgnoreCase))
            {
                brandNumber = "two";
            }
            else if (string.Equals(brand, MazoloBrand, StringComparison.OrdinalIgnoreCase))
            {
                brandNumber = "three";
            }
            else if (string.Equals(brand, ZizipotBrand, StringComparison.OrdinalIgnoreCase))
            {
                brandNumber = "four";
            }
            else
            {
                ThrowHelper();
            }
        }

        return brandNumber;
    }

    [Benchmark]
    public string Switch()
    {
        var arr = Brands;

        string brandNumber = null!;
        foreach (var brand in arr)
        {

            switch (brand.ToLowerInvariant())
            {
                case BulbzyBrand:
                    brandNumber = "one";
                    break;
                case LamplyBrand:
                    brandNumber = "two";
                    break;
                case MazoloBrand:
                    brandNumber = "three";
                    break;
                case ZizipotBrand:
                    brandNumber = "four";
                    break;
                default:
                    ThrowHelper();
                    break;
            }
        }

        return brandNumber;
    }

    [Benchmark]
    public string SwitchExpression()
    {
        var arr = Brands;

        string brandNumber = null!;
        foreach (var brand in arr)
        {
            brandNumber = brand.ToLowerInvariant() switch
            {
                BulbzyBrand => "one",
                LamplyBrand => "two",
                MazoloBrand => "three",
                ZizipotBrand => "four",
                _ => throw new Exception(),
            };
        }

        return brandNumber;
    }

    [Benchmark]
    public string DictionaryLookup()
    {
        var map = Map;
        var arr = Brands;

        string brandNumber = null!;
        foreach (var brand in arr)
        {
            if (!map.TryGetValue(brand, out brandNumber!))
            {
                ThrowHelper();
            }
        }

        return brandNumber;
    }

    [DoesNotReturn]
    private static void ThrowHelper() => throw new Exception();
}
