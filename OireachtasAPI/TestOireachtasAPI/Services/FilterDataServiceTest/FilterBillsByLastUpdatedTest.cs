using System;
using System.Net.Http;
using System.Threading.Tasks;
using OireachtasAPI.Services;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.FilterDataServiceTest
{
    public class FilterBillsByLastUpdatedTest
    {
        [Fact]
        public async Task FilterBillsByLastUpdated_WillReturnResult_IfInRange()
        {
            var filterDataService = new FilterDataService(new LoadDataService(new HttpMeanService(new HttpClient()),
                new LocalFileMeanService(), true, null));

            var bills = await filterDataService.FilterBillsByLastUpdated(new DateTime(2000, 1, 1), null);

            Assert.NotEmpty(bills);
        }

        [Fact]
        public async Task FilterBillsByLastUpdated_WillReturnEmptyResult_IfNotInRange()
        {
            var filterDataService = new FilterDataService(new LoadDataService(new HttpMeanService(new HttpClient()),
                new LocalFileMeanService(), true, null));

            var bills = await filterDataService.FilterBillsByLastUpdated(new DateTime(2050, 1, 1),
                new DateTime(2050, 2, 1));

            Assert.Empty(bills);
        }
    }
}