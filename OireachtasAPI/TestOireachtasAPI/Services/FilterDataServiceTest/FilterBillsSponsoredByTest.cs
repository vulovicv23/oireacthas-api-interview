using System.Net.Http;
using System.Threading.Tasks;
using OireachtasAPI.Services;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.FilterDataServiceTest
{
    public class FilterBillsSponsoredByTest
    {
        [Fact]
        public async Task FilterBillsSponsoredBy_WillReturnResult_IfPIdIsFound()
        {
            var filterDataService = new FilterDataService(new LoadDataService(new HttpMeanService(new HttpClient()),
                new LocalFileMeanService(), true, null));

            var bills = await filterDataService.FilterBillsSponsoredBy("IvanaBacik");

            Assert.NotEmpty(bills);
        }

        [Fact]
        public async Task FilterBillsSponsoredBy_WillReturnEmptyResult_IfPIdIsNotFound()
        {
            var filterDataService = new FilterDataService(new LoadDataService(new HttpMeanService(new HttpClient()),
                new LocalFileMeanService(), true, null));

            var bills = await filterDataService.FilterBillsSponsoredBy("IvanaBacik123");

            Assert.Empty(bills);
        }
    }
}