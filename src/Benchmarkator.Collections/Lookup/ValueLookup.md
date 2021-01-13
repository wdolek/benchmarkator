## Lookup of value in collection

Having collection of items, what is the fastest way to find (structured) value in it?
Is `Dictionary<TKey,TValue>` the best option here, or should we use good ol' array when
collection is short?

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]   : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  ShortRun : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|             Method | Categories | Size |        Mean |       Error |    StdDev | Ratio | RatioSD |
|------------------- |----------- |----- |------------:|------------:|----------:|------:|--------:|
|   ArrayLookupFirst |      First |    4 |   0.5580 ns |   0.7898 ns | 0.0433 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |    4 |   0.9958 ns |   0.7461 ns | 0.0409 ns |  1.80 |    0.22 |
|    DictLookupFirst |      First |    4 |   8.6415 ns |  16.6360 ns | 0.9119 ns | 15.57 |    2.23 |
|                    |            |      |             |             |           |       |         |
|    ArrayLookupLast |       Last |    4 |   2.8392 ns |   0.9516 ns | 0.0522 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |    4 |   3.6830 ns |   0.9002 ns | 0.0493 ns |  1.30 |    0.03 |
|     DictLookupLast |       Last |    4 |   7.6745 ns |   0.4949 ns | 0.0271 ns |  2.70 |    0.04 |
|                    |            |      |             |             |           |       |         |
| ArrayLookupMissing |    Missing |    4 |   1.4926 ns |   0.9145 ns | 0.0501 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |    4 |   4.2163 ns |   1.7988 ns | 0.0986 ns |  2.83 |    0.06 |
|  DictLookupMissing |    Missing |    4 |   6.6516 ns |   0.3165 ns | 0.0173 ns |  4.46 |    0.16 |
|                    |            |      |             |             |           |       |         |
|   ArrayLookupFirst |      First |   16 |   0.5997 ns |   1.0855 ns | 0.0595 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |   16 |   1.0491 ns |   0.6879 ns | 0.0377 ns |  1.76 |    0.24 |
|    DictLookupFirst |      First |   16 |  10.4391 ns |   1.7895 ns | 0.0981 ns | 17.53 |    1.89 |
|                    |            |      |             |             |           |       |         |
|     DictLookupLast |       Last |   16 |   7.5982 ns |   1.4253 ns | 0.0781 ns |  0.94 |    0.01 |
|    ArrayLookupLast |       Last |   16 |   8.0493 ns |   0.6613 ns | 0.0362 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |   16 |  20.4331 ns |   1.2453 ns | 0.0683 ns |  2.54 |    0.01 |
|                    |            |      |             |             |           |       |         |
|  DictLookupMissing |    Missing |   16 |   8.7710 ns |   2.0300 ns | 0.1113 ns |  0.75 |    0.03 |
| ArrayLookupMissing |    Missing |   16 |  11.6518 ns |   4.9794 ns | 0.2729 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |   16 |  12.8385 ns |   4.9785 ns | 0.2729 ns |  1.10 |    0.01 |
|                    |            |      |             |             |           |       |         |
|   ArrayLookupFirst |      First |   64 |   0.4602 ns |   0.9163 ns | 0.0502 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |   64 |   1.0325 ns |   0.9595 ns | 0.0526 ns |  2.26 |    0.29 |
|    DictLookupFirst |      First |   64 |   9.3571 ns |   5.2096 ns | 0.2856 ns | 20.53 |    2.68 |
|                    |            |      |             |             |           |       |         |
|     DictLookupLast |       Last |   64 |   7.6526 ns |   1.7597 ns | 0.0965 ns |  0.24 |    0.00 |
|    ArrayLookupLast |       Last |   64 |  32.2519 ns |   5.5597 ns | 0.3047 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |   64 |  48.4083 ns |  15.8468 ns | 0.8686 ns |  1.50 |    0.04 |
|                    |            |      |             |             |           |       |         |
|  DictLookupMissing |    Missing |   64 |   9.2917 ns |   7.7872 ns | 0.4268 ns |  0.28 |    0.02 |
| ArrayLookupMissing |    Missing |   64 |  32.7482 ns |   9.8611 ns | 0.5405 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |   64 |  55.7414 ns | 128.5509 ns | 7.0463 ns |  1.70 |    0.24 |
|                    |            |      |             |             |           |       |         |
|   ArrayLookupFirst |      First |  128 |   0.4089 ns |   0.0376 ns | 0.0021 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |  128 |   1.0488 ns |   0.7713 ns | 0.0423 ns |  2.56 |    0.09 |
|    DictLookupFirst |      First |  128 |   8.0330 ns |   2.3760 ns | 0.1302 ns | 19.65 |    0.35 |
|                    |            |      |             |             |           |       |         |
|     DictLookupLast |       Last |  128 |   7.7156 ns |   0.7370 ns | 0.0404 ns |  0.10 |    0.00 |
|    ArrayLookupLast |       Last |  128 |  76.6564 ns |  10.7352 ns | 0.5884 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |  128 | 182.5638 ns |  25.4275 ns | 1.3938 ns |  2.38 |    0.03 |
|                    |            |      |             |             |           |       |         |
|  DictLookupMissing |    Missing |  128 |   6.6252 ns |   2.1207 ns | 0.1162 ns |  0.09 |    0.00 |
| ArrayLookupMissing |    Missing |  128 |  75.1295 ns |   5.0531 ns | 0.2770 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |  128 | 181.8757 ns |  26.5310 ns | 1.4543 ns |  2.42 |    0.02 |
|                    |            |      |             |             |           |       |         |
|   ArrayLookupFirst |      First |  512 |   0.6132 ns |   0.5753 ns | 0.0315 ns |  1.00 |    0.00 |
|    ListLookupFirst |      First |  512 |   0.6922 ns |   1.1020 ns | 0.0604 ns |  1.13 |    0.12 |
|    DictLookupFirst |      First |  512 |  10.5950 ns |   3.6767 ns | 0.2015 ns | 17.31 |    1.00 |
|                    |            |      |             |             |           |       |         |
|     DictLookupLast |       Last |  512 |   7.7158 ns |   1.9537 ns | 0.1071 ns |  0.02 |    0.00 |
|    ArrayLookupLast |       Last |  512 | 342.9829 ns |  29.2244 ns | 1.6019 ns |  1.00 |    0.00 |
|     ListLookupLast |       Last |  512 | 487.7923 ns |  50.1151 ns | 2.7470 ns |  1.42 |    0.01 |
|                    |            |      |             |             |           |       |         |
|  DictLookupMissing |    Missing |  512 |   5.6358 ns |   1.3522 ns | 0.0741 ns |  0.02 |    0.00 |
| ArrayLookupMissing |    Missing |  512 | 340.2287 ns |  14.9082 ns | 0.8172 ns |  1.00 |    0.00 |
|  ListLookupMissing |    Missing |  512 | 725.4401 ns |  84.7125 ns | 4.6434 ns |  2.13 |    0.02 |
