## Iterating enumerable

When enumerating `IEnumerable`, performance can actually differ based on implementation:

- yielded enumerable
- `Enumerable.Range` enumerable
- array iteration

|          Method |     Mean |    Error |   StdDev |   Median |
|---------------- |---------:|---------:|---------:|---------:|
| YieldEnumerable | 557.9 μs | 22.53 μs | 63.56 μs | 529.8 μs |
| RangeEnumerable | 541.9 μs | 21.01 μs | 60.62 μs | 528.6 μs |
| ArrayEnumerable | 537.6 μs | 24.27 μs | 68.45 μs | 505.1 μs |
