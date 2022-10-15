## Iterating `Dictionary<,>` by `foreach`: deconstruct or not deconstruct?

Is there any difference when deconstructing `KeyValuePair<,>`?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2130 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.402
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT


```
|               Method |     Mean |   Error |  StdDev | Ratio | RatioSD |
|--------------------- |---------:|--------:|--------:|------:|--------:|
|      SimpleIteration | 381.1 ns | 5.36 ns | 4.76 ns |  1.00 |    0.00 |
| DeconstructIteration | 407.0 ns | 5.76 ns | 5.11 ns |  1.07 |    0.02 |
