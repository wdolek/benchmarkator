## Allocator, benchmarks and stuff

### Deserializing of JSON response

Should you read string data and then deserialize or deserialize response stream right away?

Reading:
- [Newtonsoft.Json Performance Tips](https://www.newtonsoft.com/json/help/html/Performance.htm)

#### Small JSON response

JSON example: [here](src/Allocator/Data/S.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-DRZIDG : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |    Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|----------:|------:|------:|------:|----------:|
| &#39;String d13n&#39; | 1.496 ms | 4.244 ms | 12.51 ms | 0.2390 ms |     - |     - |     - | 295.31 KB |
| &#39;Stream d13n&#39; | 1.565 ms | 4.563 ms | 13.45 ms | 0.2107 ms |     - |     - |     - | 567.19 KB |

#### Medium JSON response

JSON example: [here](src/Allocator/Data/M.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-DRZIDG : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|---------:|------:|------:|------:|----------:|
| &#39;Stream d13n&#39; | 3.550 ms | 4.960 ms | 14.62 ms | 2.043 ms |     - |     - |     - |   1.14 MB |
| &#39;String d13n&#39; | 3.618 ms | 4.925 ms | 14.52 ms | 2.131 ms |     - |     - |     - |    1.1 MB |

#### Large JSON response

JSON example: [here](src/Allocator/Data/L.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-DRZIDG : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |   Median |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|---------:|-----------:|----------:|----------:|----------:|
| &#39;Stream d13n&#39; | 203.1 ms | 5.680 ms | 16.75 ms | 199.8 ms | 10000.0000 | 2000.0000 |         - |  47.48 MB |
| &#39;String d13n&#39; | 209.3 ms | 5.622 ms | 16.58 ms | 204.9 ms | 19000.0000 | 9000.0000 | 9000.0000 |   77.3 MB |
