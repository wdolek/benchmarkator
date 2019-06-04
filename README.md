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
  Job-YNKLGM : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |       Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|------------:|------:|------:|----------:|
| &#39;Stream d13n&#39; | 190.3 ms | 6.570 ms | 19.37 ms | 138000.0000 |     - |     - | 553.89 MB |
| &#39;String d13n&#39; | 205.6 ms | 5.110 ms | 15.07 ms |  72000.0000 |     - |     - | 288.39 MB |

#### Medium JSON response

JSON example: [here](src/Allocator/Data/M.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-YNKLGM : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |   Median |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|---------:|-----------:|------:|------:|----------:|
| &#39;Stream d13n&#39; | 210.9 ms | 6.779 ms | 19.99 ms | 204.9 ms | 28000.0000 |     - |     - | 114.06 MB |
| &#39;String d13n&#39; | 231.1 ms | 8.453 ms | 24.92 ms | 226.6 ms | 27000.0000 |     - |     - | 110.09 MB |

#### Large JSON response

JSON example: [here](src/Allocator/Data/L.json).

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.529 (1809/October2018Update/Redstone5)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.104
  [Host]     : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT
  Job-YNKLGM : .NET Core 2.2.2 (CoreCLR 4.6.27317.07, CoreFX 4.6.27318.02), 64bit RyuJIT

RunStrategy=ColdStart  

```
|        Method |     Mean |    Error |   StdDev |   Median |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|---------:|-----------:|----------:|----------:|----------:|
| &#39;Stream d13n&#39; | 230.0 ms | 7.945 ms | 23.43 ms | 222.8 ms | 10000.0000 | 2000.0000 |         - |  47.48 MB |
| &#39;String d13n&#39; | 273.0 ms | 9.763 ms | 28.79 ms | 265.5 ms | 19000.0000 | 9000.0000 | 9000.0000 |   77.3 MB |
