## What's `static` for lambda?

Let's see how using lambda differs:

- inline lambda
- static inline lambda (`static i => 0`)
- using static method
- using instance method
- using method group (`Func` instead of `i => Func(i)`) (allocation of member group is fixed in .NET 7)

... after looking into IL it should be obvious that there's no huge difference - but what does benchmark say?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|               Method | Categories |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|--------------------- |----------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|
|   InlineStaticLambda |     Inline |  1.385 ns | 0.0611 ns | 0.0572 ns |  1.00 |    0.06 |      - |         - |
|         InlineLambda |     Inline |  1.390 ns | 0.0565 ns | 0.0472 ns |  1.00 |    0.00 |      - |         - |
|                      |            |           |           |           |       |         |        |           |
|   StaticMemberLambda |     Member |  1.352 ns | 0.0638 ns | 0.0655 ns |  0.10 |    0.01 |      - |         - |
|  InstanceMethodGroup |     Member | 12.749 ns | 0.2832 ns | 0.3478 ns |  0.96 |    0.03 | 0.0153 |      64 B |
| InstanceMemberLambda |     Member | 13.250 ns | 0.2896 ns | 0.3219 ns |  1.00 |    0.00 | 0.0153 |      64 B |
|    StaticMethodGroup |     Member | 13.703 ns | 0.3114 ns | 0.5849 ns |  1.03 |    0.06 | 0.0153 |      64 B |
