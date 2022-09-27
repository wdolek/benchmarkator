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
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Length |        Mean |      Error |     StdDev |
|------------------- |------- |------------:|-----------:|-----------:|
|   BitArrayContains |     32 |    32.48 ns |   0.504 ns |   0.421 ns |
|        SetContains |     32 |   181.41 ns |   3.169 ns |   2.965 ns |
| DictionaryContains |     32 |   208.84 ns |   4.052 ns |   3.790 ns |
|   BitArrayContains |   1024 | 1,180.00 ns |  37.496 ns |  98.780 ns |
|        SetContains |   1024 | 7,351.39 ns | 142.542 ns | 133.334 ns |
| DictionaryContains |   1024 | 8,574.08 ns | 154.863 ns | 152.096 ns |

### Randomly generated bitmap, random access

In this scenario, bitmap is generated and accessed randomly - not all bits are set, not all bits are read.
See [BitmapRandomContains](./BitmapRandomContains.cs) for more details.

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Length |        Mean |     Error |    StdDev |
|------------------- |------- |------------:|----------:|----------:|
|   BitArrayContains |     32 |    38.98 ns |  0.484 ns |  0.404 ns |
| DictionaryContains |     32 |   185.53 ns |  2.684 ns |  2.241 ns |
|        SetContains |     32 |   194.28 ns |  3.496 ns |  3.271 ns |
|   BitArrayContains |   1024 | 1,200.61 ns | 14.300 ns | 12.677 ns |
|        SetContains |   1024 | 6,693.49 ns | 70.844 ns | 66.268 ns |
| DictionaryContains |   1024 | 7,328.08 ns | 98.860 ns | 92.473 ns |
