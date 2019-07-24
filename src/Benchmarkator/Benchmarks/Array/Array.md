## Accessing array

### Iterating over and accessing array

This benchmarks tries to find the fastest way to access item of given array.
Several approaches are tested:

- getting value using array access operator, indexer (`[]`)
- accessing value using pointer (`unsafe`)
- accessing value using reference (`ref`)

Besides simple iteration, other approaches than basic `for` loop are tested.
For details, see [ArrayIteration.cs](./ArrayIteration.cs).

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|             Method | Length |     Mean |     Error |    StdDev |
|------------------- |------- |---------:|----------:|----------:|
| WhileLoopAccessRef |   4096 | 1.088 us | 0.0047 us | 0.0044 us |
| WhileLoopAccessPtr |   4096 | 1.088 us | 0.0042 us | 0.0037 us |
|   ForLoopAccessPtr |   4096 | 1.126 us | 0.0135 us | 0.0106 us |
| ForLoopAccessIndex |   4096 | 1.874 us | 0.0036 us | 0.0034 us |
|   ForLoopAccessRef |   4096 | 1.933 us | 0.0041 us | 0.0034 us |
