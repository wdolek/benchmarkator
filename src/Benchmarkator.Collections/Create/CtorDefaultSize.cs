using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Collections.Create;

[GenericTypeArguments(typeof(int))]
[GenericTypeArguments(typeof(string))]
public class CtorDefaultSize<T>
    where T : notnull
{
    [Benchmark]
    public ImmutableArray<T> ImmutableArray() => System.Collections.Immutable.ImmutableArray.Create<T>();

    [Benchmark]
    public ImmutableDictionary<T, T> ImmutableDictionary() => System.Collections.Immutable.ImmutableDictionary.Create<T, T>();

    [Benchmark]
    public ImmutableHashSet<T> ImmutableHashSet() => System.Collections.Immutable.ImmutableHashSet.Create<T>();

    [Benchmark]
    public ImmutableList<T> ImmutableList() => System.Collections.Immutable.ImmutableList.Create<T>();

    [Benchmark]
    public ImmutableQueue<T> ImmutableQueue() => System.Collections.Immutable.ImmutableQueue.Create<T>();

    [Benchmark]
    public ImmutableStack<T> ImmutableStack() => System.Collections.Immutable.ImmutableStack.Create<T>();

    [Benchmark]
    public ImmutableSortedDictionary<T, T> ImmutableSortedDictionary() => System.Collections.Immutable.ImmutableSortedDictionary.Create<T, T>();

    [Benchmark]
    public ImmutableSortedSet<T> ImmutableSortedSet() => System.Collections.Immutable.ImmutableSortedSet.Create<T>();

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
