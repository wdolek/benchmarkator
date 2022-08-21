## Throw or return?

What's the price of throwing exception?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                   Method |            Mean |         Error |        StdDev |    Ratio | RatioSD |
|------------------------- |----------------:|--------------:|--------------:|---------:|--------:|
| SuccessWithSuccessResult |        330.8 ns |       6.62 ns |       9.27 ns |     0.19 |    0.01 |
|  FailureWithFailedResult |        333.4 ns |       6.49 ns |       7.47 ns |     0.19 |    0.00 |
|   SuccessWithNoException |      1,731.5 ns |      11.37 ns |      10.08 ns |     1.00 |    0.00 |
|     FailureWithException | 11,577,116.5 ns | 230,085.70 ns | 514,619.64 ns | 6,511.44 |  258.81 |
