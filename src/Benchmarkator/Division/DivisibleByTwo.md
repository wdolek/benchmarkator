## Divisible by two

Should you use modulo? Should you use _logical and_? Is this gonna be JITted anyway?
See [DivisibleByTwo.cs](./DivisibleByTwo.cs) for implementation details (shockinlgy nothing ... surprising).

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|     Method |      Number |      Mean |     Error |    StdDev |    Median |
|----------- |------------ |----------:|----------:|----------:|----------:|
| LogicalAnd |         256 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|     Modulo |        1024 | 0.0009 ns | 0.0026 ns | 0.0024 ns | 0.0000 ns |
|     Modulo |  2147483647 | 0.0009 ns | 0.0038 ns | 0.0032 ns | 0.0000 ns |
|     Modulo |           0 | 0.0019 ns | 0.0040 ns | 0.0037 ns | 0.0000 ns |
| LogicalAnd |        1024 | 0.0037 ns | 0.0133 ns | 0.0125 ns | 0.0000 ns |
| LogicalAnd | -2147483648 | 0.0072 ns | 0.0122 ns | 0.0108 ns | 0.0007 ns |
| LogicalAnd |           0 | 0.0076 ns | 0.0135 ns | 0.0139 ns | 0.0000 ns |
| LogicalAnd |  2147483647 | 0.0109 ns | 0.0162 ns | 0.0151 ns | 0.0024 ns |
|     Modulo |         256 | 0.0124 ns | 0.0238 ns | 0.0223 ns | 0.0000 ns |
|     Modulo |           2 | 0.0196 ns | 0.0220 ns | 0.0195 ns | 0.0173 ns |
|     Modulo | -2147483648 | 0.0259 ns | 0.0225 ns | 0.0211 ns | 0.0263 ns |
| LogicalAnd |           2 | 0.0861 ns | 0.0362 ns | 0.0948 ns | 0.0429 ns |
