using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace System.Collections
{
    [GenericTypeArguments(typeof(int), typeof(int))]
    [GenericTypeArguments(typeof(string), typeof(string))]
    public class ImmutableContainsKeyFalse<TKey, TValue>
        where TKey : notnull
    {
        private TKey[] _notFound = null!;
        private Dictionary<TKey, TValue> _source = null!;

        private ImmutableDictionary<TKey, TValue> _immutableDictionary = null!;
        private ImmutableSortedDictionary<TKey, TValue> _immutableSortedDictionary = null!;
        private LanguageExt.HashMap<TKey, TValue> _langExtHashMap;
        private LanguageExt.Map<TKey, TValue> _langExtMap;

        [Params(512, 8192)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            TKey[] values = ValuesGenerator.Instance.GenerateUniqueValues<TKey>(Size * 2);

            _notFound = values
                .Take(Size)
                .ToArray();

            _source = values
                .Skip(Size)
                .Take(Size)
                .ToDictionary(item => item, item => (TValue)(object)item);

            // corefx
            _immutableDictionary = Immutable.ImmutableDictionary.CreateRange<TKey, TValue>(_source);
            _immutableSortedDictionary = Immutable.ImmutableSortedDictionary.CreateRange<TKey, TValue>(_source);

            // LanguageExt.Core
            _langExtHashMap = new LanguageExt.HashMap<TKey, TValue>().AddRange(_source);
            _langExtMap = new LanguageExt.Map<TKey, TValue>().AddRange(_source);
        }

        [Benchmark]
        public bool ImmutableDictionary()
        {
            bool result = default;
            var collection = _immutableDictionary;
            var notFound = _notFound;

            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.ContainsKey(notFound[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableSortedDictionary()
        {
            bool result = default;
            var collection = _immutableSortedDictionary;
            var notFound = _notFound;

            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.ContainsKey(notFound[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtHashMap()
        {
            bool result = default;
            var collection = _langExtHashMap;
            var found = _notFound;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.ContainsKey(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtMap()
        {
            bool result = default;
            var collection = _langExtMap;
            var found = _notFound;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.ContainsKey(found[i]);
            }

            return result;
        }
    }
}