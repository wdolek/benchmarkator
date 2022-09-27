## Having fun iterating `ImmutableArray<>`

Is it better to iterate forward or backward? Does it even matter?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                       Method | Length |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------- |------- |-----------:|----------:|----------:|------:|--------:|
|              ForLoopFrom0ToN |      1 |  0.2233 ns | 0.0230 ns | 0.0179 ns |  1.00 |    0.00 |
| ForLoopFrom0ToNWithPrefixInc |      1 |  0.2537 ns | 0.0337 ns | 0.0299 ns |  1.13 |    0.14 |
| ForLoopFromNTo0WithPrefixDec |      1 |  0.2706 ns | 0.0201 ns | 0.0168 ns |  1.22 |    0.15 |
|              ForLoopFromNTo0 |      1 |  0.4890 ns | 0.0400 ns | 0.0355 ns |  2.18 |    0.22 |
|    ForLoopFrom0ToNCallMethod |      1 |  0.6419 ns | 0.0397 ns | 0.0371 ns |  2.91 |    0.28 |
|                              |        |            |           |           |       |         |
|              ForLoopFromNTo0 |      4 |  1.6143 ns | 0.0563 ns | 0.0527 ns |  0.97 |    0.03 |
| ForLoopFromNTo0WithPrefixDec |      4 |  1.6353 ns | 0.0593 ns | 0.0495 ns |  0.99 |    0.04 |
| ForLoopFrom0ToNWithPrefixInc |      4 |  1.6381 ns | 0.0522 ns | 0.0407 ns |  0.99 |    0.02 |
|              ForLoopFrom0ToN |      4 |  1.6587 ns | 0.0390 ns | 0.0346 ns |  1.00 |    0.00 |
|    ForLoopFrom0ToNCallMethod |      4 |  2.1945 ns | 0.0468 ns | 0.0415 ns |  1.32 |    0.04 |
|                              |        |            |           |           |       |         |
| ForLoopFrom0ToNWithPrefixInc |     16 |  5.7744 ns | 0.0894 ns | 0.0698 ns |  1.00 |    0.02 |
|              ForLoopFrom0ToN |     16 |  5.7825 ns | 0.0925 ns | 0.0820 ns |  1.00 |    0.00 |
|    ForLoopFrom0ToNCallMethod |     16 |  6.0264 ns | 0.1530 ns | 0.1503 ns |  1.04 |    0.03 |
| ForLoopFromNTo0WithPrefixDec |     16 |  7.1627 ns | 0.1499 ns | 0.1402 ns |  1.24 |    0.03 |
|              ForLoopFromNTo0 |     16 |  7.2441 ns | 0.1745 ns | 0.2143 ns |  1.26 |    0.05 |
|                              |        |            |           |           |       |         |
|    ForLoopFrom0ToNCallMethod |    128 | 69.9170 ns | 1.4362 ns | 1.3434 ns |  0.99 |    0.03 |
|              ForLoopFrom0ToN |    128 | 70.4406 ns | 1.3067 ns | 1.3982 ns |  1.00 |    0.00 |
| ForLoopFrom0ToNWithPrefixInc |    128 | 70.6895 ns | 0.5971 ns | 0.4986 ns |  1.01 |    0.02 |
| ForLoopFromNTo0WithPrefixDec |    128 | 71.7860 ns | 1.0308 ns | 0.8608 ns |  1.02 |    0.03 |
|              ForLoopFromNTo0 |    128 | 75.9980 ns | 1.3680 ns | 1.2127 ns |  1.08 |    0.02 |
