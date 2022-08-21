## Converting `S.T.Json.JsonDocument` to `MongoDB.Bson.BsonDocument`

What's the fastest way converting loose `JsonDocument` to `BsonDocument`? Let's find out:

- serializing `JsonDocument` to string, and then parsing string to `BsonDocument`
- using `JsonDocument.WriteTo` to write serialized document to buffer, creating string out of that buffer and then parsing string to `BsonDocument`
- using `JsonDocument.WriteTo` to write serialized document to buffer and then using same buffer do deserialize `BsonDocument` out of it

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1889 (21H2)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.303
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
```
|                                                            Method |   Size |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen 0 | Allocated |
|------------------------------------------------------------------ |------- |-----------:|----------:|----------:|------:|--------:|--------:|----------:|
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; |  Small |   2.490 μs | 0.0328 μs | 0.0307 μs |  1.00 |    0.00 |  0.8545 |      4 KB |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; |  Small |   2.749 μs | 0.0341 μs | 0.0319 μs |  1.10 |    0.02 |  1.0338 |      4 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; |  Small |   3.034 μs | 0.0583 μs | 0.0545 μs |  1.22 |    0.02 |  2.3079 |      9 KB |
|                                                                   |        |            |           |           |       |         |         |           |
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; | Medium |  20.626 μs | 0.2212 μs | 0.1961 μs |  1.00 |    0.00 |  6.7139 |     27 KB |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; | Medium |  21.268 μs | 0.4090 μs | 0.4200 μs |  1.03 |    0.03 |  8.4229 |     34 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; | Medium |  22.066 μs | 0.4327 μs | 0.5473 μs |  1.07 |    0.03 |  9.8572 |     40 KB |
|                                                                   |        |            |           |           |       |         |         |           |
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; |  Large | 111.541 μs | 1.2759 μs | 1.0655 μs |  1.00 |    0.00 | 34.5459 |    141 KB |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; |  Large | 112.895 μs | 1.5988 μs | 1.3351 μs |  1.01 |    0.01 | 42.9688 |    176 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; |  Large | 138.318 μs | 2.4092 μs | 2.2535 μs |  1.24 |    0.02 | 53.2227 |    218 KB |
