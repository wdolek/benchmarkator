## Comparison of `System.Collections.Immutable` with `LanguageExt`

Immutable collections shipped within [`corefx`](https://github.com/dotnet/corefx) are using binary tree,
whereas collections from [`LanguageExt.Core`](https://github.com/louthy/language-ext) are using various data structures, such as
[CHAMP](https://michael.steindorfer.name/publications/phd-thesis-efficient-immutable-collections.pdf)
or [AVL tree](http://en.wikipedia.org/wiki/AVL_tree).

(Note that `LanguageExt` collections are not drop-in replacement for `System.Collections.Immutable`)

Current benchmarks are _inspired_ by benchmarks from [dotnet/performance](https://github.com/dotnet/performance) (by @adamsitnik).

Related GitHub issues:

- dotnet/corefx#36406
- dotnet/corefx#36412
- ... umbrella ticket: dotnet/performance#93

### Contains(int) -> True

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Size |            Mean |         Error |        StdDev |
|------------------- |----- |----------------:|--------------:|--------------:|
|     **ImmutableArray** |  **512** |        **51.20 μs** |      **0.960 μs** |      **0.898 μs** |
|   ImmutableHashSet |  512 |        37.85 μs |      0.742 μs |      1.134 μs |
|      ImmutableList |  512 |       839.31 μs |     16.420 μs |     15.360 μs |
| ImmutableSortedSet |  512 |        40.84 μs |      0.783 μs |      0.933 μs |
|     LanguageExtArr |  512 |        61.42 μs |      1.222 μs |      1.407 μs |
| LanguageExtHashSet |  512 |        23.16 μs |      0.452 μs |      0.730 μs |
|     LanguageExtLst |  512 |     5,625.32 μs |    108.417 μs |    120.505 μs |
|     LanguageExtSet |  512 |        48.58 μs |      0.915 μs |      1.089 μs |
|     **ImmutableArray** | **8192** |    **12,318.98 μs** |    **243.918 μs** |    **400.765 μs** |
|   ImmutableHashSet | 8192 |     1,436.48 μs |     28.525 μs |     52.874 μs |
|      ImmutableList | 8192 |   324,520.76 μs |  6,119.356 μs | 14,895.358 μs |
| ImmutableSortedSet | 8192 |     1,523.09 μs |     29.715 μs |     57.957 μs |
|     LanguageExtArr | 8192 |    13,695.18 μs |    267.230 μs |    347.475 μs |
| LanguageExtHashSet | 8192 |       584.20 μs |     11.662 μs |     21.903 μs |
|     LanguageExtLst | 8192 | 1,757,870.75 μs | 34,618.554 μs | 45,013.899 μs |
|     LanguageExtSet | 8192 |     1,819.51 μs |     36.219 μs |     33.879 μs |


### Contains(string) -> True

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Size |            Mean |          Error |         StdDev |          Median |
|------------------- |----- |----------------:|---------------:|---------------:|----------------:|
|     **ImmutableArray** |  **512** |       **660.43 μs** |      **13.078 μs** |      **22.904 μs** |       **656.39 μs** |
|   ImmutableHashSet |  512 |        53.82 μs |       1.065 μs |       1.094 μs |        53.53 μs |
|      ImmutableList |  512 |     2,070.41 μs |      40.601 μs |      70.034 μs |     2,058.92 μs |
| ImmutableSortedSet |  512 |       423.99 μs |       8.253 μs |      10.437 μs |       420.89 μs |
|     LanguageExtArr |  512 |       557.97 μs |      11.110 μs |      16.629 μs |       557.05 μs |
| LanguageExtHashSet |  512 |        49.76 μs |       0.994 μs |       1.487 μs |        49.50 μs |
|     LanguageExtLst |  512 |     8,130.08 μs |     161.542 μs |     411.176 μs |     8,047.93 μs |
|     LanguageExtSet |  512 |       338.67 μs |       5.282 μs |       6.083 μs |       338.51 μs |
|     **ImmutableArray** | **8192** |   **142,462.92 μs** |   **2,834.883 μs** |   **7,317.739 μs** |   **141,681.14 μs** |
|   ImmutableHashSet | 8192 |     1,217.83 μs |      23.199 μs |      23.823 μs |     1,217.94 μs |
|      ImmutableList | 8192 |   427,170.00 μs |   8,505.315 μs |  13,734.499 μs |   425,143.80 μs |
| ImmutableSortedSet | 8192 |     8,039.55 μs |     155.895 μs |     202.707 μs |     8,036.60 μs |
|     LanguageExtArr | 8192 |   112,509.87 μs |   2,238.013 μs |   4,365.072 μs |   111,281.10 μs |
| LanguageExtHashSet | 8192 |       864.06 μs |      17.258 μs |      21.194 μs |       861.01 μs |
|     LanguageExtLst | 8192 | 2,865,025.37 μs | 151,401.715 μs | 444,035.022 μs | 3,061,044.90 μs |
|     LanguageExtSet | 8192 |    14,938.27 μs |     289.315 μs |     309.564 μs |    14,954.56 μs |


### Contains(int) -> False

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Size |            Mean |         Error |        StdDev |
|------------------- |----- |----------------:|--------------:|--------------:|
|     **ImmutableArray** |  **512** |        **99.61 μs** |      **1.208 μs** |      **1.009 μs** |
|   ImmutableHashSet |  512 |        33.69 μs |      0.628 μs |      0.587 μs |
|      ImmutableList |  512 |     1,582.02 μs |     30.330 μs |     33.712 μs |
| ImmutableSortedSet |  512 |        44.09 μs |      0.655 μs |      0.581 μs |
|     LanguageExtArr |  512 |       109.62 μs |      1.689 μs |      1.580 μs |
| LanguageExtHashSet |  512 |        11.58 μs |      0.195 μs |      0.172 μs |
|     LanguageExtLst |  512 |     7,844.53 μs |    152.636 μs |    181.702 μs |
|     LanguageExtSet |  512 |        45.23 μs |      0.823 μs |      0.770 μs |
|     **ImmutableArray** | **8192** |    **22,388.36 μs** |    **447.002 μs** |    **695.929 μs** |
|   ImmutableHashSet | 8192 |     1,040.40 μs |     17.636 μs |     22.304 μs |
|      ImmutableList | 8192 |   458,718.20 μs |  9,056.671 μs | 16,560.635 μs |
| ImmutableSortedSet | 8192 |       973.34 μs |     13.395 μs |     12.530 μs |
|     LanguageExtArr | 8192 |    19,970.24 μs |    351.175 μs |    456.627 μs |
| LanguageExtHashSet | 8192 |       211.70 μs |      3.935 μs |      6.787 μs |
|     LanguageExtLst | 8192 | 2,036,873.16 μs | 25,565.253 μs | 22,662.931 μs |
|     LanguageExtSet | 8192 |     1,303.66 μs |     20.373 μs |     19.057 μs |


### Contains(string) -> False

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Size |            Mean |          Error |         StdDev |
|------------------- |----- |----------------:|---------------:|---------------:|
|     **ImmutableArray** |  **512** |     **1,128.76 μs** |      **22.338 μs** |      **39.707 μs** |
|   ImmutableHashSet |  512 |        39.95 μs |       0.770 μs |       0.823 μs |
|      ImmutableList |  512 |     2,948.27 μs |      13.097 μs |      12.251 μs |
| ImmutableSortedSet |  512 |       376.80 μs |       6.189 μs |       5.168 μs |
|     LanguageExtArr |  512 |       809.89 μs |      12.328 μs |      11.532 μs |
| LanguageExtHashSet |  512 |        33.33 μs |       0.650 μs |       0.577 μs |
|     LanguageExtLst |  512 |    16,200.16 μs |     208.245 μs |     184.604 μs |
|     LanguageExtSet |  512 |       378.18 μs |       2.850 μs |       2.380 μs |
|     **ImmutableArray** | **8192** |   **240,823.91 μs** |   **3,643.846 μs** |   **3,408.456 μs** |
|   ImmutableHashSet | 8192 |     1,188.43 μs |      20.687 μs |      19.351 μs |
|      ImmutableList | 8192 |   799,904.57 μs |   9,718.442 μs |   9,090.637 μs |
| ImmutableSortedSet | 8192 |     8,654.37 μs |     138.001 μs |     129.086 μs |
|     LanguageExtArr | 8192 |   206,839.64 μs |   2,072.177 μs |   1,836.931 μs |
| LanguageExtHashSet | 8192 |       603.75 μs |       1.876 μs |       1.566 μs |
|     LanguageExtLst | 8192 | 6,551,654.96 μs | 188,778.026 μs | 523,103.022 μs |
|     LanguageExtSet | 8192 |    18,175.25 μs |     356.258 μs |     660.347 μs |


### ContainsKey(int) -> True

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |        Mean |     Error |    StdDev |
|-------------------------- |----- |------------:|----------:|----------:|
|       **ImmutableDictionary** |  **512** |    **25.73 μs** |  **0.492 μs** |  **0.604 μs** |
| ImmutableSortedDictionary |  512 |    35.81 μs |  0.399 μs |  0.373 μs |
|        LanguageExtHashMap |  512 |    17.59 μs |  0.345 μs |  0.397 μs |
|            LanguageExtMap |  512 |    53.67 μs |  1.048 μs |  0.981 μs |
|       **ImmutableDictionary** | **8192** | **1,325.89 μs** | **26.278 μs** | **41.679 μs** |
| ImmutableSortedDictionary | 8192 | 1,553.45 μs | 31.024 μs | 49.208 μs |
|        LanguageExtHashMap | 8192 |   638.00 μs | 12.695 μs | 21.557 μs |
|            LanguageExtMap | 8192 | 1,896.53 μs | 36.665 μs | 51.399 μs |


### ContainsKey(string) -> True

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |         Mean |      Error |     StdDev |
|-------------------------- |----- |-------------:|-----------:|-----------:|
|       **ImmutableDictionary** |  **512** |     **47.94 μs** |   **0.936 μs** |   **1.184 μs** |
| ImmutableSortedDictionary |  512 |    388.83 μs |   7.554 μs |   8.699 μs |
|        LanguageExtHashMap |  512 |     61.12 μs |   1.222 μs |   1.713 μs |
|            LanguageExtMap |  512 |    459.87 μs |   6.052 μs |   5.365 μs |
|       **ImmutableDictionary** | **8192** |  **1,820.66 μs** |  **25.277 μs** |  **21.108 μs** |
| ImmutableSortedDictionary | 8192 | 13,193.69 μs | 260.775 μs | 279.027 μs |
|        LanguageExtHashMap | 8192 |  1,536.55 μs |  29.858 μs |  43.766 μs |
|            LanguageExtMap | 8192 | 15,744.52 μs | 314.220 μs | 430.108 μs |


### ContainsKey(int) -> False

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |        Mean |     Error |    StdDev |
|-------------------------- |----- |------------:|----------:|----------:|
|       **ImmutableDictionary** |  **512** |    **26.99 μs** |  **0.533 μs** |  **0.547 μs** |
| ImmutableSortedDictionary |  512 |    49.51 μs |  0.824 μs |  0.770 μs |
|        LanguageExtHashMap |  512 |    16.16 μs |  0.308 μs |  0.354 μs |
|            LanguageExtMap |  512 |    42.66 μs |  0.789 μs |  0.699 μs |
|       **ImmutableDictionary** | **8192** |   **921.83 μs** |  **2.213 μs** |  **1.728 μs** |
| ImmutableSortedDictionary | 8192 | 1,000.05 μs | 19.494 μs | 20.019 μs |
|        LanguageExtHashMap | 8192 |   229.20 μs |  1.055 μs |  0.987 μs |
|            LanguageExtMap | 8192 | 1,233.22 μs | 22.016 μs | 17.188 μs |

### ContainsKey(string) -> False

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                    Method | Size |        Mean |      Error |     StdDev |
|-------------------------- |----- |------------:|-----------:|-----------:|
|       **ImmutableDictionary** |  **512** |    **34.72 μs** |   **0.228 μs** |   **0.190 μs** |
| ImmutableSortedDictionary |  512 |   353.37 μs |   1.249 μs |   1.107 μs |
|        LanguageExtHashMap |  512 |    34.42 μs |   0.198 μs |   0.155 μs |
|            LanguageExtMap |  512 |   385.02 μs |   4.997 μs |   4.674 μs |
|       **ImmutableDictionary** | **8192** | **1,178.97 μs** |   **8.741 μs** |   **7.748 μs** |
| ImmutableSortedDictionary | 8192 | 9,002.08 μs | 166.649 μs | 147.730 μs |
|        LanguageExtHashMap | 8192 |   608.82 μs |   5.446 μs |   5.094 μs |
|            LanguageExtMap | 8192 | 9,668.40 μs | 133.036 μs | 124.442 μs |
