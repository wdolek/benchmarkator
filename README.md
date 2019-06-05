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

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-UOCDUS : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|-----------:|------:|------:|----------:|
| &#39;Stream d13n&#39; | 25.69 ms | 4.823 ms | 14.22 ms | 15000.0000 |     - |     - |  61.34 MB |
| &#39;String d13n&#39; | 31.22 ms | 4.906 ms | 14.46 ms |  9000.0000 |     - |     - |  36.01 MB |

#### Medium JSON response

JSON example: [here](src/Allocator/Data/M.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-UOCDUS : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|----------:|------:|------:|----------:|
| &#39;Stream d13n&#39; | 24.07 ms | 5.742 ms | 16.93 ms | 2000.0000 |     - |     - |     12 MB |
| &#39;String d13n&#39; | 25.09 ms | 5.759 ms | 16.98 ms | 3000.0000 |     - |     - |  12.85 MB |

#### Large JSON response

JSON example: [here](src/Allocator/Data/L.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-UOCDUS : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |      Gen 0 |      Gen 1 |      Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|-----------:|-----------:|-----------:|----------:|
| &#39;Stream d13n&#39; | 229.6 ms | 5.973 ms | 17.61 ms | 10000.0000 |  1000.0000 |          - |  47.54 MB |
| &#39;String d13n&#39; | 240.8 ms | 6.199 ms | 18.28 ms | 14000.0000 | 14000.0000 | 14000.0000 |  92.42 MB |
