```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.5.1 (25F80) [Darwin 25.5.0]
Apple M4 Pro, 1 CPU, 14 logical and 14 physical cores
.NET SDK 8.0.419
  [Host]   : .NET 8.0.25 (8.0.25, 8.0.2526.11203), Arm64 RyuJIT armv8.0-a
  .NET 8.0 : .NET 8.0.25 (8.0.25, 8.0.2526.11203), Arm64 RyuJIT armv8.0-a

Job=.NET 8.0  Runtime=.NET 8.0  

```
| Method                     | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| Baseline_FindDataKeyAndIv  | 211.7 ns | 0.20 ns | 0.18 ns |  1.00 | 0.0572 |     480 B |        1.00 |
| Optimized_FindDataKeyAndIv | 196.9 ns | 0.53 ns | 0.47 ns |  0.93 | 0.0257 |     216 B |        0.45 |
