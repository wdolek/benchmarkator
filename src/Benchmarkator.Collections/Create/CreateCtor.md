## Comparison of `System.Collections.Immutable` with `LanguageExt`, instantiation/creation

### Constructor<int>, default size

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method |      Mean |     Error |    StdDev |    Median |
|-------------------------- |----------:|----------:|----------:|----------:|
|            ImmutableArray | 0.9450 ns | 0.0546 ns | 0.0561 ns | 0.9293 ns |
|       ImmutableDictionary | 0.0182 ns | 0.0373 ns | 0.0349 ns | 0.0000 ns |
|          ImmutableHashSet | 0.0550 ns | 0.0576 ns | 0.0617 ns | 0.0184 ns |
|             ImmutableList | 0.0404 ns | 0.0545 ns | 0.0483 ns | 0.0228 ns |
|            ImmutableQueue | 0.0159 ns | 0.0376 ns | 0.0386 ns | 0.0000 ns |
|            ImmutableStack | 0.0278 ns | 0.0442 ns | 0.0575 ns | 0.0000 ns |
| ImmutableSortedDictionary | 0.0082 ns | 0.0204 ns | 0.0181 ns | 0.0000 ns |
|        ImmutableSortedSet | 0.0123 ns | 0.0297 ns | 0.0305 ns | 0.0000 ns |
|            LanguageExtArr | 0.0043 ns | 0.0062 ns | 0.0173 ns | 0.0000 ns |
|        LanguageExtHashMap | 0.0021 ns | 0.0072 ns | 0.0064 ns | 0.0000 ns |
|        LanguageExtHashSet | 0.0159 ns | 0.0186 ns | 0.0174 ns | 0.0173 ns |
|            LanguageExtLst | 0.0395 ns | 0.0265 ns | 0.0248 ns | 0.0515 ns |
|            LanguageExtQue | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|           LanguageExtStck | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            LanguageExtMap | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|            LanguageExtSet | 0.0988 ns | 0.0318 ns | 0.0312 ns | 0.0918 ns |

### Constructor<string>, default size

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method |      Mean |     Error |    StdDev |    Median |
|-------------------------- |----------:|----------:|----------:|----------:|
|            ImmutableArray | 6.3089 ns | 0.1510 ns | 0.1412 ns | 6.3395 ns |
|       ImmutableDictionary | 5.0311 ns | 0.1841 ns | 0.4585 ns | 5.0839 ns |
|          ImmutableHashSet | 4.7828 ns | 0.1861 ns | 0.2354 ns | 4.7783 ns |
|             ImmutableList | 4.5509 ns | 0.1669 ns | 0.1479 ns | 4.5025 ns |
|            ImmutableQueue | 4.8036 ns | 0.1844 ns | 0.2398 ns | 4.8338 ns |
|            ImmutableStack | 4.7860 ns | 0.1809 ns | 0.2083 ns | 4.7439 ns |
| ImmutableSortedDictionary | 4.6892 ns | 0.1862 ns | 0.2217 ns | 4.6814 ns |
|        ImmutableSortedSet | 4.7927 ns | 0.1877 ns | 0.2809 ns | 4.7323 ns |
|            LanguageExtArr | 0.0356 ns | 0.0345 ns | 0.0383 ns | 0.0186 ns |
|        LanguageExtHashMap | 0.0073 ns | 0.0102 ns | 0.0095 ns | 0.0000 ns |
|        LanguageExtHashSet | 0.0309 ns | 0.0304 ns | 0.0395 ns | 0.0082 ns |
|            LanguageExtLst | 0.0010 ns | 0.0043 ns | 0.0036 ns | 0.0000 ns |
|            LanguageExtQue | 0.0151 ns | 0.0232 ns | 0.0239 ns | 0.0049 ns |
|           LanguageExtStck | 0.0178 ns | 0.0195 ns | 0.0173 ns | 0.0142 ns |
|            LanguageExtMap | 0.0031 ns | 0.0080 ns | 0.0071 ns | 0.0000 ns |
|            LanguageExtSet | 0.0142 ns | 0.0222 ns | 0.0228 ns | 0.0000 ns |

