using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace OireachtasAPIBenchmark
{
    [SimpleJob(RunStrategy.ColdStart, iterationCount: 100)]
    [MemoryDiagnoser]
    public class FilterBillsSponsoredByBenchmark
    {
        private static readonly string pId = "IvanaBacik";

        [Benchmark(Baseline = true)]
        public List<dynamic> OldFilterBillsSponsoredBy()
        {
            return OldProgramImplementation.filterBillsSponsoredBy(pId);
        }

        [Benchmark]
        public async Task<List<dynamic>> NewFilterBillsSponsoredBy()
        {
            return await OireachtasAPI.Program.filterBillsSponsoredBy(pId);
        }
    }
}