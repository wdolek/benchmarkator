using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using Benchmarkator.Collections;

namespace System.Collections
{
    [BenchmarkCategory(Categories.Collections, Categories.GenericCollections)]
    [GenericTypeArguments(typeof(int))] // value type
    [GenericTypeArguments(typeof(string))] // reference type
    public class CtorDefaultSize<T>
    {
        [Benchmark]
        public ImmutableArray<T> ImmutableArray() => Immutable.ImmutableArray.Create<T>();

        [Benchmark]
        public ImmutableDictionary<T, T> ImmutableDictionary() => Immutable.ImmutableDictionary.Create<T, T>();

        [Benchmark]
        public ImmutableHashSet<T> ImmutableHashSet() => Immutable.ImmutableHashSet.Create<T>();

        [Benchmark]
        public ImmutableList<T> ImmutableList() => Immutable.ImmutableList.Create<T>();

        [Benchmark]
        public ImmutableQueue<T> ImmutableQueue() => Immutable.ImmutableQueue.Create<T>();

        [Benchmark]
        public ImmutableStack<T> ImmutableStack() => Immutable.ImmutableStack.Create<T>();

        [Benchmark]
        public ImmutableSortedDictionary<T, T> ImmutableSortedDictionary() => Immutable.ImmutableSortedDictionary.Create<T, T>();

        [Benchmark]
        public ImmutableSortedSet<T> ImmutableSortedSet() => Immutable.ImmutableSortedSet.Create<T>();

        [Benchmark]
        public LanguageExt.Arr<T> LanguageExtArr() => new LanguageExt.Arr<T>();

        [Benchmark]
        public LanguageExt.HashMap<T, T> LanguageExtHashMap() => new LanguageExt.HashMap<T, T>();

        [Benchmark]
        public LanguageExt.HashSet<T> LanguageExtHashSet() => new LanguageExt.HashSet<T>();

        [Benchmark]
        public LanguageExt.Lst<T> LanguageExtLst() => new LanguageExt.Lst<T>();

        [Benchmark]
        public LanguageExt.Que<T> LanguageExtQue() => new LanguageExt.Que<T>();

        [Benchmark]
        public LanguageExt.Stck<T> LanguageExtStck() => new LanguageExt.Stck<T>();

        [Benchmark]
        public LanguageExt.Map<T, T> LanguageExtMap() => new LanguageExt.Map<T, T>();

        [Benchmark]
        public LanguageExt.Set<T> LanguageExtSet() => new LanguageExt.Set<T>();
    }
}