using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class DivisibleByTwo
    {
        [Params(0, 2, 256, 1024, int.MaxValue, int.MinValue)]
        public int Number;

        [Benchmark]
        public bool Modulo()
        {
            // classic approach ...
            return Number % 2 == 0;
        }

        [Benchmark]
        public bool LogicalAnd()
        {
            // odd number ends with `0` in binary, so if logical `AND` with `1` is `0`, number is odd
            // N & (C - 1)
            return (Number & 1) == 0;
        }
    }
}
