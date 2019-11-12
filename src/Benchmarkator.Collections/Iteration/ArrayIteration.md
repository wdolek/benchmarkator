## Iterating over array with item access

This benchmarks tries to find the fastest way to access item of given array.
Several approaches are tested:

- getting value using array access operator, indexer (`[]`)
- accessing value using pointer (`unsafe`)
- accessing value using reference (`ref`)

Besides simple iteration, other approaches than basic `for` loop are tested.
For details, see [ArrayIteration.cs](./ArrayIteration.cs).

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|             Method | Length |     Mean |     Error |    StdDev |
|------------------- |------- |---------:|----------:|----------:|
| WhileLoopAccessPtr |   4096 | 1.090 us | 0.0017 us | 0.0015 us |
|   ForLoopAccessPtr |   4096 | 1.963 us | 0.0027 us | 0.0025 us |
| ForLoopAccessIndex |   4096 | 1.963 us | 0.0080 us | 0.0067 us |
|   ForLoopAccessRef |   4096 | 1.965 us | 0.0034 us | 0.0029 us |
| WhileLoopAccessRef |   4096 | 2.167 us | 0.0066 us | 0.0061 us |

## Iterating array field or local array

Comparison of iteration over local variable and field. Using field has negative impact,
because bound check is done for every access - you can't be sure if another thread hasn't
changed content of the field.

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|           Method | Length |     Mean |     Error |    StdDev |
|----------------- |------- |---------:|----------:|----------:|
| ForLoopOverField |   4096 | 1.895 us | 0.0041 us | 0.0034 us |
| ForLoopOverLocal |   4096 | 1.958 us | 0.0022 us | 0.0017 us |
