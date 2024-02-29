using System;
using System.Net.Http;
using System.Threading.Tasks;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.LoadData.LoadDataServiceTest
{
    public class LoadDataServiceLegislationTest
    {
        [Fact]
        public async Task LoadLegislations_WillReturnResults_FromFile()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                true, null);

            var loadLegislationsResult = await loadDataService.LoadLegislations();

            Assert.NotNull(loadLegislationsResult);
            Assert.True(loadLegislationsResult.UseLocalFile);
            Assert.NotEmpty(loadLegislationsResult.ResponseModel.Results);
        }

        [Fact]
        public async Task LoadLegislations_WillReturnResults_FromWeb()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                false, "https://api.oireachtas.ie/v1/");

            var loadLegislationsResult = await loadDataService.LoadLegislations();

            Assert.NotNull(loadLegislationsResult);
            Assert.False(loadLegislationsResult.UseLocalFile);
            Assert.NotEmpty(loadLegislationsResult.ResponseModel.Results);
        }

        [Fact]
        public async Task LoadLegislations_WillReturnResults_FromWebWithSinceAndUntil()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                false, "https://api.oireachtas.ie/v1/");

            var loadLegislationsResult =
                await loadDataService.LoadLegislations(new DateTime(2019, 1, 1), new DateTime(2021, 1, 5));

            Assert.NotNull(loadLegislationsResult);
            Assert.False(loadLegislationsResult.UseLocalFile);
            Assert.NotEmpty(loadLegislationsResult.ResponseModel.Results);
        }

        [Fact]
        public async Task LoadLegislations_WillReturnResults_FromWebWithSince()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                false, "https://api.oireachtas.ie/v1/");

            var loadLegislationsResult = await loadDataService.LoadLegislations(new DateTime(2019, 1, 1), null);

            Assert.NotNull(loadLegislationsResult);
            Assert.False(loadLegislationsResult.UseLocalFile);
            Assert.NotEmpty(loadLegislationsResult.ResponseModel.Results);
        }
    }
}