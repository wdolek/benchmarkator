using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Benchmarkator.Collections;
using BenchmarkDotNet.Attributes;

namespace System.Collections
{
    [BenchmarkCategory(Categories.Collections, Categories.GenericCollections)]
    [GenericTypeArguments(typeof(int), typeof(int))] // value type
    [GenericTypeArguments(typeof(string), typeof(string))] // reference type
    public class ContainsKeyTrue<TKey, TValue>
    {
        private TKey[] _found;
        private Dictionary<TKey, TValue> _source;

        private ImmutableDictionary<TKey, TValue> _immutableDictionary;
        private ImmutableSortedDictionary<TKey, TValue> _immutableSortedDictionary;
        private LanguageExt.HashMap<TKey, TValue> _langExtHashMap;
        private LanguageExt.Map<TKey, TValue> _langExtMap;

        [Params(512)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _found = ValuesGenerator.ArrayOfUniqueValues<TKey>(Size);
            _source = _found.ToDictionary(item => item, item => (TValue)(object)item);

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
            var found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.ContainsKey(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableSortedDictionary()
        {
            bool result = default;
            var collection = _immutableSortedDictionary;
            var found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.ContainsKey(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtHashMap()
        {
            bool result = default;
            var collection = _langExtHashMap;
            var found = _found;

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
            var found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.ContainsKey(found[i]);
            }

            return result;
        }
    }
}