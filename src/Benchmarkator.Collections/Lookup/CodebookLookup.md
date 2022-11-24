## Code book lookup

There's short code book (4 string entries only) and you want to find record in it.

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT


```
|             Method |     Mean |   Error |  StdDev |  Gen 0 | Allocated |
|------------------- |---------:|--------:|--------:|-------:|----------:|
| IfStringComparison | 119.0 ns | 1.46 ns | 1.29 ns |      - |         - |
|   DictionaryLookup | 139.9 ns | 2.83 ns | 2.65 ns |      - |         - |
|                 If | 170.9 ns | 1.90 ns | 1.78 ns | 0.0381 |     160 B |
|   SwitchExpression | 173.4 ns | 3.24 ns | 3.03 ns | 0.0381 |     160 B |
|             Switch | 173.6 ns | 2.03 ns | 1.80 ns | 0.0381 |     160 B |
