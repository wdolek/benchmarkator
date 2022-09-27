## String concatenation

Doing simple string concat, deciding between:

- `str1 + str2`
- `string.Concat(str1, str2)`
- `string.Create(...)`

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2006 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT


```
|    Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|---------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|
| StrConcat | 16.07 ns | 0.222 ns | 0.197 ns |  0.96 |    0.02 | 0.0191 |      40 B |
|   StrPlus | 16.77 ns | 0.303 ns | 0.269 ns |  1.00 |    0.00 | 0.0191 |      40 B |
| StrCreate | 17.68 ns | 0.290 ns | 0.257 ns |  1.05 |    0.02 | 0.0191 |      40 B |
