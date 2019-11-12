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
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Size |            Mean |        Error |       StdDev |
|------------------- |----- |----------------:|-------------:|-------------:|
|     **ImmutableArray** |  **512** |        **34.40 us** |     **0.047 us** |     **0.042 us** |
|   ImmutableHashSet |  512 |        25.75 us |     0.137 us |     0.128 us |
|      ImmutableList |  512 |     6,791.78 us |    10.595 us |     8.272 us |
| ImmutableSortedSet |  512 |        28.24 us |     0.157 us |     0.139 us |
|     LanguageExtArr |  512 |        34.62 us |     0.103 us |     0.086 us |
| LanguageExtHashSet |  512 |        15.89 us |     0.051 us |     0.045 us |
|     LanguageExtLst |  512 |     2,857.56 us |    29.670 us |    24.776 us |
|     LanguageExtSet |  512 |        32.76 us |     0.110 us |     0.103 us |
|     **ImmutableArray** | **8192** |     **7,300.69 us** |    **41.225 us** |    **38.562 us** |
|   ImmutableHashSet | 8192 |       756.69 us |     1.349 us |     1.127 us |
|      ImmutableList | 8192 | 1,763,189.30 us | 3,314.012 us | 2,937.785 us |
| ImmutableSortedSet | 8192 |       786.43 us |     2.250 us |     1.879 us |
|     LanguageExtArr | 8192 |     7,288.30 us |    12.842 us |    10.026 us |
| LanguageExtHashSet | 8192 |       334.92 us |     0.862 us |     0.720 us |
|     LanguageExtLst | 8192 |   776,261.88 us | 3,376.369 us | 2,993.063 us |
|     LanguageExtSet | 8192 |       928.60 us |     6.067 us |     5.675 us |

### Contains(string) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Size |            Mean |        Error |       StdDev |
|------------------- |----- |----------------:|-------------:|-------------:|
|     **ImmutableArray** |  **512** |       **463.10 us** |     **1.021 us** |     **0.853 us** |
|   ImmutableHashSet |  512 |        44.19 us |     0.482 us |     0.402 us |
|      ImmutableList |  512 |     9,539.49 us |    53.183 us |    47.145 us |
| ImmutableSortedSet |  512 |       449.99 us |     0.636 us |     0.531 us |
|     LanguageExtArr |  512 |       399.72 us |     2.571 us |     2.405 us |
| LanguageExtHashSet |  512 |        65.05 us |     0.089 us |     0.074 us |
|     LanguageExtLst |  512 |     8,949.75 us |    35.658 us |    33.355 us |
|     LanguageExtSet |  512 |       534.08 us |     1.000 us |     0.887 us |
|     **ImmutableArray** | **8192** |   **108,866.76 us** |    **81.914 us** |    **72.614 us** |
|   ImmutableHashSet | 8192 |     1,160.26 us |     9.485 us |     7.405 us |
|      ImmutableList | 8192 | 2,336,548.18 us | 6,113.866 us | 5,105.355 us |
| ImmutableSortedSet | 8192 |    12,845.76 us |    17.299 us |    16.181 us |
|     LanguageExtArr | 8192 |   106,467.33 us |   149.905 us |   140.221 us |
| LanguageExtHashSet | 8192 |     1,136.31 us |     2.994 us |     2.800 us |
|     LanguageExtLst | 8192 | 2,400,869.04 us | 6,938.436 us | 6,150.743 us |
|     LanguageExtSet | 8192 |    14,140.03 us |    17.065 us |    14.250 us |

### Contains(int) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Size |             Mean |         Error |        StdDev |
|------------------- |----- |-----------------:|--------------:|--------------:|
|     **ImmutableArray** |  **512** |        **59.461 us** |     **0.2505 us** |     **0.2343 us** |
|   ImmutableHashSet |  512 |        24.637 us |     0.1874 us |     0.1661 us |
|      ImmutableList |  512 |    13,490.544 us |    30.6284 us |    27.1513 us |
| ImmutableSortedSet |  512 |        33.400 us |     0.0690 us |     0.0612 us |
|     LanguageExtArr |  512 |        61.188 us |     0.1335 us |     0.1183 us |
| LanguageExtHashSet |  512 |         9.783 us |     0.0345 us |     0.0323 us |
|     LanguageExtLst |  512 |     5,951.940 us |    21.3980 us |    18.9688 us |
|     LanguageExtSet |  512 |        39.400 us |     0.0798 us |     0.0707 us |
|     **ImmutableArray** | **8192** |    **14,988.127 us** |    **20.9624 us** |    **17.5046 us** |
|   ImmutableHashSet | 8192 |       941.451 us |     3.1075 us |     2.5949 us |
|      ImmutableList | 8192 | 3,573,116.408 us | 3,121.4879 us | 2,437.0524 us |
| ImmutableSortedSet | 8192 |       939.917 us |     5.3139 us |     4.9707 us |
|     LanguageExtArr | 8192 |    14,446.703 us |    30.3090 us |    25.3094 us |
| LanguageExtHashSet | 8192 |       210.347 us |     0.3162 us |     0.2468 us |
|     LanguageExtLst | 8192 | 1,501,219.307 us | 5,478.3111 us | 4,856.3801 us |
|     LanguageExtSet | 8192 |     1,098.767 us |     3.2015 us |     2.9947 us |

