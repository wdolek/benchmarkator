## Comparison of `System.Collections.Immutable` with `LanguageExt`, instantiation/creation

### Constructor<int>, default size

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method |      Mean |     Error |    StdDev |    Median |
|-------------------------- |----------:|----------:|----------:|----------:|
|            ImmutableArray | 0.8450 ns | 0.0060 ns | 0.0053 ns | 0.8438 ns |
|       ImmutableDictionary | 0.0392 ns | 0.0075 ns | 0.0070 ns | 0.0375 ns |
|          ImmutableHashSet | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|             ImmutableList | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            ImmutableQueue | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            ImmutableStack | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| ImmutableSortedDictionary | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|        ImmutableSortedSet | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            LanguageExtArr | 0.0644 ns | 0.0322 ns | 0.0441 ns | 0.0783 ns |
|        LanguageExtHashMap | 0.0585 ns | 0.0224 ns | 0.0336 ns | 0.0637 ns |
|        LanguageExtHashSet | 0.0508 ns | 0.0041 ns | 0.0034 ns | 0.0506 ns |
|            LanguageExtLst | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            LanguageExtQue | 0.0302 ns | 0.0089 ns | 0.0083 ns | 0.0280 ns |
|           LanguageExtStck | 0.0243 ns | 0.0120 ns | 0.0106 ns | 0.0234 ns |
|            LanguageExtMap | 0.0550 ns | 0.0066 ns | 0.0055 ns | 0.0541 ns |
|            LanguageExtSet | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |

### Constructor<string>, default size

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method |      Mean |     Error |    StdDev |    Median |
|-------------------------- |----------:|----------:|----------:|----------:|
|            ImmutableArray | 5.4117 ns | 0.0443 ns | 0.0415 ns | 5.3913 ns |
|       ImmutableDictionary | 3.3232 ns | 0.0060 ns | 0.0054 ns | 3.3234 ns |
|          ImmutableHashSet | 3.2276 ns | 0.0079 ns | 0.0074 ns | 3.2283 ns |
|             ImmutableList | 3.2411 ns | 0.0157 ns | 0.0131 ns | 3.2387 ns |
|            ImmutableQueue | 3.7812 ns | 0.0178 ns | 0.0158 ns | 3.7760 ns |
|            ImmutableStack | 3.6740 ns | 0.0166 ns | 0.0147 ns | 3.6777 ns |
| ImmutableSortedDictionary | 3.3095 ns | 0.0554 ns | 0.0491 ns | 3.2853 ns |
|        ImmutableSortedSet | 3.4315 ns | 0.0072 ns | 0.0068 ns | 3.4324 ns |
|            LanguageExtArr | 0.0184 ns | 0.0112 ns | 0.0093 ns | 0.0149 ns |
|        LanguageExtHashMap | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|        LanguageExtHashSet | 0.0594 ns | 0.0031 ns | 0.0027 ns | 0.0604 ns |
|            LanguageExtLst | 0.0595 ns | 0.0101 ns | 0.0084 ns | 0.0557 ns |
|            LanguageExtQue | 0.0127 ns | 0.0073 ns | 0.0068 ns | 0.0119 ns |
|           LanguageExtStck | 0.0139 ns | 0.0113 ns | 0.0106 ns | 0.0098 ns |
|            LanguageExtMap | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            LanguageExtSet | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |

