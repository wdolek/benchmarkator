using System.Collections.Immutable;
using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;

namespace System.Collections
{
    [GenericTypeArguments(typeof(int))] // value type
    [GenericTypeArguments(typeof(string))] // reference type
    public class ImmutableContainsTrue<T>
        where T : IEquatable<T>
    {
        private T[] _found;

        private ImmutableArray<T> _immutableArray;
        private ImmutableHashSet<T> _immutableHashSet;
        private ImmutableList<T> _immutableList;
        private ImmutableSortedSet<T> _immutableSortedSet;
        private LanguageExt.Arr<T> _langExtImmutableArray;
        private LanguageExt.HashSet<T> _langExtImmutableHashSet;
        private LanguageExt.Lst<T> _langExtImmutableList;
        private LanguageExt.Set<T> _langExtImmutableSet;

        [Params(512)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _found = ValuesGenerator.Instance.GenerateUniqueValues<T>(Size);

            // corefx
            _immutableArray = Immutable.ImmutableArray.CreateRange<T>(_found);
            _immutableHashSet = Immutable.ImmutableHashSet.CreateRange<T>(_found);
            _immutableList = Immutable.ImmutableList.CreateRange<T>(_found);
            _immutableSortedSet = Immutable.ImmutableSortedSet.CreateRange<T>(_found);

            // LanguageExt.Core
            _langExtImmutableArray = new LanguageExt.Arr<T>().AddRange(_found);
            _langExtImmutableHashSet = new LanguageExt.HashSet<T>().AddRange(_found);
            _langExtImmutableList = new LanguageExt.Lst<T>().AddRange(_found);
            _langExtImmutableSet = new LanguageExt.Set<T>(_found);
        }

        [Benchmark]
        public bool ImmutableArray()
        {
            bool result = default;
            ImmutableArray<T> collection = _immutableArray;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableHashSet()
        {
            bool result = default;
            ImmutableHashSet<T> collection = _immutableHashSet;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableList()
        {
            bool result = default;
            ImmutableList<T> collection = _immutableList;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableSortedSet()
        {
            bool result = default;
            ImmutableSortedSet<T> collection = _immutableSortedSet;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtArr()
        {
            bool result = default;
            LanguageExt.Arr<T> collection = _langExtImmutableArray;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtHashSet()
        {
            bool result = default;
            LanguageExt.HashSet<T> collection = _langExtImmutableHashSet;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtLst()
        {
            bool result = default;
            LanguageExt.Lst<T> collection = _langExtImmutableList;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }

        [Benchmark]
        public bool LanguageExtSet()
        {
            bool result = default;
            LanguageExt.Set<T> collection = _langExtImmutableSet;
            T[] found = _found;

            for (int i = 0; i < found.Length; i++)
            {
                result ^= collection.Contains(found[i]);
            }

            return result;
        }
    }
}