## Bitmap: is bit set?

What's the best data structure to keep track of ints? Is `HashSet<int>` the best option? Let's see!

Following data structures are used:

- [`BitArray`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.bitarray?view=netcore-2.2)
- [`Dictionary<int,bool>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=netcore-2.2)
- [`HashSet<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netcore-2.2)

Benchmark measures access to bits.

### Whole bitmap set, access all items in sequence

In this scenario, whole data strucutre is filled as set (all bits set),
then all items are accessed to get value. See [BitmapSequentialContainsTrue](./BitmapSequentialContainsTrue.cs) for details.

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|             Method | Length |       Mean |      Error |     StdDev |
|------------------- |------- |-----------:|-----------:|-----------:|
|   BitArrayContains |     32 |   103.9 ns |  0.2558 ns |  0.2392 ns |
| DictionaryContains |     32 |   234.1 ns |  1.0526 ns |  0.9846 ns |
|        SetContains |     32 |   293.3 ns |  0.8027 ns |  0.7116 ns |
|   BitArrayContains |   1024 | 3,200.5 ns |  6.7429 ns |  5.6306 ns |
| DictionaryContains |   1024 | 9,056.4 ns | 21.3214 ns | 19.9440 ns |
|        SetContains |   1024 | 9,161.6 ns | 36.7346 ns | 34.3616 ns |


### Randomly generated bitmap, random access

In this scenario, bitmap is generated and accessed randomly - not all bits are set, not all bits are read.
See [BitmapRandomContains](./BitmapRandomContains.cs) for more details.

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|             Method | Length |        Mean |      Error |     StdDev |
|------------------- |------- |------------:|-----------:|-----------:|
|   BitArrayContains |     32 |    102.0 ns |  0.4786 ns |  0.4243 ns |
| DictionaryContains |     32 |    174.1 ns |  0.6344 ns |  0.5624 ns |
|        SetContains |     32 |    274.7 ns |  1.6816 ns |  1.5729 ns |
|   BitArrayContains |   1024 |  3,204.0 ns |  8.0440 ns |  7.1308 ns |
| DictionaryContains |   1024 |  8,355.0 ns | 63.3567 ns | 59.2639 ns |
|        SetContains |   1024 | 13,367.4 ns | 59.2698 ns | 55.4410 ns |
