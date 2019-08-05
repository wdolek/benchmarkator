## Divisible by two

Should you use modulo? Should you use logical and? Is this gonna be JITted anyway?
See [DivisibleByTwo.cs](./DivisibleByTwo.cs) for implementation details (shockinlgy nothing ... surprising).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|     Method |      Number |      Mean |     Error |    StdDev |    Median |
|----------- |------------ |----------:|----------:|----------:|----------:|
| LogicalAnd |           2 | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
| LogicalAnd |         256 | 0.0004 ns | 0.0015 ns | 0.0014 ns | 0.0000 ns |
| LogicalAnd |        1024 | 0.0029 ns | 0.0062 ns | 0.0058 ns | 0.0000 ns |
| LogicalAnd |           0 | 0.0030 ns | 0.0065 ns | 0.0057 ns | 0.0000 ns |
| LogicalAnd | -2147483648 | 0.0036 ns | 0.0066 ns | 0.0062 ns | 0.0000 ns |
| LogicalAnd |  2147483647 | 0.0060 ns | 0.0089 ns | 0.0084 ns | 0.0000 ns |
|     Modulo |           0 | 0.0957 ns | 0.0051 ns | 0.0047 ns | 0.0945 ns |
|     Modulo | -2147483648 | 0.1013 ns | 0.0076 ns | 0.0071 ns | 0.0978 ns |
|     Modulo |         256 | 0.1053 ns | 0.0109 ns | 0.0102 ns | 0.1074 ns |
|     Modulo |        1024 | 0.1114 ns | 0.0134 ns | 0.0126 ns | 0.1124 ns |
|     Modulo |  2147483647 | 0.1149 ns | 0.0069 ns | 0.0065 ns | 0.1174 ns |
|     Modulo |           2 | 0.1207 ns | 0.0140 ns | 0.0131 ns | 0.1224 ns |
