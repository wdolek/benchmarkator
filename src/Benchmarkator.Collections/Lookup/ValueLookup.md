## Lookup of value in collection

Having collection of items, what is the fastest way to find (structured) value in it?
Is `Dictionary<TKey,TValue>` the best option here, or should we use good ol' array when
collection is short?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]   : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  ShortRun : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1
WarmupCount=3
```
|             Method | Categories | Size |        Mean |       Error |     StdDev | Ratio | RatioSD |
|------------------- |----------- |----- |------------:|------------:|-----------:|------:|--------:|
|   ArrayLookupFirst |      First |    4 |   0.5657 ns |   0.6618 ns |  0.0363 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |    4 |   0.7152 ns |   2.2651 ns |  0.1242 ns |  1.27 |    0.26 |
|    DictLookupFirst |      First |    4 |   8.3127 ns |  17.2711 ns |  0.9467 ns | 14.66 |    0.71 |
|                    |            |      |             |             |            |       |         |
|    ArrayLookupLast |       Last |    4 |   2.7694 ns |   2.1951 ns |  0.1203 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |    4 |   3.7819 ns |   0.8409 ns |  0.0461 ns |  1.37 |    0.07 |
|     DictLookupLast |       Last |    4 |   7.5195 ns |   0.4922 ns |  0.0270 ns |  2.72 |    0.13 |
|                    |            |      |             |             |            |       |         |
| ArrayLookupMissing |    Missing |    4 |   2.3032 ns |   0.4549 ns |  0.0249 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |    4 |   3.8810 ns |   0.1269 ns |  0.0070 ns |  1.69 |    0.02 |
|  DictLookupMissing |    Missing |    4 |   7.2522 ns |   0.1733 ns |  0.0095 ns |  3.15 |    0.03 |
|                    |            |      |             |             |            |       |         |
|   ArrayLookupFirst |      First |   16 |   0.5052 ns |   0.2029 ns |  0.0111 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |   16 |   0.6597 ns |   0.3617 ns |  0.0198 ns |  1.31 |    0.04 |
|    DictLookupFirst |      First |   16 |  10.1857 ns |   0.8382 ns |  0.0459 ns | 20.17 |    0.52 |
|                    |            |      |             |             |            |       |         |
|     DictLookupLast |       Last |   16 |   7.9673 ns |   3.2842 ns |  0.1800 ns |  0.81 |    0.02 |
|    ArrayLookupLast |       Last |   16 |   9.7905 ns |   0.1535 ns |  0.0084 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |   16 |  19.7667 ns |   0.5566 ns |  0.0305 ns |  2.02 |    0.00 |
|                    |            |      |             |             |            |       |         |
| ArrayLookupMissing |    Missing |   16 |   9.2578 ns |   0.7516 ns |  0.0412 ns |  1.00 |    0.00 |
|  DictLookupMissing |    Missing |   16 |   9.7767 ns |   2.0647 ns |  0.1132 ns |  1.06 |    0.01 |
|  ListLookupMissing |    Missing |   16 |  20.8642 ns |  20.2916 ns |  1.1123 ns |  2.25 |    0.12 |
|                    |            |      |             |             |            |       |         |
|   ArrayLookupFirst |      First |   64 |   0.6314 ns |   2.2446 ns |  0.1230 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |   64 |   0.6692 ns |   0.1592 ns |  0.0087 ns |  1.09 |    0.25 |
|    DictLookupFirst |      First |   64 |   9.1905 ns |   1.4587 ns |  0.0800 ns | 15.00 |    3.39 |
|                    |            |      |             |             |            |       |         |
|     DictLookupLast |       Last |   64 |   8.2250 ns |   0.4255 ns |  0.0233 ns |  0.21 |    0.01 |
|    ArrayLookupLast |       Last |   64 |  39.8194 ns |  24.4838 ns |  1.3420 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |   64 |  83.4625 ns |   3.3309 ns |  0.1826 ns |  2.10 |    0.07 |
|                    |            |      |             |             |            |       |         |
|  DictLookupMissing |    Missing |   64 |   9.3276 ns |   3.7162 ns |  0.2037 ns |  0.25 |    0.01 |
| ArrayLookupMissing |    Missing |   64 |  37.3394 ns |  18.2431 ns |  1.0000 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |   64 |  88.5167 ns |  17.1972 ns |  0.9426 ns |  2.37 |    0.09 |
|                    |            |      |             |             |            |       |         |
|   ArrayLookupFirst |      First |  128 |   0.5633 ns |   0.1721 ns |  0.0094 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |  128 |   0.8455 ns |   0.3225 ns |  0.0177 ns |  1.50 |    0.03 |
|    DictLookupFirst |      First |  128 |   7.8288 ns |   7.4080 ns |  0.4061 ns | 13.91 |    0.92 |
|                    |            |      |             |             |            |       |         |
|     DictLookupLast |       Last |  128 |   7.1141 ns |   0.4536 ns |  0.0249 ns |  0.09 |    0.00 |
|    ArrayLookupLast |       Last |  128 |  78.2129 ns |   5.4081 ns |  0.2964 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |  128 | 183.4996 ns | 109.4804 ns |  6.0010 ns |  2.35 |    0.07 |
|                    |            |      |             |             |            |       |         |
|  DictLookupMissing |    Missing |  128 |   7.2917 ns |   2.1516 ns |  0.1179 ns |  0.10 |    0.00 |
| ArrayLookupMissing |    Missing |  128 |  75.5424 ns |   0.8904 ns |  0.0488 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |  128 | 178.0170 ns |  87.1320 ns |  4.7760 ns |  2.36 |    0.06 |
|                    |            |      |             |             |            |       |         |
|    ListLookupFirst |      First |  512 |   0.5178 ns |   0.4412 ns |  0.0242 ns |  0.96 |    0.15 |
|   ArrayLookupFirst |      First |  512 |   0.5450 ns |   1.1779 ns |  0.0646 ns |  1.00 |    0.00 |
|    DictLookupFirst |      First |  512 |  10.2390 ns |   2.2026 ns |  0.1207 ns | 18.94 |    1.91 |
|                    |            |      |             |             |            |       |         |
|     DictLookupLast |       Last |  512 |   7.5507 ns |   1.4540 ns |  0.0797 ns |  0.02 |    0.00 |
|    ArrayLookupLast |       Last |  512 | 332.8749 ns |  87.7630 ns |  4.8106 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |  512 | 703.5220 ns | 286.6291 ns | 15.7111 ns |  2.11 |    0.03 |
|                    |            |      |             |             |            |       |         |
|  DictLookupMissing |    Missing |  512 |   5.7155 ns |   2.8976 ns |  0.1588 ns |  0.02 |    0.00 |
| ArrayLookupMissing |    Missing |  512 | 328.8062 ns |  24.9660 ns |  1.3685 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |  512 | 699.8624 ns | 333.4925 ns | 18.2798 ns |  2.13 |    0.06 |
