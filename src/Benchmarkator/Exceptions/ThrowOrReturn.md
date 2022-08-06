## Throw or return?

|                   Method |           Mean |         Error |        StdDev |         Median |    Ratio | RatioSD |
|------------------------- |---------------:|--------------:|--------------:|---------------:|---------:|--------:|
| SuccessWithSuccessResult |       303.3 ns |       5.06 ns |       4.73 ns |       304.2 ns |     0.18 |    0.01 |
|  FailureWithFailedResult |       306.2 ns |       2.82 ns |       2.64 ns |       306.1 ns |     0.18 |    0.00 |
|   SuccessWithNoException |     1,675.0 ns |      32.56 ns |      33.44 ns |     1,684.3 ns |     1.00 |    0.00 |
|     FailureWithException | 7,310,050.8 ns | 144,603.92 ns | 375,844.82 ns | 7,196,292.2 ns | 4,498.72 |  177.04 |
