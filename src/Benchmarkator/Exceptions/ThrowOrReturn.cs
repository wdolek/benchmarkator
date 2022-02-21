using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Exceptions;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ThrowOrReturn
{
    private const int NumOfIterations = 1000;

    [Benchmark(Baseline = true)]
    public int SuccessWithNoException()
    {
        var r = 0;
        for (var i = 0; i < NumOfIterations; i++)
        {
            try
            {
                r = GetResult(@throw: false);
            }
            catch
            {
                r = -1;
            }
        }

        return r;
    }

    [Benchmark]
    public int SuccessWithSuccessResult()
    {
        var r = 0;
        for (var i = 0; i < NumOfIterations; i++)
        {
            var result = GetSuccessResult();
            r = result.IsSuccess
                ? result.Value
                : -1;
        }

        return r;
    }

    [Benchmark]
    public int FailureWithException()
    {
        var r = 0;
        for (var i = 0; i < NumOfIterations; i++)
        {
            try
            {
                r = GetResult(@throw: true);
            }
            catch
            {
                r = -1;
            }
        }

        return r;
    }

    [Benchmark]
    public int FailureWithFailedResult()
    {
        var r = 0;
        for (var i = 0; i < NumOfIterations; i++)
        {
            var result = GetFailedResult();
            r = result.IsSuccess
                ? result.Value
                : -1;
        }

        return r;
    }

    private static int GetResult(bool @throw)
    {
        if (@throw)
        {
            throw new Exception("Nope");
        }

        return 1;
    }

    private static Result<int, string> GetSuccessResult()
    {
        return new Result<int, string>(true, 1, null);
    }

    private static Result<int, string> GetFailedResult()
    {
        return new Result<int, string>(false, default, "Nope");
    }

    private readonly struct Result<TValue, TError>
    {
        public Result(bool isSuccess, TValue? value, TError? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public TValue? Value { get; }
        public TError? Error { get; }
    }
}
