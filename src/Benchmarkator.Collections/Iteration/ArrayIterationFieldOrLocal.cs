using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Linq;

namespace Benchmarkator.Collections.Iteration
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class ArrayIterationFieldOrLocal
    {
        [Params(4096)]
        public int Length;

        private int[] _data;

        [GlobalSetup]
        public void Setup()
        {
            _data = Enumerable.Range(0, Length).ToArray();
        }

        [Benchmark]
        public int ForLoopOverField()
        {
            var item = 0;
            for (var i = 0; i < _data.Length; i++)
            {
                item = _data[i];
            }

            return item;
        }

        [Benchmark]
        public int ForLoopOverLocal()
        {
            var arr = _data;
            var item = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                item = arr[i];
            }

            return item;
        }
    }
}
