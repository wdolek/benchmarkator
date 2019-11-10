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
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT


```
|             Method | Length |     Mean |     Error |    StdDev |
|------------------- |------- |---------:|----------:|----------:|
| WhileLoopAccessPtr |   4096 | 1.083 us | 0.0057 us | 0.0051 us |
| WhileLoopAccessRef |   4096 | 1.086 us | 0.0082 us | 0.0077 us |
| ForLoopAccessIndex |   4096 | 1.935 us | 0.0111 us | 0.0098 us |
|   ForLoopAccessPtr |   4096 | 1.949 us | 0.0121 us | 0.0113 us |
|   ForLoopAccessRef |   4096 | 1.950 us | 0.0180 us | 0.0159 us |

## Iterating array field or local array

Comparison of iteration over local variable and field. Using field has negative impact,
because bound check is done for every access - you can't be sure if another thread hasn't
changed content of the field.

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT


```
|           Method | Length |     Mean |     Error |    StdDev |
|----------------- |------- |---------:|----------:|----------:|
| ForLoopOverLocal |   4096 | 1.915 us | 0.0055 us | 0.0048 us |
| ForLoopOverField |   4096 | 2.154 us | 0.0143 us | 0.0134 us |
