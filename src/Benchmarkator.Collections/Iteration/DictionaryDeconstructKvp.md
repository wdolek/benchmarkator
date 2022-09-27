## Iterating `Dictionary<,>` by `foreach`: deconstruct or not deconstruct?

Is there any difference when deconstructing `KeyValuePair<,>`?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|               Method |     Mean |   Error |  StdDev | Ratio |
|--------------------- |---------:|--------:|--------:|------:|
|      SimpleIteration | 385.2 ns | 5.96 ns | 5.28 ns |  1.00 |
| DeconstructIteration | 385.5 ns | 4.86 ns | 4.06 ns |  1.00 |
