## Comparing two strings, `==` vs `string.Equals`

- `OpEquality`: `==` operator
- `Equals`: `string.Equals(string, string)`
- `EqualsOrdinal`: `string.Equals(string, string, StringComparison)` (ordinal comparison)

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT
  DefaultJob : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT


```
|        Method |              Strings |      Mean |     Error |    StdDev | Ratio | RatioSD |
|-------------- |--------------------- |----------:|----------:|----------:|------:|--------:|
|    **OpEquality** |  **([zD(...)qMM) [260]** | **0.7023 ns** | **0.0471 ns** | **0.0560 ns** |  **1.00** |    **0.00** |
|        Equals |  ([zD(...)qMM) [260] | 0.7529 ns | 0.0412 ns | 0.0385 ns |  1.06 |    0.07 |
| EqualsOrdinal |  ([zD(...)qMM) [260] | 1.3354 ns | 0.0330 ns | 0.0309 ns |  1.89 |    0.16 |
|               |                      |           |           |           |       |         |
|    **OpEquality** | **(hzZvV4?a, hzZvV4?a)** | **0.6958 ns** | **0.0398 ns** | **0.0372 ns** |  **1.00** |    **0.00** |
|        Equals | (hzZvV4?a, hzZvV4?a) | 0.6906 ns | 0.0351 ns | 0.0329 ns |  1.00 |    0.08 |
| EqualsOrdinal | (hzZvV4?a, hzZvV4?a) | 1.3454 ns | 0.0448 ns | 0.0398 ns |  1.94 |    0.13 |
|               |                      |           |           |           |       |         |
|    **OpEquality** | **(hzZv(...)b2x\) [36]** | **2.7396 ns** | **0.0521 ns** | **0.0462 ns** |  **1.00** |    **0.00** |
|        Equals | (hzZv(...)b2x\) [36] | 2.7529 ns | 0.0227 ns | 0.0177 ns |  1.01 |    0.02 |
| EqualsOrdinal | (hzZv(...)b2x\) [36] | 4.8570 ns | 0.0733 ns | 0.0685 ns |  1.77 |    0.04 |
