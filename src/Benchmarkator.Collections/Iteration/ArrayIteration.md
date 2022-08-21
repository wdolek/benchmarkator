## Iterating over array with item access

This benchmarks tries to find the fastest way to access item of given array.
Several approaches are tested:

- getting value using array access operator, indexer (`[]`)
- accessing value using pointer (`unsafe`)
- accessing value using reference (`ref`)

Besides simple iteration, other approaches than basic `for` loop are tested.
For details, see [ArrayIteration.cs](./ArrayIteration.cs).

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | Length |     Mean |     Error |    StdDev |
|------------------- |------- |---------:|----------:|----------:|
| WhileLoopAccessPtr |   4096 | 1.207 μs | 0.0237 μs | 0.0333 μs |
| WhileLoopAccessRef |   4096 | 1.215 μs | 0.0243 μs | 0.0325 μs |
|   ForLoopAccessRef |   4096 | 2.010 μs | 0.0227 μs | 0.0212 μs |
| ForLoopAccessIndex |   4096 | 2.103 μs | 0.0292 μs | 0.0273 μs |
|   ForLoopAccessPtr |   4096 | 2.115 μs | 0.0324 μs | 0.0304 μs |
