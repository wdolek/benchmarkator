## From `IEnumerable` or `ICollection` to `List` or `Array`

For context, see @dustinmoris [Tweet about `IEnumerable`](https://twitter.com/dustinmoris/status/1490606359183769604).

### Enumerable to ...

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|            Method | NumOfItems |        Mean |       Error |      StdDev | Ratio | RatioSD |
|------------------ |----------- |------------:|------------:|------------:|------:|--------:|
|  **EnumerableToList** |         **10** |    **119.9 ns** |     **1.54 ns** |     **1.37 ns** |  **1.00** |    **0.00** |
| EnumerableToArray |         10 |    151.2 ns |     2.27 ns |     2.01 ns |  1.26 |    0.02 |
|                   |            |             |             |             |       |         |
|  **EnumerableToList** |        **100** |    **717.5 ns** |    **14.36 ns** |    **14.10 ns** |  **1.00** |    **0.00** |
| EnumerableToArray |        100 |    773.1 ns |    12.49 ns |     9.75 ns |  1.08 |    0.02 |
|                   |            |             |             |             |       |         |
|  **EnumerableToList** |       **1000** |  **5,786.8 ns** |   **102.44 ns** |    **85.54 ns** |  **1.00** |    **0.00** |
| EnumerableToArray |       1000 |  5,818.2 ns |   114.94 ns |   118.04 ns |  1.01 |    0.03 |
|                   |            |             |             |             |       |         |
|  **EnumerableToList** |      **10000** | **56,798.2 ns** |   **592.17 ns** |   **494.49 ns** |  **1.00** |    **0.00** |
| EnumerableToArray |      10000 | 55,067.1 ns | 1,083.96 ns | 1,248.29 ns |  0.97 |    0.02 |


### Collection to ...

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|            Method | NumOfItems |        Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Allocated |
|------------------ |----------- |------------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|
|  **CollectionToList** |         **10** |    **28.42 ns** |  **0.312 ns** |  **0.261 ns** |  **1.00** |    **0.00** | **0.0229** |      **-** |      **96 B** |
| CollectionToArray |         10 |    11.00 ns |  0.133 ns |  0.118 ns |  0.39 |    0.01 | 0.0153 |      - |      64 B |
|                   |            |             |           |           |       |         |        |        |           |
|  **CollectionToList** |        **100** |    **49.41 ns** |  **0.906 ns** |  **0.757 ns** |  **1.00** |    **0.00** | **0.1090** |      **-** |     **456 B** |
| CollectionToArray |        100 |    32.04 ns |  0.688 ns |  0.644 ns |  0.65 |    0.02 | 0.1013 |      - |     424 B |
|                   |            |             |           |           |       |         |        |        |           |
|  **CollectionToList** |       **1000** |   **250.07 ns** |  **2.341 ns** |  **2.075 ns** |  **1.00** |    **0.00** | **0.9689** |      **-** |   **4,056 B** |
| CollectionToArray |       1000 |   229.58 ns |  4.663 ns |  5.727 ns |  0.91 |    0.03 | 0.9613 |      - |   4,024 B |
|                   |            |             |           |           |       |         |        |        |           |
|  **CollectionToList** |      **10000** | **2,477.78 ns** | **44.211 ns** | **57.487 ns** |  **1.00** |    **0.00** | **9.5215** | **1.9035** |  **40,056 B** |
| CollectionToArray |      10000 | 2,444.89 ns | 37.574 ns | 33.309 ns |  0.99 |    0.02 | 9.5215 | 1.1902 |  40,024 B |
