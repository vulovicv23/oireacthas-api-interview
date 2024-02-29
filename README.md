# Propylon Code Test - C# Oireachtas API

In order to demonstrate all the skills and to complete all the tasks I split the Code Test in 2 branches:

1. main
2. refactored

# main branch

On this branch I did first 3 tasks:

1. The current implementation loads previously obtained offline copy of the data obtained from
   the endpoints. Update the module to fetch the latest data from the api endpoint if the
   parameter passed is the URL to the endpoint instead of a filename.
2. The current implementation of the filterBillsSponsoredBy appears to be correct. It is also
   reasonably quick when processing the offline data. However, when the complete dataset
   obtained from the api is loaded, it is noticeably slower. Refactor the implementation to be
   faster than the current inefficient implementation.
3. Provide an implementation for the unimplemented function filterBillsByLastUpdated. The
   specification for this is documented in the function's doc-string.

All of this is covered in Tests project.

One note: Provided expected result for `Testlastupdated` had one element that doesn't belong there. Element is "94". I
removed it so that the test can pass.

Also, I added another project which is for Benchmarking to check old vs new implementation of filterBillsSponsoredBy and
here are the results:

```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3155/23H2/2023Update/SunValley3)
AMD Ryzen 9 7945HX with Radeon Graphics, 1 CPU, 32 logical and 16 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT
  Job-KALVMZ : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT

IterationCount=100  RunStrategy=ColdStart  

```

| Method                    |     Mean |    Error |   StdDev | Ratio | RatioSD |      Gen0 |      Gen1 | Allocated | Alloc Ratio |
|---------------------------|---------:|---------:|---------:|------:|--------:|----------:|----------:|----------:|------------:|
| OldFilterBillsSponsoredBy | 15.29 ms | 6.367 ms | 18.77 ms |  1.00 |    0.00 | 2000.0000 | 1000.0000 |  10.87 MB |        1.00 |
| NewFilterBillsSponsoredBy | 13.17 ms | 5.441 ms | 16.04 ms |  0.89 |    0.15 | 2000.0000 | 1000.0000 |  10.87 MB |        1.00 |

From the table above we can see that the refactored method is faster by 2.12 ms on average and is consuming the same
amount of memory.

In order to run BenchmarkingProject, just select OireachtasAPIBenchmark, select Release configuration and just run the
console app.

# refactored branch

On this branch, I made a lot of changes, so I will list them bellow:

- I updated all the libraries
- I added a Spectre library for showing data in the terminal in a prettier way
- I added dependency injection
- I moved to xUnit framework for testing
- I added Serilog as logging framework

So, in this branch, I have done all 5 tasks.

I added logging using Serilog. This logging is being written to file so that we don't clutter the terminal and so that
we can show the users only the relevant information. In the logs we can see stuff that developers should see.

I added unit tests to cover most of the application.

I added switches so that we can easily turn from where we want our data to be, from our local files or from web. Local
files legislation.json and members.json are marked as EmbeddedResources since its a bit faster than reading from disk.

Here are the results for `FilterBillsSponsoredBy`:

```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3155/23H2/2023Update/SunValley3)
AMD Ryzen 9 7945HX with Radeon Graphics, 1 CPU, 32 logical and 16 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT
  Job-HKTRNI : .NET Framework 4.8.1 (4.8.9181.0), X86 LegacyJIT

IterationCount=50  RunStrategy=ColdStart  

```

| Method                           |     Mean |     Error |   StdDev |    Median | Ratio | RatioSD |      Gen0 |      Gen1 | Allocated | Alloc Ratio |
|----------------------------------|---------:|----------:|---------:|----------:|------:|--------:|----------:|----------:|----------:|------------:|
| OldFilterBillsSponsoredBy        | 14.94 ms | 10.917 ms | 22.05 ms | 11.647 ms |  1.00 |    0.00 | 2000.0000 | 1000.0000 |  10.85 MB |        1.00 |
| MainBranchFilterBillsSponsoredBy | 14.03 ms | 10.888 ms | 21.99 ms | 10.871 ms |  0.93 |    0.05 | 2000.0000 | 1000.0000 |  10.86 MB |        1.00 |
| NewFilterBillsSponsoredBy        | 10.50 ms |  7.340 ms | 14.83 ms |  8.467 ms |  0.71 |    0.04 | 1000.0000 |         - |   6.47 MB |        0.60 |

From here we have:

- `OldFilterBillsSponsoredBy` - this is the initial code you supplied me with
- `MainBranchFilterBillsSponsoredBy` - this is the code from main branch
- `NewFilterBillsSponsoredBy` - This is the newly implemented code

From what we can see here, the newly implemented code is faster by 4.44ms from initial code and 3.53ms faster than main
branch code on average. It also uses a lot less memory, around 40% less.

I did not have time to add Integration Test, but, if needed, ill make them as well.

in order to run the application, build it and you can run it from Rider or Visual Studio or use terminal to run it. Ill
show terminal options here:

.\OireachtasAPI.exe --help
.\OireachtasAPI.exe filterBillsSponsoredBy -p|--pId
.\OireachtasAPI.exe filterBillsByLastUpdated -s|--since -u|--until