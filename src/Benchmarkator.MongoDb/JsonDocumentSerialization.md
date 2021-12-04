## Serializing `System.Text.JsonDocument`

What's better option:

- use `System.Text.Json.JsonSerializer.Serialize`
- or use `JsonDocument.WriteTo`

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1387 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
|     Method |   Size |        Mean |     Error |    StdDev | Ratio | RatioSD |   Gen 0 | Allocated |
|----------- |------- |------------:|----------:|----------:|------:|--------:|--------:|----------:|
| Serializer |  Small |    431.1 ns |   6.87 ns |   6.09 ns |  1.00 |    0.00 |  0.1760 |     368 B |
|    WriteTo |  Small |    499.0 ns |   6.91 ns |   6.46 ns |  1.16 |    0.01 |  0.5312 |   1,112 B |
|            |        |             |           |           |       |         |         |           |
| Serializer | Medium |  2,519.5 ns |  37.79 ns |  35.35 ns |  1.00 |    0.00 |  1.1215 |   2,352 B |
|    WriteTo | Medium |  2,936.6 ns |  23.71 ns |  22.18 ns |  1.17 |    0.02 |  4.5700 |   9,576 B |
|            |        |             |           |           |       |         |         |           |
| Serializer |  Large | 15,247.1 ns | 187.13 ns | 165.88 ns |  1.00 |    0.00 |  6.9885 |  14,664 B |
|    WriteTo |  Large | 18,698.3 ns | 229.46 ns | 214.64 ns |  1.23 |    0.02 | 22.2168 |  46,784 B |
