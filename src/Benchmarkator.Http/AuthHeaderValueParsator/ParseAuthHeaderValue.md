## Parse authorization header value

When you have raw value of authorization header ("Bearer <token>"), what is faster way of creating new `AuthenticationHeaderValue` instance?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT
  DefaultJob : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev |  Gen 0 | Allocated |
|--------- |----------:|---------:|---------:|-------:|----------:|
|     Ctor |  87.73 ns | 1.709 ns | 1.599 ns | 0.0516 |     216 B |
| TryParse | 499.59 ns | 7.390 ns | 6.551 ns | 0.0610 |     256 B |
