## Deserializing of JSON response

Should you read string data and then deserialize or deserialize response stream right away?

Deserialization is run multiple times for each iteration (to avoid short runs).
Number of repeats per iteration differs per input size (small input is repeated more than large input),
see [JsonPayloadDeserialization.cs](./JsonPayloadDeserialization.cs) for details.

Reading:
- [Newtonsoft.Json Performance Tips](https://www.newtonsoft.com/json/help/html/Performance.htm)

### Small JSON response

JSON example: [here](../../Data/S.json).

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|        Method | bufferSize |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
| &#39;Stream d13n&#39; |        128 | 1.975 us | 0.0136 us | 0.0106 us | 1.8158 |     - |     - |   3.73 KB |
| &#39;Stream d13n&#39; |        512 | 2.082 us | 0.0238 us | 0.0223 us | 2.3651 |     - |     - |   4.85 KB |
| &#39;Stream d13n&#39; |       1024 | 2.452 us | 0.0464 us | 0.0411 us | 3.0975 |     - |     - |   6.35 KB |
| &#39;String d13n&#39; |          ? | 2.693 us | 0.0181 us | 0.0169 us | 1.8311 |     - |     - |   3.76 KB |
| &#39;Stream d13n&#39; |       4096 | 3.229 us | 0.0580 us | 0.0542 us | 7.4806 |     - |     - |  15.35 KB |


### Medium JSON response

JSON example: [here](../../Data/M.json).

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|        Method | bufferSize |     Mean |     Error |    StdDev |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|----------:|----------:|--------:|------:|------:|----------:|
| &#39;Stream d13n&#39; |       1024 | 20.01 us | 0.1222 us | 0.1143 us |  6.0120 |     - |     - |  12.36 KB |
| &#39;Stream d13n&#39; |        512 | 20.34 us | 0.2077 us | 0.1943 us |  5.2795 |     - |     - |  10.86 KB |
| &#39;Stream d13n&#39; |        128 | 20.67 us | 0.4559 us | 0.3807 us |  4.7302 |     - |     - |   9.73 KB |
| &#39;String d13n&#39; |          ? | 20.98 us | 0.1964 us | 0.1837 us |  6.4392 |     - |     - |  13.23 KB |
| &#39;Stream d13n&#39; |       4096 | 21.15 us | 0.1238 us | 0.1098 us | 10.4065 |     - |     - |  21.36 KB |


### Large JSON response

JSON example: [here](../../Data/L.json).

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.301
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```

|        Method | bufferSize |     Mean |     Error |    StdDev |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------- |----------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
| &#39;Stream d13n&#39; |       1024 | 2.139 ms | 0.0415 ms | 0.0408 ms | 140.6250 |  42.9688 |        - |  486.9 KB |
| &#39;Stream d13n&#39; |        512 | 2.155 ms | 0.0112 ms | 0.0105 ms | 132.8125 |  42.9688 |        - |  485.4 KB |
| &#39;Stream d13n&#39; |       4096 | 2.174 ms | 0.0274 ms | 0.0257 ms | 128.9063 |  42.9688 |        - |  495.9 KB |
| &#39;Stream d13n&#39; |        128 | 2.201 ms | 0.0360 ms | 0.0319 ms | 140.6250 |  42.9688 |        - | 484.27 KB |
| &#39;String d13n&#39; |          ? | 2.305 ms | 0.0213 ms | 0.0189 ms | 281.2500 | 140.6250 | 140.6250 | 946.45 KB |

