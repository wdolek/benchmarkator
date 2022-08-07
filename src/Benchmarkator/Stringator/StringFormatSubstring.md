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
|                                   Method |   Categories | InputSize |      Mean |    Error |   StdDev |  Gen 0 | Allocated |
|----------------------------------------- |------------- |---------- |----------:|---------:|---------:|-------:|----------:|
| InterpolatedStringHandlerAppendFormatted | Interpolated |     Short |  47.21 ns | 0.348 ns | 0.291 ns | 0.0229 |      48 B |
|                          FormatSubstring |       Format |     Short |  68.65 ns | 1.028 ns | 0.858 ns | 0.0343 |      72 B |
|                              FormatRange |       Format |     Short |  72.50 ns | 0.654 ns | 0.611 ns | 0.0343 |      72 B |
|              FormatInterpolatedSubstring | Interpolated |     Short |  76.10 ns | 0.660 ns | 0.618 ns | 0.0573 |     120 B |
|                  FormatInterpolatedRange | Interpolated |     Short |  76.91 ns | 1.160 ns | 1.085 ns | 0.0573 |     120 B |
|                   FormatInterpolatedSpan | Interpolated |     Short |  99.11 ns | 0.776 ns | 0.726 ns | 0.0459 |      96 B |
| InterpolatedStringHandlerAppendFormatted | Interpolated |   Lengthy | 149.90 ns | 1.715 ns | 1.339 ns | 0.3097 |     648 B |
|                          FormatSubstring |       Format |   Lengthy | 232.01 ns | 4.588 ns | 4.291 ns | 0.6082 |   1,272 B |
|                              FormatRange |       Format |   Lengthy | 234.05 ns | 4.591 ns | 4.715 ns | 0.6080 |   1,272 B |
|                  FormatInterpolatedRange | Interpolated |   Lengthy | 816.94 ns | 6.087 ns | 5.396 ns | 0.9174 |   1,920 B |
|                   FormatInterpolatedSpan | Interpolated |   Lengthy | 834.33 ns | 6.244 ns | 5.841 ns | 0.6189 |   1,296 B |
|              FormatInterpolatedSubstring | Interpolated |   Lengthy | 921.15 ns | 7.985 ns | 7.078 ns | 0.9174 |   1,920 B |
