using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Iteration;

public class EnumerableIteration
{
    private const int NumOfItems = 10_000;

    private IEnumerable<int> _yieldEnumerable = null!;
    private IEnumerable<int> _rangeEnumerable = null!;
    private int[] _arrayEnumerable = null!;

    [GlobalSetup]
    public void Setup()
    {
        _yieldEnumerable = GenerateEnumerable(NumOfItems);
        _rangeEnumerable = Enumerable.Range(1, NumOfItems);
        _arrayEnumerable = GenerateEnumerable(NumOfItems).ToArray();
    }

    [Benchmark]
    public void YieldEnumerable()
    {
        var @enum = _yieldEnumerable;
        foreach (var _ in @enum)
        {
            // do nothing
        }
    }

    [Benchmark]
    public void RangeEnumerable()
    {
        var @enum = _rangeEnumerable;
        foreach (var _ in @enum)
        {
            // do nothing
        }
    }

    [Benchmark]
    public void ArrayEnumerable()
    {
        var @enum = _arrayEnumerable;
        foreach (var _ in @enum)
        {
            // do nothing
        }
    }

    private static IEnumerable<int> GenerateEnumerable(int numOfItems)
    {
        for (var i = 0; i < numOfItems; i++)
        {
            yield return i;
        }
    }
}
