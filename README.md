## Allocator, benchmarks and stuff

### Deserializing of JSON response

Should you read string data and then deserialize or deserialize response stream right away?

Deserialization is run multiple times for each iteration (to avoid short runs).
Number of repeats per iteration differs per input size (small input is repeated more than large input),
see [JsonPayloadDeserialization.cs](src/Allocator/Benchmarks/JsonPayloadDeserialization.cs) for details.

Reading:
- [Newtonsoft.Json Performance Tips](https://www.newtonsoft.com/json/help/html/Performance.htm)

#### Small JSON response

JSON example: [here](src/Allocator/Data/S.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host]     : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  Job-MJUGVB : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method | bufferSize |     Mean |    Error |   StdDev |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|---------:|---------:|-----------:|------:|------:|----------:|
| &#39;Stream d13n&#39; |        128 | 22.90 ms | 4.553 ms | 13.42 ms | 18000.0000 |     - |     - |  36.39 MB |
| &#39;Stream d13n&#39; |        512 | 23.65 ms | 4.345 ms | 12.81 ms | 23000.0000 |     - |     - |  47.38 MB |
| &#39;Stream d13n&#39; |       1024 | 25.71 ms | 4.331 ms | 12.77 ms | 31000.0000 |     - |     - |  62.03 MB |
| &#39;String d13n&#39; |          ? | 30.83 ms | 4.986 ms | 14.70 ms | 18000.0000 |     - |     - |   36.7 MB |
| &#39;Stream d13n&#39; |       4096 | 33.11 ms | 4.473 ms | 13.19 ms | 74000.0000 |     - |     - | 149.92 MB |

#### Medium JSON response

JSON example: [here](src/Allocator/Data/M.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host]     : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  Job-MJUGVB : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method | bufferSize |     Mean |    Error |   StdDev |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----------- |---------:|---------:|---------:|-----------:|------:|------:|----------:|
| &#39;Stream d13n&#39; |        512 | 22.05 ms | 5.106 ms | 15.06 ms |  5000.0000 |     - |     - |   10.6 MB |
| &#39;Stream d13n&#39; |        128 | 22.46 ms | 5.076 ms | 14.97 ms |  4000.0000 |     - |     - |   9.51 MB |
| &#39;Stream d13n&#39; |       1024 | 22.64 ms | 5.173 ms | 15.25 ms |  6000.0000 |     - |     - |  12.07 MB |
| &#39;Stream d13n&#39; |       4096 | 23.73 ms | 5.142 ms | 15.16 ms | 10000.0000 |     - |     - |  20.86 MB |
| &#39;String d13n&#39; |          ? | 24.02 ms | 5.256 ms | 15.50 ms |  6000.0000 |     - |     - |  12.92 MB |

#### Large JSON response

JSON example: [here](src/Allocator/Data/L.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host]     : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  Job-MJUGVB : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method | bufferSize |     Mean |    Error |   StdDev |      Gen 0 |      Gen 1 |      Gen 2 | Allocated |
|-------------- |----------- |---------:|---------:|---------:|-----------:|-----------:|-----------:|----------:|
| &#39;Stream d13n&#39; |        512 | 202.5 ms | 6.108 ms | 18.01 ms |  8000.0000 |  4000.0000 |          - |   47.4 MB |
| &#39;Stream d13n&#39; |       4096 | 203.5 ms | 5.710 ms | 16.84 ms |  9000.0000 |  4000.0000 |          - |  48.43 MB |
| &#39;Stream d13n&#39; |        128 | 203.7 ms | 5.631 ms | 16.60 ms |  8000.0000 |  4000.0000 |          - |  47.29 MB |
| &#39;Stream d13n&#39; |       1024 | 204.3 ms | 5.347 ms | 15.76 ms |  9000.0000 |  4000.0000 |          - |  47.55 MB |
| &#39;String d13n&#39; |          ? | 227.4 ms | 5.324 ms | 15.70 ms | 15000.0000 | 14000.0000 | 14000.0000 |  92.43 MB |
