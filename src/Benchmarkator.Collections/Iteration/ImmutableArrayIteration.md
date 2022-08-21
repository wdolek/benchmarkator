## Having fun iterating `ImmutableArray<>`

Is it better to iterate forward or backward? Does it even matter?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                       Method | Length |      Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------- |------- |----------:|----------:|----------:|------:|--------:|
|              ForLoopFrom0ToN |      1 | 0.2232 ns | 0.0356 ns | 0.0350 ns |  1.00 |    0.00 |
|              ForLoopFromNTo0 |      1 | 0.2833 ns | 0.0389 ns | 0.0364 ns |  1.28 |    0.20 |
| ForLoopFrom0ToNWithPrefixInc |      1 | 0.3267 ns | 0.0396 ns | 0.0389 ns |  1.48 |    0.21 |
| ForLoopFromNTo0WithPrefixDec |      1 | 0.5192 ns | 0.0214 ns | 0.0190 ns |  2.32 |    0.35 |
|    ForLoopFrom0ToNCallMethod |      1 | 0.6004 ns | 0.0273 ns | 0.0213 ns |  2.63 |    0.41 |
|                              |        |           |           |           |       |         |
|              ForLoopFromNTo0 |      2 | 0.5792 ns | 0.0453 ns | 0.0522 ns |  0.92 |    0.09 |
| ForLoopFromNTo0WithPrefixDec |      2 | 0.6003 ns | 0.0288 ns | 0.0255 ns |  0.95 |    0.06 |
|              ForLoopFrom0ToN |      2 | 0.6358 ns | 0.0302 ns | 0.0267 ns |  1.00 |    0.00 |
| ForLoopFrom0ToNWithPrefixInc |      2 | 0.6602 ns | 0.0435 ns | 0.0386 ns |  1.04 |    0.08 |
|    ForLoopFrom0ToNCallMethod |      2 | 1.0259 ns | 0.0540 ns | 0.0530 ns |  1.61 |    0.10 |
|                              |        |           |           |           |       |         |
|              ForLoopFrom0ToN |      4 | 1.4929 ns | 0.0532 ns | 0.0498 ns |  1.00 |    0.00 |
| ForLoopFromNTo0WithPrefixDec |      4 | 1.6318 ns | 0.0632 ns | 0.0591 ns |  1.09 |    0.05 |
|              ForLoopFromNTo0 |      4 | 1.6520 ns | 0.0664 ns | 0.0621 ns |  1.11 |    0.06 |
| ForLoopFrom0ToNWithPrefixInc |      4 | 1.7151 ns | 0.0660 ns | 0.0649 ns |  1.15 |    0.05 |
|    ForLoopFrom0ToNCallMethod |      4 | 2.1068 ns | 0.0611 ns | 0.0572 ns |  1.41 |    0.06 |
|                              |        |           |           |           |       |         |
|              ForLoopFrom0ToN |      8 | 2.6051 ns | 0.0768 ns | 0.0718 ns |  1.00 |    0.00 |
| ForLoopFrom0ToNWithPrefixInc |      8 | 2.6161 ns | 0.0763 ns | 0.0713 ns |  1.01 |    0.05 |
|              ForLoopFromNTo0 |      8 | 2.9797 ns | 0.0889 ns | 0.0742 ns |  1.15 |    0.04 |
| ForLoopFromNTo0WithPrefixDec |      8 | 3.0699 ns | 0.0836 ns | 0.0741 ns |  1.18 |    0.03 |
|    ForLoopFrom0ToNCallMethod |      8 | 3.1687 ns | 0.0211 ns | 0.0187 ns |  1.22 |    0.04 |
