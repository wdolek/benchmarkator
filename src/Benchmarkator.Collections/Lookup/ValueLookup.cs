using BenchmarkDotNet.Attributes;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarkator.Collections.Lookup
{
    [CategoriesColumn]
    public class ValueLookup
    {
        private int _existingIdFirst;
        private int _existingIdLast;
        private int _missingId;

        private ValueClass[] _array = null!;
        private List<ValueClass> _list = null!;
        private Dictionary<int, ValueClass> _dict = null!;

        [Params(4, 16, 128)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            Randomizer.Seed = new Random(123456);

            var valuesBuilder = new Faker<ValueClass>()
                .StrictMode(true)
                .RuleFor(v => v.Id, f => f.Random.Int(min: 1))
                .RuleFor(v => v.Name, f => f.Name.FirstName())
                .RuleFor(v => v.Index, f => f.IndexFaker);

            var source = valuesBuilder.Generate(Size);

            _existingIdFirst = source[0].Id;
            _existingIdLast = source[^1].Id;
            _missingId = 0;

            _array = source.ToArray();
            _list = new List<ValueClass>(source);
            _dict = new Dictionary<int, ValueClass>(
                source.Select(v => new KeyValuePair<int, ValueClass>(v.Id, v)));
        }

        [Benchmark]
        [BenchmarkCategory("Array", "First")]
        public ValueClass? ArrayLookupFirst()
        {
            var local = _array;
            for (var i = 0; i < local.Length; i++)
            {
                if (local[i].Id == _existingIdFirst)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("Array", "Last")]
        public ValueClass? ArrayLookupLast()
        {
            var local = _array;
            for (var i = 0; i < local.Length; i++)
            {
                if (local[i].Id == _existingIdLast)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("Array", "Missing")]
        public ValueClass? ArrayLookupMissing()
        {
            var local = _array;
            for (var i = 0; i < local.Length; i++)
            {
                if (local[i].Id == _missingId)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("List", "First")]
        public ValueClass? ListLookupFirst()
        {
            var local = _list;
            for (var i = 0; i < local.Count; i++)
            {
                if (local[i].Id == _existingIdFirst)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("List", "Last")]
        public ValueClass? ListLookupLast()
        {
            var local = _list;
            for (var i = 0; i < local.Count; i++)
            {
                if (local[i].Id == _existingIdLast)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("List", "Missing")]
        public ValueClass? ListLookupMissing()
        {
            var local = _list;
            for (var i = 0; i < local.Count; i++)
            {
                if (local[i].Id == _missingId)
                {
                    return local[i];
                }
            }

            return null;
        }

        [Benchmark]
        [BenchmarkCategory("Dictionary", "First")]
        public ValueClass? DictLookupFirst() =>
            _dict.TryGetValue(_existingIdFirst, out var value)
                ? value
                : null;

        [Benchmark]
        [BenchmarkCategory("Dictionary", "Last")]
        public ValueClass? DictLookupLast() =>
            _dict.TryGetValue(_existingIdLast, out var value)
                ? value
                : null;

        [Benchmark]
        [BenchmarkCategory("Dictionary", "Missing")]
        public ValueClass? DictLookupMissing() =>
            _dict.TryGetValue(_missingId, out var value)
                ? value
                : null;

        public class ValueClass
        {
            public int Id { get; init; }
            public string Name { get; init; } = null!;
            public int Index { get; init; }
        }
    }
}
