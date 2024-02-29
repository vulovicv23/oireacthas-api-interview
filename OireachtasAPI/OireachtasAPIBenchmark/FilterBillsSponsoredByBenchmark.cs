using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using OireachtasAPI.Models;
using OireachtasAPI.Services;
using OireachtasAPI.Services.LoadData;

namespace OireachtasAPIBenchmark
{
    [SimpleJob(RunStrategy.ColdStart, iterationCount: 50)]
    [MemoryDiagnoser]
    public class FilterBillsSponsoredByBenchmark
    {
        private static string pId = "IvanaBacik";

        private readonly IFilterDataService _filterDataService =
            new FilterDataService(
                new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(), true, null));

        [Benchmark(Baseline = true)]
        public List<dynamic> OldFilterBillsSponsoredBy()
        {
            return OldProgramImplementation.filterBillsSponsoredBy(pId);
        }
        
        [Benchmark]
        public async Task<List<dynamic>> MainBranchFilterBillsSponsoredBy()
        {
            return await MainBranchProgramImplementation.filterBillsSponsoredBy(pId);
        }

        [Benchmark]
        public async Task<List<Bill>> NewFilterBillsSponsoredBy()
        {
            return await _filterDataService.FilterBillsSponsoredBy(pId);
        }
    }
}