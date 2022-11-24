## Adding to collection without and with specified capacity

Difference between adding to collection without and with specified capacity and to linked list (which doesn't have pre-allocated capacity).

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT


```
|                      Method | Categories | Size |        Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|---------------------------- |----------- |----- |------------:|----------:|----------:|------:|--------:|-------:|----------:|
|       AddToListWithCapacity |       List |    4 |    15.45 ns |  0.152 ns |  0.118 ns |  0.66 |    0.02 | 0.0172 |      72 B |
|                   AddToList |       List |    4 |    23.52 ns |  0.528 ns |  0.518 ns |  1.00 |    0.00 | 0.0172 |      72 B |
|             AddToLinkedList |       List |    4 |    50.69 ns |  0.699 ns |  0.619 ns |  2.15 |    0.06 | 0.0554 |     232 B |
|                             |            |      |             |           |           |       |         |        |           |
| AddToDictionaryWithCapacity | Dictionary |    4 |    64.44 ns |  1.350 ns |  1.325 ns |  0.61 |    0.01 | 0.0650 |     272 B |
|             AddToDictionary | Dictionary |    4 |   105.44 ns |  2.173 ns |  2.033 ns |  1.00 |    0.00 | 0.0918 |     384 B |
|                             |            |      |             |           |           |       |         |        |           |
|       AddToListWithCapacity |       List |   64 |   117.64 ns |  2.035 ns |  1.903 ns |  0.57 |    0.01 | 0.0744 |     312 B |
|                   AddToList |       List |   64 |   205.07 ns |  2.165 ns |  1.920 ns |  1.00 |    0.00 | 0.1547 |     648 B |
|             AddToLinkedList |       List |   64 |   739.82 ns | 11.452 ns | 11.248 ns |  3.60 |    0.07 | 0.7439 |   3,112 B |
|                             |            |      |             |           |           |       |         |        |           |
| AddToDictionaryWithCapacity | Dictionary |   64 |   587.21 ns |  6.938 ns |  6.150 ns |  0.62 |    0.01 | 0.3710 |   1,552 B |
|             AddToDictionary | Dictionary |   64 |   941.35 ns | 18.337 ns | 17.153 ns |  1.00 |    0.00 | 0.8125 |   3,400 B |
|                             |            |      |             |           |           |       |         |        |           |
|       AddToListWithCapacity |       List |  128 |   239.40 ns |  5.678 ns | 16.564 ns |  0.77 |    0.01 | 0.1354 |     568 B |
|                   AddToList |       List |  128 |   348.89 ns |  5.287 ns |  4.128 ns |  1.00 |    0.00 | 0.2828 |   1,184 B |
|             AddToLinkedList |       List |  128 | 1,452.81 ns | 18.332 ns | 16.251 ns |  4.16 |    0.09 | 1.4782 |   6,184 B |
|                             |            |      |             |           |           |       |         |        |           |
| AddToDictionaryWithCapacity | Dictionary |  128 | 1,132.86 ns | 14.956 ns | 13.258 ns |  0.61 |    0.01 | 0.6561 |   2,752 B |
|             AddToDictionary | Dictionary |  128 | 1,844.53 ns | 21.461 ns | 21.077 ns |  1.00 |    0.00 | 1.7662 |   7,392 B |
