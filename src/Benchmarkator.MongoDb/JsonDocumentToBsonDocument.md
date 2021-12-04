## Converting `S.T.Json.JsonDocument` to `MongoDB.Bson.BsonDocument`

What's the fastest way converting loose `JsonDocument` to `BsonDocument`? Let's find out:

- serializing `JsonDocument` to string, and then parsing string to `BsonDocument`
- using `JsonDocument.WriteTo` to write serialized document to buffer, creating string out of that buffer and then parsing string to `BsonDocument`
- using `JsonDocument.WriteTo` to write serialized document to buffer and then using same buffer do deserialize `BsonDocument` out of it

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1387 (21H2)
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
|                                                            Method |   Size |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen 0 | Allocated |
|------------------------------------------------------------------ |------- |-----------:|----------:|----------:|------:|--------:|--------:|----------:|
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; |  Small |   2.876 μs | 0.0517 μs | 0.0432 μs |  1.00 |    0.00 |  1.7128 |      4 KB |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; |  Small |   2.922 μs | 0.0258 μs | 0.0241 μs |  1.02 |    0.02 |  2.0676 |      4 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; |  Small |   3.368 μs | 0.0426 μs | 0.0650 μs |  1.18 |    0.04 |  4.6158 |      9 KB |
|                                                                   |        |            |           |           |       |         |         |           |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; | Medium |  22.151 μs | 0.2180 μs | 0.1821 μs |  1.00 |    0.02 | 16.8762 |     34 KB |
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; | Medium |  22.262 μs | 0.2966 μs | 0.2775 μs |  1.00 |    0.00 | 13.4277 |     27 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; | Medium |  24.554 μs | 0.1972 μs | 0.1748 μs |  1.10 |    0.02 | 19.8059 |     40 KB |
|                                                                   |        |            |           |           |       |         |         |           |
| &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; string -&gt; BsonDocument&#39; |  Large | 105.522 μs | 0.6614 μs | 0.6187 μs |  0.99 |    0.01 | 74.8291 |    153 KB |
|                          &#39;JsonDocument -&gt; string -&gt; BsonDocument&#39; |  Large | 106.423 μs | 1.1512 μs | 1.0768 μs |  1.00 |    0.00 | 59.4482 |    122 KB |
|           &#39;JsonDocument: WriteTo -&gt; MemoryStream -&gt; BsonDocument&#39; |  Large | 126.300 μs | 1.8058 μs | 1.6891 μs |  1.19 |    0.02 | 83.9844 |    172 KB |
