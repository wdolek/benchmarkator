## Serializing `System.Text.JsonDocument`

What's better option:

- use `System.Text.Json.JsonSerializer.Serialize`
- or use `JsonDocument.WriteTo`

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|     Method |   Size |        Mean |     Error |    StdDev | Ratio | RatioSD |   Gen 0 |  Gen 1 | Allocated |
|----------- |------- |------------:|----------:|----------:|------:|--------:|--------:|-------:|----------:|
| Serializer |  Small |    415.4 ns |   6.84 ns |   6.40 ns |  1.00 |    0.00 |  0.0877 |      - |     368 B |
|    WriteTo |  Small |    475.7 ns |   5.70 ns |   5.33 ns |  1.15 |    0.02 |  0.2651 |      - |   1,112 B |
|            |        |             |           |           |       |         |         |        |           |
| Serializer | Medium |  2,322.7 ns |   7.30 ns |   6.47 ns |  1.00 |    0.00 |  0.5608 |      - |   2,360 B |
|    WriteTo | Medium |  2,819.2 ns |  56.38 ns |  62.66 ns |  1.22 |    0.03 |  2.2850 |      - |   9,576 B |
|            |        |             |           |           |       |         |         |        |           |
| Serializer |  Large | 17,239.4 ns | 253.99 ns | 212.09 ns |  1.00 |    0.00 |  4.0283 |      - |  16,992 B |
|    WriteTo |  Large | 19,618.4 ns | 366.34 ns | 342.67 ns |  1.14 |    0.03 | 11.8103 | 0.0305 |  49,560 B |
