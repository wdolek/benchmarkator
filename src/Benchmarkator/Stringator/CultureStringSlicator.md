## Reading country code from culture string

What's the fastest way to read country code from culture string? Does input string casing matter?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT


```
|     Method | Culture |      Mean |    Error |   StdDev |  Gen 0 | Allocated |
|----------- |-------- |----------:|---------:|---------:|-------:|----------:|
|  Substring |   to-TO |  33.97 ns | 0.540 ns | 0.505 ns | 0.0191 |      80 B |
|  Substring |   TO-TO |  34.65 ns | 0.678 ns | 0.634 ns | 0.0191 |      80 B |
|  Substring |   to-to |  43.14 ns | 0.906 ns | 0.890 ns | 0.0268 |     112 B |
|  SpanSplit |   to-TO |  57.37 ns | 0.536 ns | 0.475 ns | 0.0114 |      48 B |
|  SpanSplit |   to-to |  58.18 ns | 1.217 ns | 1.139 ns | 0.0114 |      48 B |
|  SpanSplit |   TO-TO |  60.07 ns | 1.108 ns | 0.865 ns | 0.0114 |      48 B |
| NaiveSplit |   TO-TO |  95.86 ns | 1.433 ns | 1.270 ns | 0.0362 |     152 B |
| NaiveSplit |   to-TO |  96.09 ns | 1.567 ns | 1.466 ns | 0.0362 |     152 B |
| NaiveSplit |   to-to | 109.32 ns | 1.266 ns | 1.184 ns | 0.0440 |     184 B |
|      Regex |   to-TO | 230.62 ns | 2.096 ns | 1.858 ns | 0.1109 |     464 B |
|      Regex |   TO-TO | 232.30 ns | 4.703 ns | 5.227 ns | 0.1109 |     464 B |
|      Regex |   to-to | 247.19 ns | 3.866 ns | 3.616 ns | 0.1183 |     496 B |
