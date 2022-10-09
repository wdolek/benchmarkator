## Adding to `ConcurrentBag<>`

When adding initial data to `ConcurrentBag<>`, how expensive is to `.Add` individual value and what approach is better?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2075 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT


```
|                           Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------------- |---------:|----------:|----------:|---------:|------:|--------:|-------:|-------:|----------:|
|              AddOnInitialization | 3.831 μs | 0.0603 μs | 0.0503 μs | 3.832 μs |  0.73 |    0.01 | 0.3624 | 0.1793 |      2 KB |
|        TryAddAfterInitialization | 5.257 μs | 0.0885 μs | 0.0785 μs | 5.239 μs |  1.00 |    0.00 | 0.3586 | 0.1755 |      2 KB |
| TryAddAfterInitializationForEach | 5.526 μs | 0.2318 μs | 0.6835 μs | 5.116 μs |  1.22 |    0.04 | 0.3662 | 0.1831 |      2 KB |