### Constructor<int>, from existing collection

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |           Mean |         Error |        StdDev |
|-------------------------- |----- |---------------:|--------------:|--------------:|
|            **ImmutableArray** |  **256** |       **198.8 ns** |       **4.02 ns** |      **10.02 ns** |
|       ImmutableDictionary |  256 |    81,401.4 ns |   1,587.92 ns |   1,559.55 ns |
|             ImmutableList |  256 |     7,420.4 ns |     147.79 ns |     234.41 ns |
|            ImmutableQueue |  256 |     2,365.3 ns |      46.52 ns |      73.79 ns |
|            ImmutableStack |  256 |     3,627.4 ns |      71.45 ns |     123.25 ns |
| ImmutableSortedDictionary |  256 |    56,113.0 ns |   1,094.27 ns |   1,603.96 ns |
|        ImmutableSortedSet |  256 |    10,946.6 ns |     204.25 ns |     352.32 ns |
|            LanguageExtArr |  256 |       193.1 ns |       3.81 ns |       7.06 ns |
|        LanguageExtHashMap |  256 |    88,840.6 ns |   8,360.91 ns |  24,123.14 ns |
|            LanguageExtLst |  256 |    36,738.2 ns |     687.95 ns |     609.85 ns |
|            LanguageExtQue |  256 |     7,990.5 ns |     159.23 ns |     243.16 ns |
|           LanguageExtStck |  256 |     4,694.0 ns |      70.72 ns |      62.69 ns |
|            LanguageExtMap |  256 |   109,848.5 ns |   7,615.94 ns |  22,095.22 ns |
|            LanguageExtSet |  256 |   112,386.3 ns |   7,222.35 ns |  20,722.26 ns |
|            **ImmutableArray** | **4096** |     **2,127.9 ns** |      **49.48 ns** |     **143.54 ns** |
|       ImmutableDictionary | 4096 | 2,584,373.3 ns |  51,525.75 ns |  87,494.63 ns |
|             ImmutableList | 4096 |   149,026.0 ns |   2,896.89 ns |   4,335.92 ns |
|            ImmutableQueue | 4096 |    50,054.0 ns |     979.32 ns |   1,790.75 ns |
|            ImmutableStack | 4096 |    68,584.0 ns |   1,178.73 ns |   1,490.72 ns |
| ImmutableSortedDictionary | 4096 | 1,578,749.6 ns |  30,589.94 ns |  45,785.58 ns |
|        ImmutableSortedSet | 4096 |   392,681.0 ns |   6,026.96 ns |   5,342.74 ns |
|            LanguageExtArr | 4096 |     2,170.7 ns |      43.41 ns |     107.29 ns |
|        LanguageExtHashMap | 4096 | 1,379,390.0 ns | 152,199.46 ns | 448,763.55 ns |
|            LanguageExtLst | 4096 |   795,041.6 ns |  12,839.48 ns |  13,738.10 ns |
|            LanguageExtQue | 4096 |   186,187.1 ns |   3,680.54 ns |   6,542.16 ns |
|           LanguageExtStck | 4096 |    83,166.8 ns |   1,639.41 ns |   2,552.36 ns |
|            LanguageExtMap | 4096 | 1,995,646.9 ns | 298,683.93 ns | 871,275.03 ns |
|            LanguageExtSet | 4096 | 2,110,593.5 ns | 220,661.07 ns | 625,978.74 ns |

### Constructor<string>, from existing collection

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |           Mean |        Error |        StdDev |
|-------------------------- |----- |---------------:|-------------:|--------------:|
|            **ImmutableArray** |  **256** |       **378.2 ns** |      **7.53 ns** |      **13.96 ns** |
|       ImmutableDictionary |  256 |    98,115.5 ns |  1,927.59 ns |   2,885.13 ns |
|             ImmutableList |  256 |     9,896.6 ns |    191.96 ns |     249.60 ns |
|            ImmutableQueue |  256 |     3,533.4 ns |     68.37 ns |      91.27 ns |
|            ImmutableStack |  256 |     4,989.8 ns |     96.48 ns |     147.34 ns |
| ImmutableSortedDictionary |  256 |   226,338.2 ns |  4,481.29 ns |   8,080.69 ns |
|        ImmutableSortedSet |  256 |   177,442.8 ns |  2,971.44 ns |   2,779.49 ns |
|            LanguageExtArr |  256 |       191.8 ns |      3.50 ns |       3.10 ns |
|        LanguageExtHashMap |  256 |    66,645.6 ns |  1,271.11 ns |   1,248.40 ns |
|            LanguageExtLst |  256 |    32,168.1 ns |    510.31 ns |     426.14 ns |
|            LanguageExtQue |  256 |     5,894.5 ns |    101.95 ns |     109.08 ns |
|           LanguageExtStck |  256 |     4,120.4 ns |     82.26 ns |     130.47 ns |
|            LanguageExtMap |  256 |   158,800.9 ns |  2,755.24 ns |   2,442.45 ns |
|            LanguageExtSet |  256 |   172,628.1 ns |  3,323.43 ns |   4,081.47 ns |
|            **ImmutableArray** | **4096** |     **2,353.1 ns** |     **46.74 ns** |      **62.40 ns** |
|       ImmutableDictionary | 4096 | 1,962,078.9 ns | 37,230.62 ns |  34,825.54 ns |
|             ImmutableList | 4096 |   129,395.6 ns |  2,579.18 ns |   3,167.46 ns |
|            ImmutableQueue | 4096 |    45,559.1 ns |    888.11 ns |   1,273.70 ns |
|            ImmutableStack | 4096 |    61,718.1 ns |  1,063.71 ns |   1,306.33 ns |
| ImmutableSortedDictionary | 4096 | 4,337,863.7 ns | 83,043.45 ns |  92,302.58 ns |
|        ImmutableSortedSet | 4096 | 4,514,653.4 ns | 88,583.48 ns | 124,181.67 ns |
|            LanguageExtArr | 4096 |     2,558.0 ns |     50.74 ns |      65.98 ns |
|        LanguageExtHashMap | 4096 | 1,525,048.2 ns | 28,529.17 ns |  28,019.46 ns |
|            LanguageExtLst | 4096 |   703,255.9 ns | 13,651.49 ns |  17,264.77 ns |
|            LanguageExtQue | 4096 |   121,836.5 ns |  2,409.88 ns |   2,578.54 ns |
|           LanguageExtStck | 4096 |    66,604.9 ns |  1,248.79 ns |   1,107.02 ns |
|            LanguageExtMap | 4096 | 4,509,360.0 ns | 88,740.07 ns | 130,074.04 ns |
|            LanguageExtSet | 4096 | 4,596,392.5 ns | 87,471.73 ns | 100,732.57 ns |
