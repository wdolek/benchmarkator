## Getting name of `enum` value

Is `ToString()` causing allocation (boxing)? Is it better to use `Enum.GetName<T>(T)` instead?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT


```
|       Method |     Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Allocated |
|------------- |---------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|
| EnumToString | 1.493 μs | 0.0296 μs | 0.0291 μs |  1.00 |    0.00 | 0.2861 | 0.0019 |   1,201 B |
|  GetEnumName | 1.643 μs | 0.0289 μs | 0.0271 μs |  1.10 |    0.03 |      - |      - |         - |
