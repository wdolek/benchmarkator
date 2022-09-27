## Adding to collection without and with specified capacity

Difference between adding to collection without and with specified capacity.

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                      Method | Categories |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|---------------------------- |----------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|
|       AddToListWithCapacity |       List |   323.9 ns |  6.38 ns |  6.26 ns |  0.60 |    0.03 | 0.1354 |     568 B |
|                   AddToList |       List |   538.3 ns | 10.27 ns | 17.15 ns |  1.00 |    0.00 | 0.2823 |   1,184 B |
|                             |            |            |          |          |       |         |        |           |
| AddToDictionaryWithCapacity | Dictionary | 1,664.8 ns | 32.72 ns | 33.60 ns |  0.56 |    0.02 | 0.6561 |   2,752 B |
|             AddToDictionary | Dictionary | 2,973.9 ns | 59.43 ns | 70.74 ns |  1.00 |    0.00 | 1.7662 |   7,392 B |
