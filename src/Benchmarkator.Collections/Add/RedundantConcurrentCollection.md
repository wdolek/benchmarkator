## Adding to `ConcurrentDictionary` when not really needed

After refactoring/simplifying code, what's the price of using `ConcurrentDictionary<,>` once it's not really needed?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2075 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT


```
|               Method | Categories |        Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------- |------------:|---------:|---------:|------:|--------:|
|                 List |       List |    383.8 ns |  3.36 ns |  3.14 ns |  1.00 |    0.00 |
|        ConcurrentBag |       List |  4,146.5 ns | 73.59 ns | 68.83 ns | 10.80 |    0.19 |
|                      |            |             |          |          |       |         |
|           Dictionary | Dictionary |  2,119.5 ns | 28.52 ns | 25.29 ns |  1.00 |    0.00 |
| ConcurrentDictionary | Dictionary | 12,852.7 ns | 83.10 ns | 77.73 ns |  6.07 |    0.08 |
