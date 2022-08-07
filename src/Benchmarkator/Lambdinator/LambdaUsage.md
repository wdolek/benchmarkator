## What's `static` for lambda?

Let's see how using lambda differs:

- inline lambda
- static inline lambda (`static i => 0`)
- using static method
- using instance method
- using method group (`Func` instead of `i => Func(i)`) (allocation of member group is fixed in .NET 7)

... after looking into IL it should be obvious that there's no huge difference - but what does benchmark say?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1826 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  DefaultJob : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT


```
|               Method | Categories |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|--------------------- |----------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|
|   InlineStaticLambda |     Inline | 0.8223 ns | 0.0095 ns | 0.0079 ns |  0.73 |    0.01 |      - |         - |
|         InlineLambda |     Inline | 1.1329 ns | 0.0209 ns | 0.0175 ns |  1.00 |    0.00 |      - |         - |
|                      |            |           |           |           |       |         |        |           |
|   StaticMemberLambda |     Member | 0.8094 ns | 0.0089 ns | 0.0074 ns |  0.12 |    0.00 |      - |         - |
| InstanceMemberLambda |     Member | 6.8336 ns | 0.1678 ns | 0.1723 ns |  1.00 |    0.00 | 0.0306 |      64 B |
|  InstanceMethodGroup |     Member | 6.9161 ns | 0.1052 ns | 0.0984 ns |  1.02 |    0.02 | 0.0306 |      64 B |
|    StaticMethodGroup |     Member | 7.1837 ns | 0.1002 ns | 0.0937 ns |  1.06 |    0.03 | 0.0306 |      64 B |
