## From collection to `Dictionary<,>`

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT


```
| Method             | NumOfItems |         Mean |       Error |      StdDev | Ratio | RatioSD |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|--------------------|----------- |-------------:|------------:|------------:|------:|--------:|---------:|---------:|---------:|----------:|
| ToDictionarySimple |         10 |     113.8 ns |     2.17 ns |     4.82 ns |  1.00 |    0.00 |   0.0839 |        - |        - |     352 B |
| ToDictionaryLinq   |         10 |     158.0 ns |     2.51 ns |     2.10 ns |  1.34 |    0.07 |   0.0842 |        - |        - |     352 B |
| AggregateLinq      |         10 |     300.9 ns |     3.89 ns |     3.44 ns |  2.56 |    0.13 |   0.1950 |        - |        - |     816 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |        100 |     979.2 ns |    14.40 ns |    13.47 ns |  1.00 |    0.00 |   0.5417 |        - |        - |   2,272 B |
| ToDictionaryLinq   |        100 |   1,298.3 ns |    23.72 ns |    22.19 ns |  1.33 |    0.03 |   0.5417 |        - |        - |   2,272 B |
| AggregateLinq      |        100 |   2,753.6 ns |    30.25 ns |    23.61 ns |  2.82 |    0.04 |   1.7738 |        - |        - |   7,432 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |       1000 |  12,570.0 ns |    76.81 ns |    68.09 ns |  1.00 |    0.00 |   5.2795 |        - |        - |  22,192 B |
| ToDictionaryLinq   |       1000 |  15,445.4 ns |   139.83 ns |   123.96 ns |  1.23 |    0.01 |   5.2795 |   0.6409 |        - |  22,192 B |
| AggregateLinq      |       1000 |  31,727.2 ns |   326.93 ns |   273.00 ns |  2.53 |    0.03 |  17.3950 |   3.8452 |        - |  73,208 B |
|                    |            |              |             |             |       |         |          |          |          |           |
| ToDictionarySimple |      10000 | 175,699.5 ns | 1,531.55 ns | 1,357.68 ns |  1.00 |    0.00 |  49.8047 |  49.8047 |  49.8047 | 202,209 B |
| ToDictionaryLinq   |      10000 | 192,129.8 ns | 3,136.24 ns | 2,933.64 ns |  1.09 |    0.02 |  49.8047 |  49.8047 |  49.8047 | 202,209 B |
| AggregateLinq      |      10000 | 370,464.7 ns | 4,206.03 ns | 3,728.54 ns |  2.11 |    0.03 | 124.5117 | 124.5117 | 124.5117 | 673,146 B |
