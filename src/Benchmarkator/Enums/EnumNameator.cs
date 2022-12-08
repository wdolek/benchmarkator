using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Enums;

[MemoryDiagnoser]
public class EnumNameator
{
    private enum Number
    {
        One = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
        Nineteen,
        Twenty,
        TwentyOne,
        TwentyTwo,
        TwentyThree,
        TwentyFour,
        TwentyFive,
        TwentySix,
        TwentySeven,
        TwentyEight,
        TwentyNine,
        Thirty,
        ThirtyOne,
        ThirtyTwo,
        ThirtyThree,
        ThirtyFour,
        ThirtyFive,
        ThirtySix,
        ThirtySeven,
        ThirtyEight,
        ThirtyNine,
        Forty,
        FortyOne,
        FortyTwo,
        FortyThree,
        FortyFour,
        FortyFive,
        FortySix,
        FortySeven,
        FortyEight,
        FortyNine,
        Fifty,
    }

    private Number[] _enums;

    [GlobalSetup]
    public void Setup()
    {
        var enums = new Number[50];
        for (int i = 0; i < 50; i++)
        {
            enums[i] = (Number)(i + 1);
        }

        _enums = enums;
    }

    [Benchmark(Baseline = true)]
    public string EnumToString()
    {
        var arr = _enums;

        string str = null!;
        foreach (var number in arr)
        {
            str = number.ToString();
        }

        return str;
    }

    [Benchmark]
    public string GetEnumName()
    {
        var arr = _enums;

        string str = null!;
        foreach (var number in arr)
        {
            str = Enum.GetName(number)!;
        }

        return str;
    }
}
