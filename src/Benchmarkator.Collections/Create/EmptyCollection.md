## Returning empty collection

Returning _empty_ collection to indicate action does not have anything to return. Is it fine to go with always instantiating empty `List<>`
or is it worth optimizing this as well? (Keep in mind, returned value should be read-only, otherwise you can shoot yourself in the foot).

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                        Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|------------------------------ |----------:|----------:|----------:|----------:|------:|--------:|
|            &#39;Default capacity&#39; | 4.1372 ns | 0.1658 ns | 0.1551 ns | 4.1179 ns | 1.000 |    0.00 |
|               &#39;Capacity of 0&#39; | 4.5195 ns | 0.1738 ns | 0.2260 ns | 4.4572 ns | 1.104 |    0.08 |
| &#39;Single pre-created instance&#39; | 0.0032 ns | 0.0101 ns | 0.0084 ns | 0.0000 ns | 0.001 |    0.00 |