### Contains(string) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Size |            Mean |         Error |        StdDev |          Median |
|------------------- |----- |----------------:|--------------:|--------------:|----------------:|
|     **ImmutableArray** |  **512** |       **850.68 us** |      **0.985 us** |      **0.873 us** |       **850.70 us** |
|   ImmutableHashSet |  512 |        43.42 us |      0.086 us |      0.076 us |        43.41 us |
|      ImmutableList |  512 |    18,683.24 us |     50.421 us |     39.366 us |    18,672.75 us |
| ImmutableSortedSet |  512 |       601.82 us |      0.561 us |      0.469 us |       601.68 us |
|     LanguageExtArr |  512 |       756.32 us |      1.011 us |      0.844 us |       756.18 us |
| LanguageExtHashSet |  512 |        49.54 us |      0.337 us |      0.282 us |        49.42 us |
|     LanguageExtLst |  512 |    18,524.95 us |    361.905 us |    495.379 us |    18,814.76 us |
|     LanguageExtSet |  512 |       653.71 us |      1.780 us |      1.578 us |       653.26 us |
|     **ImmutableArray** | **8192** |   **246,900.88 us** |    **704.392 us** |    **549.943 us** |   **246,840.37 us** |
|   ImmutableHashSet | 8192 |     1,253.96 us |      2.529 us |      2.242 us |     1,253.50 us |
|      ImmutableList | 8192 | 4,811,899.68 us |  3,035.293 us |  2,369.757 us | 4,811,431.50 us |
| ImmutableSortedSet | 8192 |    15,541.33 us |    108.964 us |     90.990 us |    15,502.79 us |
|     LanguageExtArr | 8192 |   196,285.47 us |    344.642 us |    305.516 us |   196,285.85 us |
| LanguageExtHashSet | 8192 |       901.55 us |      6.693 us |      6.261 us |       898.38 us |
|     LanguageExtLst | 8192 | 4,875,448.75 us | 21,997.618 us | 18,369.007 us | 4,880,213.40 us |
|     LanguageExtSet | 8192 |    17,229.63 us |     52.443 us |     49.055 us |    17,231.51 us |

### ContainsKey(int) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |      Mean |    Error |   StdDev |
|-------------------------- |----- |----------:|---------:|---------:|
|       **ImmutableDictionary** |  **512** |  **22.76 us** | **0.043 us** | **0.038 us** |
| ImmutableSortedDictionary |  512 |  28.74 us | 0.062 us | 0.052 us |
|        LanguageExtHashMap |  512 |  15.70 us | 0.046 us | 0.041 us |
|            LanguageExtMap |  512 |  33.43 us | 0.112 us | 0.105 us |
|       **ImmutableDictionary** | **8192** | **717.86 us** | **3.022 us** | **2.523 us** |
| ImmutableSortedDictionary | 8192 | 845.53 us | 5.086 us | 4.758 us |
|        LanguageExtHashMap | 8192 | 316.63 us | 1.072 us | 0.950 us |
|            LanguageExtMap | 8192 | 954.02 us | 1.540 us | 1.203 us |

### ContainsKey(string) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |         Mean |     Error |    StdDev |
|-------------------------- |----- |-------------:|----------:|----------:|
|       **ImmutableDictionary** |  **512** |     **39.77 us** |  **0.137 us** |  **0.114 us** |
| ImmutableSortedDictionary |  512 |    458.30 us |  0.561 us |  0.438 us |
|        LanguageExtHashMap |  512 |     54.44 us |  0.104 us |  0.081 us |
|            LanguageExtMap |  512 |    537.64 us |  1.163 us |  1.031 us |
|       **ImmutableDictionary** | **8192** |  **1,033.02 us** |  **2.137 us** |  **1.999 us** |
| ImmutableSortedDictionary | 8192 | 12,791.34 us | 96.548 us | 80.622 us |
|        LanguageExtHashMap | 8192 |    963.12 us |  4.598 us |  4.076 us |
|            LanguageExtMap | 8192 | 14,937.52 us | 15.695 us | 13.106 us |

### ContainsKey(int) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |        Mean |    Error |   StdDev |
|-------------------------- |----- |------------:|---------:|---------:|
|       **ImmutableDictionary** |  **512** |    **19.23 us** | **0.105 us** | **0.099 us** |
| ImmutableSortedDictionary |  512 |    32.17 us | 0.070 us | 0.062 us |
|        LanguageExtHashMap |  512 |    10.14 us | 0.349 us | 0.402 us |
|            LanguageExtMap |  512 |    39.24 us | 0.190 us | 0.177 us |
|       **ImmutableDictionary** | **8192** |   **875.00 us** | **5.003 us** | **4.435 us** |
| ImmutableSortedDictionary | 8192 |   889.24 us | 4.919 us | 4.602 us |
|        LanguageExtHashMap | 8192 |   211.27 us | 2.067 us | 1.832 us |
|            LanguageExtMap | 8192 | 1,196.19 us | 3.216 us | 2.851 us |

### ContainsKey(string) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|                    Method | Size |         Mean |     Error |    StdDev |
|-------------------------- |----- |-------------:|----------:|----------:|
|       **ImmutableDictionary** |  **512** |     **34.46 us** |  **0.137 us** |  **0.122 us** |
| ImmutableSortedDictionary |  512 |    600.96 us |  0.977 us |  0.914 us |
|        LanguageExtHashMap |  512 |     42.97 us |  0.066 us |  0.055 us |
|            LanguageExtMap |  512 |    665.18 us |  1.221 us |  1.083 us |
|       **ImmutableDictionary** | **8192** |  **1,154.18 us** |  **3.436 us** |  **3.046 us** |
| ImmutableSortedDictionary | 8192 | 15,455.11 us | 37.898 us | 31.647 us |
|        LanguageExtHashMap | 8192 |    739.90 us |  3.308 us |  2.932 us |
|            LanguageExtMap | 8192 | 17,323.50 us | 60.380 us | 53.526 us |
