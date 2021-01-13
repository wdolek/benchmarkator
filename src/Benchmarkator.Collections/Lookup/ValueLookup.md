## Lookup of value in collection

Having collection of items, what is the fastest way to find (structured) value in it?
Is `Dictionary<TKey,TValue>` the best option here, or should we use good ol' array when
collection is short?

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|             Method |         Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------------------- |----- |------------:|----------:|----------:|------:|--------:|
|   ArrayLookupFirst |        Array,First |    4 |   0.5050 ns | 0.0290 ns | 0.0271 ns |  1.00 |    0.00 |
|    ArrayLookupLast |         Array,Last |    4 |   2.7640 ns | 0.0885 ns | 0.0785 ns |  1.00 |    0.00 |
| ArrayLookupMissing |      Array,Missing |    4 |   1.9755 ns | 0.0292 ns | 0.0244 ns |  1.00 |    0.00 |
|                                                                                                        |
|    ListLookupFirst |         List,First |    4 |   0.7665 ns | 0.0208 ns | 0.0185 ns |     ? |       ? |
|     ListLookupLast |          List,Last |    4 |   3.3710 ns | 0.0265 ns | 0.0247 ns |     ? |       ? |
|  ListLookupMissing |       List,Missing |    4 |   3.3580 ns | 0.0352 ns | 0.0312 ns |     ? |       ? |
|                                                                                                        |
|    DictLookupFirst |   Dictionary,First |    4 |   7.1307 ns | 0.0817 ns | 0.0724 ns |     ? |       ? |
|     DictLookupLast |    Dictionary,Last |    4 |   7.1377 ns | 0.0650 ns | 0.0577 ns |     ? |       ? |
|  DictLookupMissing | Dictionary,Missing |    4 |   6.3819 ns | 0.0450 ns | 0.0421 ns |     ? |       ? |

|             Method |         Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------------------- |----- |------------:|----------:|----------:|------:|--------:|
|   ArrayLookupFirst |        Array,First |   16 |   0.4731 ns | 0.0259 ns | 0.0229 ns |  1.00 |    0.00 |
|    ArrayLookupLast |         Array,Last |   16 |   7.5572 ns | 0.0618 ns | 0.0578 ns |  1.00 |    0.00 |
| ArrayLookupMissing |      Array,Missing |   16 |   7.3454 ns | 0.0556 ns | 0.0521 ns |  1.00 |    0.00 |
|                                                                                                        |
|    ListLookupFirst |         List,First |   16 |   0.7593 ns | 0.0240 ns | 0.0225 ns |     ? |       ? |
|     ListLookupLast |          List,Last |   16 |  12.5585 ns | 0.0856 ns | 0.0759 ns |     ? |       ? |
|  ListLookupMissing |       List,Missing |   16 |  12.1288 ns | 0.0869 ns | 0.0813 ns |     ? |       ? |
|                                                                                                        |
|    DictLookupFirst |   Dictionary,First |   16 |   9.6770 ns | 0.0350 ns | 0.0327 ns |     ? |       ? |
|     DictLookupLast |    Dictionary,Last |   16 |   7.0745 ns | 0.0372 ns | 0.0348 ns |     ? |       ? |
|  DictLookupMissing | Dictionary,Missing |   16 |   8.5955 ns | 0.0537 ns | 0.0503 ns |     ? |       ? |

