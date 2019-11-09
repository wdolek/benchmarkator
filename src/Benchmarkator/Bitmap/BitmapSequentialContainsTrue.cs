using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Bitmap
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class BitmapSequentialContainsTrue
    {
        [Params(32, 1024)]
        public int Length;

        private System.Collections.BitArray _bitArray;
        private Dictionary<int, bool> _map;
        private HashSet<int> _set;

        [GlobalSetup]
        public void Setup()
        {
            // only `true` values are stored, missing value means bit is not set
            _bitArray = new System.Collections.BitArray(Length, true);

            _map = new Dictionary<int, bool>(
                Enumerable
                    .Range(0, Length)
                    .Select(v => KeyValuePair.Create(v, true)));

            _set = new HashSet<int>(
                Enumerable.Range(0, Length));
        }

        [Benchmark]
        public bool BitArrayContains()
        {
            var contains = false;
            for (var i = 0; i < Length; i++)
            {
                // or: `_bitArray[i]`
                contains |= _bitArray.Get(i);
            }

            return contains;
        }

        [Benchmark]
        public bool DictionaryContains()
        {
            var contains = false;
            for (var i = 0; i < Length; i++)
            {
                // NB! value is `true`, see setup
                // (alternatively: `_map.ContainsKey(i)`)
                contains |= _map[i];
            }

            return contains;
        }

        [Benchmark]
        public bool SetContains()
        {
            var contains = false;
            for (var i = 0; i < Length; i++)
            {
                contains |= _set.Contains(i);
            }

            return contains;
        }
    }
}
