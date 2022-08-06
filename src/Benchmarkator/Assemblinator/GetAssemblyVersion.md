## Get Assembly version

Benchmark demonstrating impact of accessing `AssemblyInformationalVersionAttribute` over and over.
(There's nothing surprising, just showing how _bad_ it is to not cache custom attribute)

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1826 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  DefaultJob : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT


```
|                                                                  Method |         Mean |      Error |     StdDev |
|------------------------------------------------------------------------ |-------------:|-----------:|-----------:|
| &#39;Always read assembly version by looking for custom assembly attribute&#39; | 1,973.004 ns | 37.8516 ns | 43.5899 ns |
|                    &#39;Read assembly version once, store value into field&#39; |     2.812 ns |  0.0732 ns |  0.0685 ns |
