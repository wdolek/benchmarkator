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
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|             Method | Size |        Mean |     Error |    StdDev |
|------------------- |----- |------------:|----------:|----------:|
|     ImmutableArray |  512 |    33.95 us |  0.307 us |  0.287 us |
|   ImmutableHashSet |  512 |    25.34 us |  0.132 us |  0.123 us |
|      ImmutableList |  512 | 6,705.91 us | 24.236 us | 22.670 us |
| ImmutableSortedSet |  512 |    27.80 us |  0.168 us |  0.157 us |
|     LanguageExtArr |  512 |    34.50 us |  0.245 us |  0.230 us |
| LanguageExtHashSet |  512 |    15.66 us |  0.140 us |  0.131 us |
|     LanguageExtLst |  512 | 3,007.95 us |  9.516 us |  7.946 us |
|     LanguageExtSet |  512 |    33.33 us |  0.103 us |  0.096 us |


### Contains(string) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|             Method | Size |        Mean |      Error |     StdDev |
|------------------- |----- |------------:|-----------:|-----------:|
|     ImmutableArray |  512 |   422.43 us |   1.865 us |   1.653 us |
|   ImmutableHashSet |  512 |    43.66 us |   0.366 us |   0.343 us |
|      ImmutableList |  512 | 9,417.70 us |  93.735 us |  87.680 us |
| ImmutableSortedSet |  512 |   470.56 us |   1.775 us |   1.574 us |
|     LanguageExtArr |  512 |   391.41 us |   2.199 us |   2.057 us |
| LanguageExtHashSet |  512 |    66.98 us |   0.712 us |   0.666 us |
|     LanguageExtLst |  512 | 8,734.51 us | 111.713 us | 104.497 us |
|     LanguageExtSet |  512 |   519.18 us |   6.151 us |   5.453 us |

### Contains(int) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|             Method | Size |          Mean |       Error |     StdDev |
|------------------- |----- |--------------:|------------:|-----------:|
|     ImmutableArray |  512 |     60.876 us |   0.3811 us |  0.3565 us |
|   ImmutableHashSet |  512 |     23.990 us |   0.1315 us |  0.1230 us |
|      ImmutableList |  512 | 13,445.844 us | 103.0165 us | 96.3617 us |
| ImmutableSortedSet |  512 |     34.503 us |   0.2570 us |  0.2404 us |
|     LanguageExtArr |  512 |     60.614 us |   0.3368 us |  0.3151 us |
| LanguageExtHashSet |  512 |      9.718 us |   0.0607 us |  0.0568 us |
|     LanguageExtLst |  512 |  5,882.961 us |  46.9845 us | 43.9493 us |
|     LanguageExtSet |  512 |     38.799 us |   0.1308 us |  0.1159 us |

### Contains(string) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|             Method | Size |         Mean |      Error |     StdDev |
|------------------- |----- |-------------:|-----------:|-----------:|
|     ImmutableArray |  512 |    865.47 us |  16.317 us |  16.756 us |
|   ImmutableHashSet |  512 |     41.66 us |   0.299 us |   0.279 us |
|      ImmutableList |  512 | 18,802.12 us | 164.909 us | 146.187 us |
| ImmutableSortedSet |  512 |    583.61 us |   5.064 us |   4.489 us |
|     LanguageExtArr |  512 |    790.64 us |   8.317 us |   7.780 us |
| LanguageExtHashSet |  512 |     50.03 us |   0.386 us |   0.361 us |
|     LanguageExtLst |  512 | 18,432.33 us | 224.400 us | 187.384 us |
|     LanguageExtSet |  512 |    646.19 us |   4.374 us |   4.092 us |

### ContainsKey(int) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|                    Method | Size |     Mean |    Error |   StdDev |
|-------------------------- |----- |---------:|---------:|---------:|
|       ImmutableDictionary |  512 | 22.73 us | 0.141 us | 0.132 us |
| ImmutableSortedDictionary |  512 | 29.95 us | 0.159 us | 0.141 us |
|        LanguageExtHashMap |  512 | 15.51 us | 0.143 us | 0.134 us |
|            LanguageExtMap |  512 | 32.87 us | 0.225 us | 0.210 us |


### ContainsKey(string) -> True

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|                    Method | Size |      Mean |    Error |   StdDev |
|-------------------------- |----- |----------:|---------:|---------:|
|       ImmutableDictionary |  512 |  38.86 us | 0.311 us | 0.291 us |
| ImmutableSortedDictionary |  512 | 439.81 us | 1.668 us | 1.560 us |
|        LanguageExtHashMap |  512 |  54.49 us | 0.198 us | 0.154 us |
|            LanguageExtMap |  512 | 541.97 us | 4.076 us | 3.813 us |


### ContainsKey(int) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|                    Method | Size |      Mean |     Error |    StdDev |
|-------------------------- |----- |----------:|----------:|----------:|
|       ImmutableDictionary |  512 | 17.543 us | 0.1377 us | 0.1288 us |
| ImmutableSortedDictionary |  512 | 32.716 us | 0.2188 us | 0.2047 us |
|        LanguageExtHashMap |  512 |  9.993 us | 0.0755 us | 0.0706 us |
|            LanguageExtMap |  512 | 38.594 us | 0.2381 us | 0.2227 us |


### ContainsKey(string) -> False

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

```
|                    Method | Size |      Mean |    Error |   StdDev |
|-------------------------- |----- |----------:|---------:|---------:|
|       ImmutableDictionary |  512 |  32.28 us | 0.296 us | 0.277 us |
| ImmutableSortedDictionary |  512 | 584.02 us | 5.940 us | 4.960 us |
|        LanguageExtHashMap |  512 |  43.57 us | 0.371 us | 0.347 us |
|            LanguageExtMap |  512 | 663.43 us | 5.577 us | 4.944 us |
