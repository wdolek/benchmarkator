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
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                           Method |       Categories | bufferSize |        Mean |       Error |      StdDev |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|--------------------------------- |----------------- |----------- |------------:|------------:|------------:|-------:|-------:|-------:|----------:|
| &#39;String d13n (System.Text.Json)&#39; | system.text.json |          ? |    916.1 ns |    12.42 ns |    11.01 ns | 0.2594 |      - |      - |      1 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        512 |  1,480.6 ns |    23.73 ns |    21.04 ns | 1.1826 |      - |      - |      5 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        128 |  1,529.1 ns |    29.89 ns |    33.22 ns | 0.9079 |      - |      - |      4 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       1024 |  1,599.4 ns |    26.54 ns |    22.17 ns | 1.5507 |      - |      - |      6 KB |
|       &#39;String d13n (Newtonsoft)&#39; |       newtonsoft |          ? |  1,708.1 ns |    27.16 ns |    27.89 ns | 0.9136 |      - |      - |      4 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       4096 |  2,015.6 ns |    14.86 ns |    13.90 ns | 3.7498 |      - |      - |     15 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       4096 | 89,918.8 ns | 1,302.57 ns | 1,279.29 ns | 2.8076 | 1.3428 | 0.1221 |     12 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       1024 | 90,269.8 ns | 1,714.61 ns | 1,905.79 ns | 2.8076 | 1.3428 | 0.1221 |     12 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        128 | 91,374.1 ns | 1,780.70 ns | 2,252.01 ns | 2.8076 | 1.3428 | 0.1221 |     12 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        512 | 91,578.6 ns | 1,793.74 ns | 2,332.36 ns | 2.8076 | 1.3428 | 0.1221 |     12 KB |

### Medium JSON response

JSON example: [here](./Data/M.json).

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                           Method |       Categories | bufferSize |       Mean |     Error |     StdDev |     Median |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|--------------------------------- |----------------- |----------- |-----------:|----------:|-----------:|-----------:|--------:|-------:|-------:|----------:|
| &#39;String d13n (System.Text.Json)&#39; | system.text.json |          ? |   4.058 μs | 0.0182 μs |  0.0161 μs |   4.061 μs |  1.1520 |      - |      - |      5 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        128 |  14.146 μs | 0.1919 μs |  0.1603 μs |  14.150 μs |  2.3041 |      - |      - |      9 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       4096 |  14.303 μs | 0.1308 μs |  0.1092 μs |  14.306 μs |  5.1422 |      - |      - |     21 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        512 |  14.539 μs | 0.2870 μs |  0.4950 μs |  14.428 μs |  2.5787 |      - |      - |     11 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       1024 |  14.546 μs | 0.2844 μs |  0.5614 μs |  14.327 μs |  2.9449 |      - |      - |     12 KB |
|       &#39;String d13n (Newtonsoft)&#39; |       newtonsoft |          ? |  14.594 μs | 0.1390 μs |  0.1161 μs |  14.576 μs |  3.1586 |      - |      - |     13 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       4096 | 419.653 μs | 8.3512 μs |  8.9357 μs | 416.160 μs | 18.0664 | 8.7891 | 1.4648 |     75 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        128 | 425.301 μs | 8.2541 μs | 10.1368 μs | 424.064 μs | 18.0664 | 8.7891 | 1.9531 |     75 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       1024 | 428.619 μs | 8.4618 μs |  7.9151 μs | 426.447 μs | 18.0664 | 8.7891 | 1.9531 |     75 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        512 | 442.283 μs | 8.8355 μs | 13.4928 μs | 440.107 μs | 18.0664 | 8.7891 | 1.9531 |     75 KB |

### Large JSON response

JSON example: [here](./Data/L.json).

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                           Method |       Categories | bufferSize |       Mean |    Error |   StdDev |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|--------------------------------- |----------------- |----------- |-----------:|---------:|---------:|---------:|---------:|---------:|----------:|
| &#39;String d13n (System.Text.Json)&#39; | system.text.json |          ? |   591.2 μs |  4.84 μs |  4.29 μs | 131.8359 | 124.0234 | 124.0234 |    494 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       4096 |   951.6 μs | 11.63 μs |  9.71 μs |  25.3906 |  12.6953 |   1.9531 |    106 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |       1024 | 1,042.0 μs | 10.15 μs |  9.00 μs |  25.3906 |  11.7188 |        - |    106 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        128 | 1,054.5 μs |  5.07 μs |  4.49 μs |  25.3906 |  11.7188 |        - |    106 KB |
| &#39;Stream d13n (System.Text.Json)&#39; | system.text.json |        512 | 1,056.9 μs | 10.76 μs |  8.98 μs |  25.3906 |  11.7188 |        - |    106 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       4096 | 1,510.5 μs | 26.66 μs | 26.19 μs | 111.3281 |  29.2969 |        - |    467 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |       1024 | 1,512.2 μs | 29.77 μs | 27.85 μs | 103.5156 |  31.2500 |        - |    458 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        512 | 1,535.2 μs | 30.13 μs | 34.70 μs |  99.6094 |  33.2031 |        - |    456 KB |
|       &#39;Stream d13n (Newtonsoft)&#39; |       newtonsoft |        128 | 1,568.1 μs | 30.22 μs | 28.27 μs |  99.6094 |  33.2031 |        - |    455 KB |
|       &#39;String d13n (Newtonsoft)&#39; |       newtonsoft |          ? | 1,847.7 μs | 16.82 μs | 13.14 μs | 142.5781 | 142.5781 | 142.5781 |    917 KB |

---

(_d13n_ stands for "deserialization")
