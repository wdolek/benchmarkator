## Deserializing of JSON response

Should you read string data and then deserialize or deserialize response stream right away?

Deserialization is run multiple times for each iteration (to avoid short runs).
Number of repeats per iteration differs per input size (small input is repeated more than large input),
see [JsonPayloadDeserialization.cs](./JsonPayloadDeserialization.cs) for details.

Reading:
- [Newtonsoft.Json Performance Tips](https://www.newtonsoft.com/json/help/html/Performance.htm)

### Small JSON response

JSON example: [here](./Data/S.json).

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|        Method | bufferSize |     Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|----------:|----------:|-------:|-------:|------:|----------:|
| &#39;Stream d13n&#39; |        128 | 1.611 us | 0.0050 us | 0.0044 us | 0.9079 |      - |     - |   3.71 KB |
| &#39;Stream d13n&#39; |        512 | 1.663 us | 0.0117 us | 0.0110 us | 1.1826 |      - |     - |   4.84 KB |
| &#39;Stream d13n&#39; |       1024 | 1.730 us | 0.0068 us | 0.0064 us | 1.5507 |      - |     - |   6.34 KB |
| &#39;String d13n&#39; |          ? | 1.925 us | 0.0033 us | 0.0029 us | 0.9117 |      - |     - |   3.73 KB |
| &#39;Stream d13n&#39; |       4096 | 2.115 us | 0.0141 us | 0.0132 us | 3.7498 | 0.1678 |     - |  15.34 KB |

### Medium JSON response

JSON example: [here](./Data/M.json).

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|        Method | bufferSize |     Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|---------:|---------:|-------:|-------:|------:|----------:|
| &#39;Stream d13n&#39; |        512 | 15.24 us | 0.106 us | 0.094 us | 2.5787 |      - |     - |  10.59 KB |
| &#39;Stream d13n&#39; |       1024 | 15.59 us | 0.067 us | 0.060 us | 2.9297 |      - |     - |  12.09 KB |
| &#39;String d13n&#39; |          ? | 15.68 us | 0.098 us | 0.082 us | 3.1433 |      - |     - |  12.94 KB |
| &#39;Stream d13n&#39; |        128 | 15.83 us | 0.054 us | 0.048 us | 2.2888 |      - |     - |   9.46 KB |
| &#39;Stream d13n&#39; |       4096 | 15.87 us | 0.071 us | 0.066 us | 5.1270 | 0.3052 |     - |  21.09 KB |

### Large JSON response

JSON example: [here](./Data/L.json).

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|        Method | bufferSize |     Mean |     Error |    StdDev |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------- |----------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
| &#39;Stream d13n&#39; |       1024 | 1.648 ms | 0.0158 ms | 0.0140 ms | 101.5625 |  35.1563 |        - |  457.8 KB |
| &#39;Stream d13n&#39; |       4096 | 1.658 ms | 0.0236 ms | 0.0209 ms | 105.4688 |  29.2969 |        - |  466.8 KB |
| &#39;Stream d13n&#39; |        512 | 1.664 ms | 0.0171 ms | 0.0160 ms |  99.6094 |  33.2031 |        - |  456.3 KB |
| &#39;Stream d13n&#39; |        128 | 1.684 ms | 0.0129 ms | 0.0121 ms |  99.6094 |  35.1563 |        - | 455.17 KB |
| &#39;String d13n&#39; |          ? | 1.856 ms | 0.0111 ms | 0.0104 ms | 142.5781 | 142.5781 | 142.5781 | 917.33 KB |

---

(_d13n_ stands for "deserialization")
