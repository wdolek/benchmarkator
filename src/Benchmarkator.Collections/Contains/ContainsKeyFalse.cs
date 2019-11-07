﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.Contains
{
    [BenchmarkCategory(Categories.CoreFX, Categories.Collections, Categories.GenericCollections)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GenericTypeArguments(typeof(int), typeof(int))] // value type
    [GenericTypeArguments(typeof(string), typeof(string))] // reference type
    public class ContainsKeyFalse<TKey, TValue>
    {
        private TKey[] _notFound;
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
            TKey[] values = ValuesGenerator.ArrayOfUniqueValues<TKey>(Size * 2);
            
            _notFound = values
                .Take(Size)
                .ToArray();
            
            _source = values
                .Skip(Size)
                .Take(Size)
                .ToDictionary(item => item, item => (TValue)(object)item);

            // corefx
            _immutableDictionary = System.Collections.Immutable.ImmutableDictionary.CreateRange<TKey, TValue>(_source);
            _immutableSortedDictionary = System.Collections.Immutable.ImmutableSortedDictionary.CreateRange<TKey, TValue>(_source);
            
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