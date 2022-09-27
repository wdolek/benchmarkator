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
|                        Method | Categories |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|------------------------------ |----------- |----------:|----------:|----------:|----------:|------:|--------:|
|            &#39;Default capacity&#39; |       List | 4.8004 ns | 0.1582 ns | 0.1480 ns | 4.8463 ns |  1.00 |    0.00 |
|               &#39;Capacity of 0&#39; |       List | 4.2538 ns | 0.1572 ns | 0.1227 ns | 4.2917 ns |  0.89 |    0.03 |
| &#39;Single pre-created instance&#39; |       List | 0.1082 ns | 0.0645 ns | 0.0603 ns | 0.1346 ns |  0.02 |    0.01 |
|                               |            |           |           |           |           |       |         |
|            &#39;Default capacity&#39; | Dictionary | 4.2503 ns | 0.0721 ns | 0.0639 ns | 4.2601 ns |  1.00 |    0.00 |
|               &#39;Capacity of 0&#39; | Dictionary | 6.0897 ns | 0.1357 ns | 0.1203 ns | 6.1034 ns |  1.43 |    0.03 |
| &#39;Single pre-created instance&#39; | Dictionary | 0.0987 ns | 0.0667 ns | 0.0624 ns | 0.1348 ns |  0.02 |    0.02 |