|             Method |         Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------------------- |----- |------------:|----------:|----------:|------:|--------:|
|   ArrayLookupFirst |        Array,First |   64 |   0.4737 ns | 0.0294 ns | 0.0275 ns |  1.00 |    0.00 |
|    ArrayLookupLast |         Array,Last |   64 |  28.7849 ns | 0.3092 ns | 0.2892 ns |  1.00 |    0.00 |
| ArrayLookupMissing |      Array,Missing |   64 |  28.7539 ns | 0.1827 ns | 0.1620 ns |  1.00 |    0.00 |
|                                                                                                        |
|    ListLookupFirst |         List,First |   64 |   0.7585 ns | 0.0242 ns | 0.0226 ns |     ? |       ? |
|     ListLookupLast |          List,Last |   64 |  44.6614 ns | 0.1675 ns | 0.1399 ns |     ? |       ? |
|  ListLookupMissing |       List,Missing |   64 |  44.4509 ns | 0.2037 ns | 0.1701 ns |     ? |       ? |
|                                                                                                        |
|    DictLookupFirst |   Dictionary,First |   64 |   8.2567 ns | 0.0479 ns | 0.0448 ns |     ? |       ? |
|     DictLookupLast |    Dictionary,Last |   64 |   7.5018 ns | 0.0623 ns | 0.0583 ns |     ? |       ? |
|  DictLookupMissing | Dictionary,Missing |   64 |   8.6111 ns | 0.0617 ns | 0.0577 ns |     ? |       ? |

|             Method |         Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------------------- |----- |------------:|----------:|----------:|------:|--------:|
|   ArrayLookupFirst |        Array,First |  128 |   0.4839 ns | 0.0260 ns | 0.0243 ns |  1.00 |    0.00 |
|    ArrayLookupLast |         Array,Last |  128 |  74.0659 ns | 0.2993 ns | 0.2653 ns |  1.00 |    0.00 |
| ArrayLookupMissing |      Array,Missing |  128 |  70.2869 ns | 0.3472 ns | 0.3247 ns |  1.00 |    0.00 |
|                                                                                                        |
|    ListLookupFirst |         List,First |  128 |   0.7660 ns | 0.0178 ns | 0.0149 ns |     ? |       ? |
|     ListLookupLast |          List,Last |  128 |  97.4065 ns | 0.5958 ns | 0.5573 ns |     ? |       ? |
|  ListLookupMissing |       List,Missing |  128 |  94.3053 ns | 0.5400 ns | 0.4787 ns |     ? |       ? |
|                                                                                                        |
|    DictLookupFirst |   Dictionary,First |  128 |   7.0824 ns | 0.0612 ns | 0.0572 ns |     ? |       ? |
|     DictLookupLast |    Dictionary,Last |  128 |   7.0998 ns | 0.0689 ns | 0.0644 ns |     ? |       ? |
|  DictLookupMissing | Dictionary,Missing |  128 |   6.4240 ns | 0.0668 ns | 0.0625 ns |     ? |       ? |

|             Method |         Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------------------- |----- |------------:|----------:|----------:|------:|--------:|
|   ArrayLookupFirst |        Array,First |  512 |   0.4929 ns | 0.0215 ns | 0.0190 ns |  1.00 |    0.00 |
|    ArrayLookupLast |         Array,Last |  512 | 308.1027 ns | 1.2997 ns | 1.1521 ns |  1.00 |    0.00 |
| ArrayLookupMissing |      Array,Missing |  512 | 308.6997 ns | 2.5426 ns | 2.3784 ns |  1.00 |    0.00 |
|                                                                                                        |
|    ListLookupFirst |         List,First |  512 |   0.7661 ns | 0.0178 ns | 0.0166 ns |     ? |       ? |
|     ListLookupLast |          List,Last |  512 | 443.2900 ns | 2.6982 ns | 2.2531 ns |     ? |       ? |
|  ListLookupMissing |       List,Missing |  512 | 432.3245 ns | 3.4198 ns | 3.1989 ns |     ? |       ? |
|                                                                                                        |
|    DictLookupFirst |   Dictionary,First |  512 |   9.8058 ns | 0.1065 ns | 0.0996 ns |     ? |       ? |
|     DictLookupLast |    Dictionary,Last |  512 |   7.1185 ns | 0.0444 ns | 0.0394 ns |     ? |       ? |
|  DictLookupMissing | Dictionary,Missing |  512 |   5.4143 ns | 0.0412 ns | 0.0365 ns |     ? |       ? |
