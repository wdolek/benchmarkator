## Iterating enumerable

When enumerating `IEnumerable`, performance can actually differ based on implementation:

- yielded enumerable
- `Enumerable.Range` enumerable
- array iteration

|          Method |      Mean |     Error |    StdDev |    Median |
|---------------- |----------:|----------:|----------:|----------:|
| YieldEnumerable | 53.532 μs | 1.8368 μs | 4.9972 μs | 51.627 μs |
| RangeEnumerable | 47.877 μs | 0.8422 μs | 0.9362 μs | 47.611 μs |
| ArrayEnumerable |  3.273 μs | 0.0650 μs | 0.1467 μs |  3.248 μs |
