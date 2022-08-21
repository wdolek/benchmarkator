## Adding to `ConcurrentDictionary` when not really needed

After refactoring/simplifying code, what's the price of using `ConcurrentDictionary<,>` once it's not really needed?

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|           Dictionary |  2.993 μs | 0.0588 μs | 0.1076 μs |  1.00 |    0.00 |
| ConcurrentDictionary | 18.427 μs | 0.3608 μs | 0.4692 μs |  6.10 |    0.27 |
