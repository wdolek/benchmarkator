## String.Format - what about Spans?

What if we want to format string but print just part of interpolated value? `string.Substring` obviously allocates new string,
so let's find what we can do:

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                                   Method |   Categories | InputSize |        Mean |     Error |    StdDev |  Gen 0 | Allocated |
|----------------------------------------- |------------- |---------- |------------:|----------:|----------:|-------:|----------:|
| InterpolatedStringHandlerAppendFormatted | Interpolated |     Short |    55.79 ns |  1.203 ns |  2.375 ns | 0.0114 |      48 B |
|                              FormatRange |       Format |     Short |    87.50 ns |  1.761 ns |  1.647 ns | 0.0172 |      72 B |
|                          FormatSubstring |       Format |     Short |    87.53 ns |  1.768 ns |  1.568 ns | 0.0172 |      72 B |
|              FormatInterpolatedSubstring | Interpolated |     Short |    93.68 ns |  1.944 ns |  1.996 ns | 0.0286 |     120 B |
|                  FormatInterpolatedRange | Interpolated |     Short |    94.55 ns |  1.966 ns |  2.341 ns | 0.0286 |     120 B |
|                   FormatInterpolatedSpan | Interpolated |     Short |   116.19 ns |  2.211 ns |  1.960 ns | 0.0229 |      96 B |
| InterpolatedStringHandlerAppendFormatted | Interpolated |   Lengthy |   232.18 ns |  4.758 ns |  7.546 ns | 0.1547 |     648 B |
|                              FormatRange |       Format |   Lengthy |   346.93 ns |  6.829 ns |  9.793 ns | 0.3037 |   1,272 B |
|                          FormatSubstring |       Format |   Lengthy |   349.39 ns |  6.996 ns | 11.879 ns | 0.3037 |   1,272 B |
|                   FormatInterpolatedSpan | Interpolated |   Lengthy | 1,069.12 ns | 20.472 ns | 20.106 ns | 0.3090 |   1,296 B |
|              FormatInterpolatedSubstring | Interpolated |   Lengthy | 1,074.19 ns | 21.150 ns | 28.950 ns | 0.4578 |   1,920 B |
|                  FormatInterpolatedRange | Interpolated |   Lengthy | 1,085.95 ns | 20.413 ns | 19.094 ns | 0.4578 |   1,920 B |
