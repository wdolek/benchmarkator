## From collection to `Dictionary<,>`

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|             Method | NumOfItems |         Mean |       Error |      StdDev | Ratio | RatioSD |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|------------------- |----------- |-------------:|------------:|------------:|------:|--------:|---------:|---------:|---------:|----------:|
| ToDictionarySimple |         10 |     108.3 ns |     0.54 ns |     0.48 ns |  1.00 |    0.00 |   0.0842 |        - |        - |     352 B |
|   ToDictionaryLinq |         10 |     150.2 ns |     0.91 ns |     0.76 ns |  1.39 |    0.01 |   0.0842 |        - |        - |     352 B |
|      AggregateLinq |         10 |     297.7 ns |     5.96 ns |     6.12 ns |  2.76 |    0.06 |   0.1950 |        - |        - |     816 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |        100 |     931.8 ns |    16.00 ns |    18.42 ns |  1.00 |    0.00 |   0.5426 |        - |        - |   2,272 B |
|   ToDictionaryLinq |        100 |   1,229.6 ns |    23.73 ns |    26.37 ns |  1.32 |    0.05 |   0.5417 |        - |        - |   2,272 B |
|      AggregateLinq |        100 |   2,675.2 ns |    32.43 ns |    28.75 ns |  2.88 |    0.06 |   1.7738 |        - |        - |   7,432 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |       1000 |  11,753.7 ns |   226.07 ns |   232.16 ns |  1.00 |    0.00 |   5.2795 |   0.6561 |        - |  22,192 B |
|   ToDictionaryLinq |       1000 |  14,883.7 ns |   208.42 ns |   194.96 ns |  1.27 |    0.03 |   5.2795 |   0.1373 |        - |  22,192 B |
|      AggregateLinq |       1000 |  30,395.4 ns |   209.34 ns |   195.82 ns |  2.58 |    0.06 |  17.3950 |   3.8452 |        - |  73,208 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |      10000 | 169,542.5 ns | 1,859.68 ns | 1,648.55 ns |  1.00 |    0.00 |  49.8047 |  49.8047 |  49.8047 | 202,209 B |
|   ToDictionaryLinq |      10000 | 192,696.5 ns | 3,826.44 ns | 3,758.08 ns |  1.14 |    0.01 |  49.8047 |  49.8047 |  49.8047 | 202,209 B |
|      AggregateLinq |      10000 | 363,137.5 ns | 6,961.58 ns | 7,737.78 ns |  2.13 |    0.05 | 124.5117 | 124.5117 | 124.5117 | 673,146 B |
