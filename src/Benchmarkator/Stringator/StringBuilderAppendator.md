## `StringBuilder.Append` when concatenating simple string

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT
  Job-SWYYKG : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT

InvocationCount=1  UnrollFactor=1

```
|             Method |       Mean |     Error |   StdDev |     Median | Ratio | RatioSD |
|------------------- |-----------:|----------:|---------:|-----------:|------:|--------:|
|             Append |   493.0 ns |  54.31 ns | 147.8 ns |   400.0 ns |  0.71 |    0.28 |
| AppendInterpolated |   733.3 ns |  66.72 ns | 179.2 ns |   700.0 ns |  1.00 |    0.00 |
|       AppendFormat | 1,305.6 ns | 166.83 ns | 462.3 ns | 1,100.0 ns |  1.86 |    0.83 |
