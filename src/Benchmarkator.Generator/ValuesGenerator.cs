using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmarkator.Generator;

public sealed class ValuesGenerator
{
    private const string StringChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static readonly ValuesGenerator Instance = new ValuesGenerator(DefaultValues.RandomSeed);

    private readonly Randomizer _rand;

    public ValuesGenerator(int seed)
    {
        _rand = new Randomizer(seed);
    }

    public T[] GenerateUniqueValues<T>(int count)
    {
        var set = new HashSet<T>(count);
        while (set.Count < count)
        {
            set.Add(GenerateValue<T>());
        }

        return set.ToArray();
    }

    public Dictionary<TKey, TValue> GenerateDictionary<TKey, TValue>(int count)
        where TKey : notnull
    {
        var dict = new Dictionary<TKey, TValue>(count);
        while (dict.Count < count)
        {
            var key = GenerateValue<TKey>();
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, GenerateValue<TValue>());
            }
        }

        return dict;
    }

    private T GenerateValue<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)_rand.Int();
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)_rand.String2(1, 50, StringChars);
        }

        throw new NotImplementedException($"Type {typeof(T)} not supported for generating data");
    }
}
