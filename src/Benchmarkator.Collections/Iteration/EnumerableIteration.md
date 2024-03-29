## Iterating enumerable

When enumerating `IEnumerable`, performance can actually differ based on implementation:

- yielded enumerable
- `Enumerable.Range` enumerable
- array iteration

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|          Method |     Mean |   Error |  StdDev |
|---------------- |---------:|--------:|--------:|
| YieldEnumerable | 459.2 μs | 7.00 μs | 6.20 μs |
| RangeEnumerable | 432.0 μs | 8.37 μs | 9.96 μs |
| ArrayEnumerable | 438.2 μs | 8.35 μs | 8.94 μs |
