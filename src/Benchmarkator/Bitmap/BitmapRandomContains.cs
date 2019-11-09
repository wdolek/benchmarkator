using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Bitmap
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class BitmapRandomContains
    {
        [Params(32, 1024)]
        public int Length;

        private int[] _idx;

        private System.Collections.BitArray _bitArray;
        private Dictionary<int, bool> _map;
        private HashSet<int> _set;

        [GlobalSetup]
        public void Setup()
        {
            var rand = new Bogus.Randomizer();

            // generate input data, e.g.: [ t, f, f, t, t, t, f ]
            bool[] data = Enumerable
                .Range(0, Length)
                .Select(_ => rand.Bool())
                .ToArray();

            // generate indexes to check, e.g.: [ 5, 2, 0, 6, 9, 1, 8, 3 ]
            // (we don't really care whether bit is set or not; we need same indexes for all benchmarks)
            _idx = Enumerable
                .Range(0, Length)
                .Select(_ => rand.Int(0, Length - 1))
                .ToArray();

            // only `true` values are stored, missing value means bit is not set
            _bitArray = new System.Collections.BitArray(data);

            _map = new Dictionary<int, bool>(
                data.Select((v, i) => KeyValuePair.Create(i, v))
                    .Where(kv => kv.Value));

            _set = new HashSet<int>(
                data.Select((v, i) => (v, i))
                    .Where(t => t.v)
                    .Select(t => t.i));
        }

        [Benchmark]
        public bool BitArrayContains()
        {
            var contains = false;
            foreach (var i in _idx)
            {
                // or: _bitArray[i]
                contains |= _bitArray.Get(i);
            }

            return contains;
        }

        [Benchmark]
        public bool DictionaryContains()
        {
            var contains = false;
            foreach (var i in _idx)
            {
                // we assume that if key-value pair is set, bit is set
                // (alternatively we can check value as well: `_map.TryGetValue(i, out var v) && v`)
                contains |= _map.ContainsKey(i);
            }

            return contains;
        }

        [Benchmark]
        public bool SetContains()
        {
            var contains = false;
            foreach (var i in _idx)
            {
                contains |= _set.Contains(i);
            }

            return contains;
        }
    }
}
