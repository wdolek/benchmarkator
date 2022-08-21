## Iterating array and List<> using `foreach`

Comparison of using `foreach` when iterating over array and `List<>`.

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT


```
|            Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------ |-----------:|---------:|---------:|------:|--------:|
|             Array |   137.0 ns |  1.89 ns |  1.77 ns |  1.00 |    0.00 |
|              List |   380.7 ns |  7.58 ns |  7.09 ns |  2.78 |    0.06 |
| ArrayAsEnumerable | 1,072.2 ns | 20.54 ns | 27.42 ns |  7.82 |    0.23 |
