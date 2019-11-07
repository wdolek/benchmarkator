﻿using System;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Collections.Contains
{
    [BenchmarkCategory(Categories.CoreFX, Categories.Collections, Categories.GenericCollections)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GenericTypeArguments(typeof(int))] // value type
    [GenericTypeArguments(typeof(string))] // reference type
    public class ContainsFalse<T> 
        where T : IEquatable<T>
    {
        private T[] _notFound;
        
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
            T[] values = ValuesGenerator.ArrayOfUniqueValues<T>(Size * 2);
            
            _notFound = values
                .Take(Size)
                .ToArray();
            
            var secondHalf = values
                .Skip(Size)
                .Take(Size)
                .ToArray();
            
            // corefx
            _immutableArray = System.Collections.Immutable.ImmutableArray.CreateRange<T>(secondHalf);
            _immutableHashSet = System.Collections.Immutable.ImmutableHashSet.CreateRange<T>(secondHalf);
            _immutableList = System.Collections.Immutable.ImmutableList.CreateRange<T>(secondHalf);
            _immutableSortedSet = System.Collections.Immutable.ImmutableSortedSet.CreateRange<T>(secondHalf);
            
            // LanguageExt.Core
            _langExtImmutableArray = new LanguageExt.Arr<T>().AddRange(secondHalf);
            _langExtImmutableHashSet = new LanguageExt.HashSet<T>().AddRange(secondHalf);
            _langExtImmutableList = new LanguageExt.Lst<T>().AddRange(secondHalf);
            _langExtImmutableSet = new LanguageExt.Set<T>(secondHalf);
        }

        [Benchmark]
        public bool ImmutableArray()
        {
            bool result = default;
            ImmutableArray<T> collection = _immutableArray;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableHashSet()
        {
            bool result = default;
            ImmutableHashSet<T> collection = _immutableHashSet;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableList()
        {
            bool result = default;
            ImmutableList<T> collection = _immutableList;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }

        [Benchmark]
        public bool ImmutableSortedSet()
        {
            bool result = default;
            ImmutableSortedSet<T> collection = _immutableSortedSet;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }
        
        [Benchmark]
        public bool LanguageExtArr()
        {
            bool result = default;
            LanguageExt.Arr<T> collection = _langExtImmutableArray;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }
        
        [Benchmark]
        public bool LanguageExtHashSet()
        {
            bool result = default;
            LanguageExt.HashSet<T> collection = _langExtImmutableHashSet;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }
        
        [Benchmark]
        public bool LanguageExtLst()
        {
            bool result = default;
            LanguageExt.Lst<T> collection = _langExtImmutableList;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }
        
        [Benchmark]
        public bool LanguageExtSet()
        {
            bool result = default;
            LanguageExt.Set<T> collection = _langExtImmutableSet;
            T[] notFound = _notFound;
            
            for (int i = 0; i < notFound.Length; i++)
            {
                result ^= collection.Contains(notFound[i]);
            }

            return result;
        }        
    }
}