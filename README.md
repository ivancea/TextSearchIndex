# Text search index algorithm

A simple text search index algorithm to allow a "contains" search over any number of indexed elements.

## How to use

- Build: `dotnet build`
- Execute tests: `dotnet test`
- Execute mutation tests: `dotnet stryker`
- Execute benchmark: `dotnet run -c Release --project TextSearch.Benchmarks`


## Benchmarks

### [IndexSearchRandomBenchmark](TextSearch.Benchmarks/IndexSearchRandomBenchmark.cs)

This benchmark tests the performance over many randomly generated elements.
As elements and serach terms are chosen at random, the results may vary.

#### Results

``` Log
05/02/2023
BenchmarkDotNet=v0.13.4, OS=Windows 10 (10.0.19045.2546)
Intel Core i5-4460 CPU 3.20GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK=7.0.100
```

##### 100.000 elements

|         Method | SearchTextLength |        Mean |      Error |     StdDev |      Median | Ratio |
|--------------- |----------------- |------------:|-----------:|-----------:|------------:|------:|
| SearchWithLinq |                3 | 6,279.35 us | 111.577 us | 124.018 us | 6,316.45 us |  1.00 |
|         Search |                3 | 2,238.39 us |  60.865 us | 179.463 us | 2,183.98 us |  0.37 |
|                |                  |             |            |            |             |       |
| SearchWithLinq |                5 | 6,261.03 us | 114.967 us | 101.915 us | 6,254.11 us |  1.00 |
|         Search |                5 | 7,694.39 us | 151.883 us | 266.011 us | 7,722.72 us |  1.21 |
|                |                  |             |            |            |             |       |
| SearchWithLinq |               20 | 5,844.98 us |  78.683 us |  73.600 us | 5,841.20 us |  1.00 |
|         Search |               20 |    67.68 us |   0.545 us |   0.510 us |    67.58 us |  0.01 |

##### 1.000.000 elements

|         Method | SearchTextLength |        Mean |     Error |    StdDev |      Median | Ratio |
|--------------- |----------------- |------------:|----------:|----------:|------------:|------:|
| SearchWithLinq |                3 | 47,365.6 us | 806.36 us | 754.27 us | 47,195.3 us |  1.00 |
|         Search |                3 |  9,384.4 us | 184.67 us | 360.18 us |  9,327.7 us |  0.20 |
|                |                  |             |           |           |             |       |
| SearchWithLinq |                5 | 47,098.0 us | 922.50 us | 862.90 us | 46,851.8 us |  1.00 |
|         Search |                5 |  1,205.3 us | 107.32 us | 302.69 us |  1,057.2 us |  0.03 |
|                |                  |             |           |           |             |       |
| SearchWithLinq |               20 | 46,082.5 us | 651.16 us | 609.09 us | 46,004.5 us |  1.00 |
|         Search |               20 |    858.3 us |   5.44 us |   5.09 us |    856.8 us |  0.02 |