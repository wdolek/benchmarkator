## To(List|Array)

For context, see @dustinmoris [Tweet about `IEnumerable`](https://twitter.com/dustinmoris/status/1490606359183769604).

|            Method | Categories | NumOfItems |          Mean |        Error |        StdDev |        Median | Ratio | RatioSD |   Gen 0 |   Gen 1 | Allocated |
|------------------ |----------- |----------- |--------------:|-------------:|--------------:|--------------:|------:|--------:|--------:|--------:|----------:|
|  **EnumerableToList** | **Enumerable** |          **4** |      **76.33 ns** |     **2.040 ns** |      **6.014 ns** |      **73.85 ns** |  **1.00** |    **0.00** |  **0.0535** |       **-** |     **112 B** |
| EnumerableToArray | Enumerable |          4 |      85.93 ns |     2.241 ns |      6.609 ns |      82.71 ns |  1.13 |    0.13 |  0.0381 |       - |      80 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  CollectionToList | Collection |          4 |      37.31 ns |     1.221 ns |      3.563 ns |      35.76 ns |  1.00 |    0.00 |  0.0344 |       - |      72 B |
| CollectionToArray | Collection |          4 |      14.89 ns |     0.505 ns |      1.489 ns |      14.29 ns |  0.40 |    0.05 |  0.0191 |       - |      40 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  **EnumerableToList** | **Enumerable** |         **24** |     **290.41 ns** |     **7.574 ns** |     **22.213 ns** |     **280.32 ns** |  **1.00** |    **0.00** |  **0.1950** |       **-** |     **408 B** |
| EnumerableToArray | Enumerable |         24 |     345.85 ns |     9.017 ns |     26.586 ns |     334.22 ns |  1.20 |    0.12 |  0.2179 |       - |     456 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  CollectionToList | Collection |         24 |      44.26 ns |     1.245 ns |      3.671 ns |      42.69 ns |  1.00 |    0.00 |  0.0727 |       - |     152 B |
| CollectionToArray | Collection |         24 |      22.33 ns |     0.796 ns |      2.334 ns |      21.90 ns |  0.51 |    0.07 |  0.0574 |       - |     120 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  **EnumerableToList** | **Enumerable** |        **128** |   **1,132.14 ns** |    **29.036 ns** |     **85.612 ns** |   **1,097.69 ns** |  **1.00** |    **0.00** |  **0.5836** |       **-** |   **1,224 B** |
| EnumerableToArray | Enumerable |        128 |   1,203.32 ns |    31.662 ns |     92.860 ns |   1,167.26 ns |  1.07 |    0.11 |  0.6218 |       - |   1,304 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  CollectionToList | Collection |        128 |      83.50 ns |     2.376 ns |      6.968 ns |      80.22 ns |  1.00 |    0.00 |  0.2716 |       - |     568 B |
| CollectionToArray | Collection |        128 |      58.19 ns |     1.788 ns |      5.272 ns |      56.04 ns |  0.70 |    0.08 |  0.2562 |       - |     536 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  **EnumerableToList** | **Enumerable** |      **16384** | **117,389.92 ns** | **3,501.865 ns** | **10,325.329 ns** | **114,193.90 ns** |  **1.00** |    **0.00** | **58.7158** | **14.6484** | **131,440 B** |
| EnumerableToArray | Enumerable |      16384 | 120,609.77 ns | 3,116.992 ns |  9,042.958 ns | 117,069.10 ns |  1.04 |    0.14 | 62.3779 |       - | 131,760 B |
|                   |            |            |               |              |               |               |       |         |         |         |           |
|  CollectionToList | Collection |      16384 |   6,261.66 ns |   168.182 ns |    493.250 ns |   6,050.47 ns |  1.00 |    0.00 | 29.6249 |  7.4005 |  65,592 B |
| CollectionToArray | Collection |      16384 |   4,370.36 ns |    86.355 ns |    126.579 ns |   4,347.82 ns |  0.70 |    0.06 | 31.2424 |       - |  65,560 B |
