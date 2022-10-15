## Iterating `Dictionary<,>` by `foreach`: deconstruct or access value by key

What's the price of accessing value using key?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2130 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.402
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT


```
|      Method |       Mean |    Error |   StdDev | Ratio |
|------------ |-----------:|---------:|---------:|------:|
|       Index | 1,238.9 ns | 23.70 ns | 37.59 ns |  1.00 |
| Deconstruct |   407.3 ns |  3.83 ns |  3.40 ns |  0.33 |
