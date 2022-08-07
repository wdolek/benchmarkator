## Divisible by two

Should you use modulo? Should you use _logical and_? Is this gonna be JITted anyway?
See [DivisibleByTwo.cs](./DivisibleByTwo.cs) for implementation details (shockinlgy nothing ... surprising).

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|     Method |      Number |      Mean |     Error |    StdDev |    Median |
|----------- |------------ |----------:|----------:|----------:|----------:|
| LogicalAnd |           2 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| LogicalAnd |         256 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| LogicalAnd |        1024 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| LogicalAnd |  2147483647 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| LogicalAnd | -2147483648 | 0.0018 ns | 0.0042 ns | 0.0040 ns | 0.0000 ns |
| LogicalAnd |           0 | 0.0034 ns | 0.0090 ns | 0.0080 ns | 0.0000 ns |
|     Modulo |  2147483647 | 0.0971 ns | 0.0050 ns | 0.0044 ns | 0.0972 ns |
|     Modulo |           2 | 0.1007 ns | 0.0066 ns | 0.0058 ns | 0.1000 ns |
|     Modulo |        1024 | 0.1024 ns | 0.0086 ns | 0.0076 ns | 0.0998 ns |
|     Modulo |         256 | 0.1037 ns | 0.0098 ns | 0.0077 ns | 0.1036 ns |
|     Modulo | -2147483648 | 0.1217 ns | 0.0324 ns | 0.0333 ns | 0.1123 ns |
|     Modulo |           0 | 0.2914 ns | 0.0024 ns | 0.0022 ns | 0.2915 ns |
