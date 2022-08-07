using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Bogus;
using System;
using System.Runtime.CompilerServices;

namespace Benchmarkator.Stringator;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[CategoriesColumn]
[MemoryDiagnoser]
public class StringFormatSubstring
{
    private string _data = null!;
    private int _length;

    [ParamsAllValues]
    public Size InputSize { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        Randomizer.Seed = new Random(1337);
        var faker = new Faker();

        _data = InputSize switch
        {
            Size.Short => faker.Lorem.Word(),
            Size.Lengthy => faker.Lorem.Paragraphs(3),
            _ => throw new NotImplementedException()
        };

        _length = _data.Length / 2;
    }

    [Benchmark]
    [BenchmarkCategory("Format")]
    public string FormatSubstring()
    {
        var local = _data;
        var length = _length;
        
        return string.Format("In short, {0}", local.Substring(0, length));
    }

    [Benchmark]
    [BenchmarkCategory("Interpolated")]
    public string FormatInterpolatedSubstring()
    {
        var local = _data;
        var length = _length;

        return string.Format($"In short, {local.Substring(0, length)}");
    }

    [Benchmark]
    [BenchmarkCategory("Format")]
    public string FormatRange()
    {
        var local = _data;
        var length = _length;
        
        return string.Format("In short, {0}", local[..length]);
    }

    [Benchmark]
    [BenchmarkCategory("Interpolated")]
    public string FormatInterpolatedRange()
    {
        var local = _data;
        var length = _length;
        
        return string.Format($"In short, {local[..length]}");
    }

    [Benchmark]
    [BenchmarkCategory("Interpolated")]
    public string FormatInterpolatedSpan()
    {
        var local = _data;
        var length = _length;
        
        return string.Format($"In short, {local.AsSpan(0, length)}");
    }

    [Benchmark]
    [BenchmarkCategory("Interpolated")]
    public string InterpolatedStringHandlerAppendFormatted()
    {
        var local = _data;
        var length = _length;

        var builder = new DefaultInterpolatedStringHandler(1, 1);
        builder.AppendLiteral("In short, ");
        builder.AppendFormatted(local.AsSpan(0, length));

        return builder.ToStringAndClear();
    }

    public enum Size
    {
        Short,
        Lengthy
    }
}
