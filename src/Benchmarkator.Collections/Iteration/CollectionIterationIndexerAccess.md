## Accessing value of collection using indexer (`[]`)

Is there any significant difference when accessing array value directly or trough `List<>` indexer?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT


```
|                  Method |     Mean |   Error |  StdDev | Ratio | RatioSD |
|------------------------ |---------:|--------:|--------:|------:|--------:|
|  &#39;Access array indexer&#39; | 137.1 ns | 1.27 ns | 1.13 ns |  1.00 |    0.00 |
| &#39;Access List&lt;&gt; indexer&#39; | 152.9 ns | 2.78 ns | 2.60 ns |  1.11 |    0.02 |
