using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Stringator;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class StringBuilderAppendator
{
    private const string Key = "le_key";
    private const string Value = "le_value";

    private StringBuilder _sb = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _sb = new StringBuilder(Key.Length + Value.Length + 2);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        _sb.Clear();
    }

    [Benchmark(Baseline = true)]
    public StringBuilder AppendInterpolated()
    {
        var k = Key;
        var v = Value;
        var sb = _sb;

        sb.Append($"&{k}={v}");

        return sb;
    }

    [Benchmark]
    public StringBuilder AppendFormat()
    {
        var k = Key;
        var v = Value;
        var sb = _sb;

        sb.AppendFormat("&{0}={1}", k, v);

        return sb;
    }

    [Benchmark]
    public StringBuilder Append()
    {
        var k = Key;
        var v = Value;
        var sb = _sb;

        sb.Append('&');
        sb.Append(k);
        sb.Append('=');
        sb.Append(v);

        return sb;
    }
}
