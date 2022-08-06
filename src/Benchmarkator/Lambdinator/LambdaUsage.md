## What's `static` for lambda?

Let's see how using lambda differs:

- inline lambda
- static inline lambda
- using static method
- using instance method
- using member group (`Func` instead of `i => Func(i)`) (allocation of member group to be fixed in .NET 7)

... after looking into IL, it should be obvious that there's no huge difference - but what does benchmark say?

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1826 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  DefaultJob : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT


```
|                    Method |      Categories |     Mean |   Error |  StdDev |    Gen 0 | Allocated |
|-------------------------- |---------------- |---------:|--------:|--------:|---------:|----------:|
|              InlineLambda |          Inline | 143.9 μs | 1.04 μs | 0.97 μs | 113.5254 |    232 KB |
|        InlineStaticLambda |          Inline | 152.3 μs | 1.16 μs | 0.97 μs | 113.5254 |    232 KB |
|        StaticMemberLambda |   Static,Member | 173.5 μs | 1.08 μs | 0.96 μs | 113.5254 |    232 KB |
| InstanceMemberGroupLambda | Instance,Member | 175.6 μs | 1.50 μs | 1.40 μs | 144.7754 |    296 KB |
|   StaticMemberGroupLambda |   Static,Member | 187.2 μs | 1.38 μs | 1.29 μs | 144.7754 |    296 KB |
|      InstanceMemberLambda | Instance,Member | 209.4 μs | 3.24 μs | 2.87 μs | 144.7754 |    296 KB |
