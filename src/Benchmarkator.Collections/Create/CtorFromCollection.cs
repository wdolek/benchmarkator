using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Benchmarkator.Collections;
using BenchmarkDotNet.Attributes;

namespace System.Collections
{
    [BenchmarkCategory(Categories.Collections, Categories.GenericCollections)]
    [GenericTypeArguments(typeof(int))] // value type
    [GenericTypeArguments(typeof(string))] // reference type
    public class CtorFromCollection<T>
    {
        private ICollection<T> _collection;
        private IDictionary<T, T> _dictionary;
        private (T, T)[] _map;

        [Params(256)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _collection = ValuesGenerator.ArrayOfUniqueValues<T>(Size);
            _dictionary = ValuesGenerator.Dictionary<T, T>(Size);
            _map = _dictionary.Select(kvp => (kvp.Key, kvp.Value)).ToArray();
        }

        [Benchmark]
        public ImmutableArray<T> ImmutableArray() => Immutable.ImmutableArray.CreateRange<T>(_collection);

        [Benchmark]
        public ImmutableDictionary<T, T> ImmutableDictionary() => Immutable.ImmutableDictionary.CreateRange<T, T>(_dictionary);

        [Benchmark]
        public ImmutableList<T> ImmutableList() => Immutable.ImmutableList.CreateRange<T>(_collection);

        [Benchmark]
        public ImmutableQueue<T> ImmutableQueue() => Immutable.ImmutableQueue.CreateRange<T>(_collection);

        [Benchmark]
        public ImmutableStack<T> ImmutableStack() => Immutable.ImmutableStack.CreateRange<T>(_collection);

        [Benchmark]
        public ImmutableSortedDictionary<T, T> ImmutableSortedDictionary() => Immutable.ImmutableSortedDictionary.CreateRange<T, T>(_dictionary);

        [Benchmark]
        public ImmutableSortedSet<T> ImmutableSortedSet() => Immutable.ImmutableSortedSet.CreateRange<T>(_collection);

        [Benchmark]
        public LanguageExt.Arr<T> LanguageExtArr() => new LanguageExt.Arr<T>(_collection);

        [Benchmark]
        public LanguageExt.HashMap<T, T> LanguageExtHashMap() => new LanguageExt.HashMap<T, T>(_map);

        [Benchmark]
        public LanguageExt.Lst<T> LanguageExtLst() => new LanguageExt.Lst<T>(_collection);

        [Benchmark]
        public LanguageExt.Que<T> LanguageExtQue() => new LanguageExt.Que<T>(_collection);

        [Benchmark]
        public LanguageExt.Stck<T> LanguageExtStck() => new LanguageExt.Stck<T>(_collection);

        [Benchmark]
        public LanguageExt.Map<T, T> LanguageExtMap() => new LanguageExt.Map<T, T>(_map);

        [Benchmark]
        public LanguageExt.Set<T> LanguageExtSet() => new LanguageExt.Set<T>(_collection);
    }
}