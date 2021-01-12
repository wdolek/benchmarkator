## Lookup of value in collection

Having collection of items, what is the fastest way to find (structured) value in it?
Is `Dictionary<TKey,TValue>` the best option here, or should we use good ol' array when
collection is short?

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.200-preview.20601.7
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```

### Small (4)

|             Method |         Categories | Size |        Mean |     Error |    StdDev |
|------------------- |------------------- |----- |------------:|----------:|----------:|
|   ArrayLookupFirst |        Array,First |    4 |   0.5209 ns | 0.0155 ns | 0.0129 ns |
|    ArrayLookupLast |         Array,Last |    4 |   2.8561 ns | 0.0333 ns | 0.0312 ns |
| ArrayLookupMissing |      Array,Missing |    4 |   2.0679 ns | 0.0386 ns | 0.0361 ns |
|    ListLookupFirst |         List,First |    4 |   0.9029 ns | 0.0218 ns | 0.0204 ns |
|     ListLookupLast |          List,Last |    4 |   3.5342 ns | 0.0384 ns | 0.0320 ns |
|  ListLookupMissing |       List,Missing |    4 |   3.5253 ns | 0.0323 ns | 0.0286 ns |
|    DictLookupFirst |   Dictionary,First |    4 |   7.5464 ns | 0.0343 ns | 0.0304 ns |
|     DictLookupLast |    Dictionary,Last |    4 |   7.5526 ns | 0.0345 ns | 0.0306 ns |
|  DictLookupMissing | Dictionary,Missing |    4 |   6.8570 ns | 0.0434 ns | 0.0406 ns |

### Small (16)

|             Method |         Categories | Size |        Mean |     Error |    StdDev |
|------------------- |------------------- |----- |------------:|----------:|----------:|
|   ArrayLookupFirst |        Array,First |   16 |   0.5025 ns | 0.0240 ns | 0.0225 ns |
|    ArrayLookupLast |         Array,Last |   16 |   7.8876 ns | 0.1023 ns | 0.0957 ns |
| ArrayLookupMissing |      Array,Missing |   16 |   7.9863 ns | 0.1239 ns | 0.1159 ns |
|    ListLookupFirst |         List,First |   16 |   0.9854 ns | 0.0665 ns | 0.0556 ns |
|     ListLookupLast |          List,Last |   16 |  13.2018 ns | 0.1387 ns | 0.1297 ns |
|  ListLookupMissing |       List,Missing |   16 |  12.4615 ns | 0.0760 ns | 0.0711 ns |
|    DictLookupFirst |   Dictionary,First |   16 |  10.2966 ns | 0.0568 ns | 0.0474 ns |
|     DictLookupLast |    Dictionary,Last |   16 |   7.5694 ns | 0.1296 ns | 0.1083 ns |
|  DictLookupMissing | Dictionary,Missing |   16 |  14.0606 ns | 0.3506 ns | 0.7984 ns |

### Big(ger) (128)

|             Method |         Categories | Size |        Mean |     Error |    StdDev |
|------------------- |------------------- |----- |------------:|----------:|----------:|
|   ArrayLookupFirst |        Array,First |  128 |   0.5516 ns | 0.0287 ns | 0.0254 ns |
|    ArrayLookupLast |         Array,Last |  128 |  75.8179 ns | 0.4168 ns | 0.3694 ns |
| ArrayLookupMissing |      Array,Missing |  128 |  72.0080 ns | 0.5037 ns | 0.4711 ns |
|    ListLookupFirst |         List,First |  128 |   0.8633 ns | 0.0303 ns | 0.0268 ns |
|     ListLookupLast |          List,Last |  128 | 101.3391 ns | 0.5142 ns | 0.4810 ns |
|  ListLookupMissing |       List,Missing |  128 |  98.3375 ns | 0.4495 ns | 0.3984 ns |
|    DictLookupFirst |   Dictionary,First |  128 |   7.6604 ns | 0.0589 ns | 0.0551 ns |
|     DictLookupLast |    Dictionary,Last |  128 |   7.6246 ns | 0.0501 ns | 0.0444 ns |
|  DictLookupMissing | Dictionary,Missing |  128 |   6.9494 ns | 0.0551 ns | 0.0516 ns |
