## Bitmap: is bit set?

What's the best data structure to keep track of ints? Is `HashSet<int>` the best option? Let's see!

Following data structures are used:

- [`BitArray`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.bitarray?view=netcore-2.2)
- [`Dictionary<int,bool>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=netcore-2.2)
- [`HashSet<int>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netcore-2.2)

Benchmark measures access to bits.

### Whole bitmap set, access all items in sequence

In this scenario, whole data structure is filled as set (all bits set),
then all items are accessed to get value. See [BitmapSequentialContainsTrue](./BitmapSequentialContainsTrue.cs) for details.

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Length |        Mean |     Error |    StdDev |
|------------------- |------- |------------:|----------:|----------:|
|   BitArrayContains |     32 |    31.47 ns |  0.123 ns |  0.102 ns |
| DictionaryContains |     32 |   243.68 ns |  1.732 ns |  1.536 ns |
|        SetContains |     32 |   286.01 ns |  0.525 ns |  0.439 ns |
|   BitArrayContains |   1024 | 1,023.04 ns |  5.720 ns |  4.466 ns |
| DictionaryContains |   1024 | 7,596.67 ns | 63.255 ns | 59.169 ns |
|        SetContains |   1024 | 9,692.24 ns | 28.163 ns | 26.344 ns |

### Randomly generated bitmap, random access

In this scenario, bitmap is generated and accessed randomly - not all bits are set, not all bits are read.
See [BitmapRandomContains](./BitmapRandomContains.cs) for more details.

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Length |         Mean |      Error |    StdDev |
|------------------- |------- |-------------:|-----------:|----------:|
|   BitArrayContains |     32 |     38.79 ns |   2.330 ns |  2.947 ns |
| DictionaryContains |     32 |    186.07 ns |   1.500 ns |  1.171 ns |
|        SetContains |     32 |    267.01 ns |   2.234 ns |  1.866 ns |
|   BitArrayContains |   1024 |  1,161.31 ns |   3.080 ns |  2.881 ns |
| DictionaryContains |   1024 |  6,896.75 ns | 117.314 ns | 97.962 ns |
|        SetContains |   1024 | 13,708.36 ns |  80.975 ns | 71.782 ns |