### Constructor<int>, from existing collection

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |           Mean |        Error |       StdDev |
|-------------------------- |----- |---------------:|-------------:|-------------:|
|            **ImmutableArray** |  **256** |       **108.6 ns** |      **2.87 ns** |      **2.68 ns** |
|       ImmutableDictionary |  256 |    78,344.8 ns |    239.53 ns |    212.34 ns |
|             ImmutableList |  256 |     4,127.3 ns |     16.81 ns |     15.72 ns |
|            ImmutableQueue |  256 |     1,161.7 ns |      7.19 ns |      5.61 ns |
|            ImmutableStack |  256 |     2,074.0 ns |      9.58 ns |      8.96 ns |
| ImmutableSortedDictionary |  256 |    39,471.4 ns |    109.79 ns |     91.68 ns |
|        ImmutableSortedSet |  256 |     6,894.7 ns |     41.22 ns |     38.55 ns |
|            LanguageExtArr |  256 |       104.9 ns |      0.72 ns |      0.64 ns |
|        LanguageExtHashMap |  256 |    49,289.7 ns |    973.71 ns |  1,705.38 ns |
|            LanguageExtLst |  256 |    25,746.3 ns |    172.44 ns |    161.30 ns |
|            LanguageExtQue |  256 |     4,789.6 ns |     17.17 ns |     14.34 ns |
|           LanguageExtStck |  256 |     2,526.3 ns |      7.57 ns |      6.71 ns |
|            LanguageExtMap |  256 |    85,775.0 ns |  1,617.40 ns |  1,588.50 ns |
|            LanguageExtSet |  256 |    84,940.0 ns |  1,682.48 ns |  1,937.55 ns |
|            **ImmutableArray** | **4096** |       **858.9 ns** |     **10.56 ns** |      **9.88 ns** |
|       ImmutableDictionary | 4096 | 2,033,821.7 ns |  6,394.63 ns |  5,668.67 ns |
|             ImmutableList | 4096 |    89,017.4 ns |    918.43 ns |    814.17 ns |
|            ImmutableQueue | 4096 |    25,291.3 ns |    245.10 ns |    229.27 ns |
|            ImmutableStack | 4096 |    39,126.7 ns |    131.69 ns |    123.18 ns |
| ImmutableSortedDictionary | 4096 |   991,173.8 ns |  2,370.03 ns |  2,100.97 ns |
|        ImmutableSortedSet | 4096 |   281,402.7 ns |    407.69 ns |    340.44 ns |
|            LanguageExtArr | 4096 |       917.2 ns |      9.14 ns |      8.55 ns |
|        LanguageExtHashMap | 4096 |   913,340.0 ns | 17,773.90 ns | 20,468.45 ns |
|            LanguageExtLst | 4096 |   582,389.4 ns |    862.67 ns |    764.73 ns |
|            LanguageExtQue | 4096 |   119,612.3 ns |  1,122.46 ns |    995.03 ns |
|           LanguageExtStck | 4096 |    48,446.7 ns |    229.47 ns |    203.42 ns |
|            LanguageExtMap | 4096 | 1,947,113.0 ns | 42,305.82 ns | 89,237.33 ns |
|            LanguageExtSet | 4096 | 1,957,117.4 ns | 47,758.46 ns | 92,014.35 ns |

### Constructor<string>, from existing collection

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |           Mean |         Error |        StdDev |
|-------------------------- |----- |---------------:|--------------:|--------------:|
|            **ImmutableArray** |  **256** |       **188.7 ns** |       **1.78 ns** |       **1.67 ns** |
|       ImmutableDictionary |  256 |   106,931.7 ns |     175.12 ns |     163.80 ns |
|             ImmutableList |  256 |     6,299.2 ns |      10.89 ns |       9.65 ns |
|            ImmutableQueue |  256 |     1,961.8 ns |      13.27 ns |      11.76 ns |
|            ImmutableStack |  256 |     2,916.2 ns |      21.32 ns |      18.90 ns |
| ImmutableSortedDictionary |  256 |   254,129.5 ns |     484.54 ns |     429.54 ns |
|        ImmutableSortedSet |  256 |   285,773.0 ns |     508.00 ns |     424.20 ns |
|            LanguageExtArr |  256 |       188.2 ns |       1.22 ns |       1.14 ns |
|        LanguageExtHashMap |  256 |    99,891.7 ns |   1,895.45 ns |   1,479.84 ns |
|            LanguageExtLst |  256 |    28,277.9 ns |      65.86 ns |      58.39 ns |
|            LanguageExtQue |  256 |     6,001.2 ns |      15.30 ns |      12.78 ns |
|           LanguageExtStck |  256 |     4,219.1 ns |      23.14 ns |      21.65 ns |
|            LanguageExtMap |  256 |   366,173.3 ns |   7,222.89 ns |  10,810.88 ns |
|            LanguageExtSet |  256 |   363,004.2 ns |  23,275.73 ns |  30,265.02 ns |
|            **ImmutableArray** | **4096** |     **2,163.9 ns** |      **38.36 ns** |      **35.88 ns** |
|       ImmutableDictionary | 4096 | 2,594,166.9 ns |   3,956.39 ns |   3,507.24 ns |
|             ImmutableList | 4096 |   115,346.6 ns |     964.18 ns |     854.72 ns |
|            ImmutableQueue | 4096 |    37,246.4 ns |     175.20 ns |     146.30 ns |
|            ImmutableStack | 4096 |    52,030.9 ns |     261.64 ns |     244.74 ns |
| ImmutableSortedDictionary | 4096 | 7,322,629.6 ns |  54,850.15 ns |  51,306.87 ns |
|        ImmutableSortedSet | 4096 | 7,139,658.8 ns |  18,510.41 ns |  15,457.03 ns |
|            LanguageExtArr | 4096 |     2,547.1 ns |      47.15 ns |      44.11 ns |
|        LanguageExtHashMap | 4096 | 1,927,943.6 ns |  87,309.28 ns | 249,098.28 ns |
|            LanguageExtLst | 4096 |   615,816.4 ns |   3,843.74 ns |   3,407.37 ns |
|            LanguageExtQue | 4096 |   146,367.1 ns |     552.29 ns |     516.61 ns |
|           LanguageExtStck | 4096 |    83,277.6 ns |     442.12 ns |     413.56 ns |
|            LanguageExtMap | 4096 | 8,155,348.0 ns | 288,008.09 ns | 849,198.36 ns |
|            LanguageExtSet | 4096 | 8,208,332.3 ns | 298,807.84 ns | 876,351.68 ns |
