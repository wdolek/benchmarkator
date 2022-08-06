## String.Format - what about Spans?

What if we want to format string but print just part of interpolated value? `string.Substring` obviously allocates new string,
so let's find what we can do:

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1826 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  DefaultJob : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT


```
|                                   Method |   Categories | InputSize |      Mean |     Error |   StdDev |  Gen 0 | Allocated |
|----------------------------------------- |------------- |---------- |----------:|----------:|---------:|-------:|----------:|
| InterpolatedStringHandlerAppendFormatted | Interpolated |     Short |  40.11 ns |  0.197 ns | 0.153 ns | 0.0114 |      24 B |
|              FormatInterpolatedSubstring | Interpolated |     Short |  42.66 ns |  0.339 ns | 0.301 ns | 0.0229 |      48 B |
|                  FormatInterpolatedRange | Interpolated |     Short |  43.32 ns |  0.280 ns | 0.262 ns | 0.0229 |      48 B |
|                          FormatSubstring |       Format |     Short |  55.54 ns |  0.663 ns | 0.621 ns | 0.0229 |      48 B |
|                              FormatRange |       Format |     Short |  63.52 ns |  1.356 ns | 2.612 ns | 0.0229 |      48 B |
|                   FormatInterpolatedSpan | Interpolated |     Short |  79.09 ns |  0.646 ns | 0.572 ns | 0.0229 |      48 B |
| InterpolatedStringHandlerAppendFormatted | Interpolated |   Lengthy | 145.14 ns |  1.237 ns | 1.157 ns | 0.2983 |     624 B |
|                          FormatSubstring |       Format |   Lengthy | 215.99 ns |  2.613 ns | 2.182 ns | 0.5968 |   1,248 B |
|                              FormatRange |       Format |   Lengthy | 250.82 ns |  2.552 ns | 2.263 ns | 0.5965 |   1,248 B |
|              FormatInterpolatedSubstring | Interpolated |   Lengthy | 726.12 ns |  6.602 ns | 6.175 ns | 0.5960 |   1,248 B |
|                   FormatInterpolatedSpan | Interpolated |   Lengthy | 821.31 ns | 11.148 ns | 9.882 ns | 0.5960 |   1,248 B |
|                  FormatInterpolatedRange | Interpolated |   Lengthy | 871.39 ns |  9.355 ns | 8.750 ns | 0.5960 |   1,248 B |
