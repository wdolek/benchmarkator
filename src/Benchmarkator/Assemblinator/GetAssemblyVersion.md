## Get Assembly version

Benchmark demonstrating impact of accessing `AssemblyInformationalVersionAttribute` over and over.
(There's nothing surprising, just showing how _bad_ it is to not cache custom attribute)

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                                                                  Method |         Mean |      Error |     StdDev |
|------------------------------------------------------------------------ |-------------:|-----------:|-----------:|
| &#39;Always read assembly version by looking for custom assembly attribute&#39; | 1,762.519 ns | 35.3235 ns | 33.0416 ns |
|                    &#39;Read assembly version once, store value into field&#39; |     2.219 ns |  0.1171 ns |  0.1096 ns |
