``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.985 (21H1/May2021Update)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET 6.0.0 (6.0.21.25307), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.25307), X64 RyuJIT


```
|                  Method |     Mean |   Error |   StdDev | Ratio | RatioSD |
|------------------------ |---------:|--------:|---------:|------:|--------:|
| StringConcatenationTest | 499.5 ns | 9.85 ns | 18.97 ns |  1.00 |    0.00 |
|       StringBuilderTest | 320.9 ns | 4.45 ns |  3.71 ns |  0.67 |    0.04 |
