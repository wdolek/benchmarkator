## Iterating `Dictionary<,>` by `foreach`: deconstruct or not deconstruct?

Is there any difference when deconstructing `KeyValuePair<,>`?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2130 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT


```
|                   Method |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |
|------------------------- |-----------:|---------:|---------:|-----------:|------:|--------:|
|          SimpleIteration |   453.8 ns | 28.15 ns | 81.68 ns |   413.0 ns |  1.00 |    0.00 |
| SimpleIterationWithIndex | 1,167.9 ns |  7.21 ns |  6.02 ns | 1,167.2 ns |  2.60 |    0.37 |
|     DeconstructIteration |   389.5 ns |  2.53 ns |  1.97 ns |   389.3 ns |  0.86 |    0.13 |
