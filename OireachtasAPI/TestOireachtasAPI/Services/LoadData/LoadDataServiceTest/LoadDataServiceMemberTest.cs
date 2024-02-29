using System.Net.Http;
using System.Threading.Tasks;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.LoadData.LoadDataServiceTest
{
    public class LoadDataServiceMemberTest
    {
        [Fact]
        public async Task LoadMembers_WillReturnResults_FromFile()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                true, null);

            var loadMembersResult = await loadDataService.LoadMembers();

            Assert.NotNull(loadMembersResult);
            Assert.True(loadMembersResult.UseLocalFile);
            Assert.NotEmpty(loadMembersResult.ResponseModel.Results);
        }

        [Fact]
        public async Task LoadMembers_WillReturnResults_FromWeb()
        {
            var loadDataService = new LoadDataService(new HttpMeanService(new HttpClient()), new LocalFileMeanService(),
                false, "https://api.oireachtas.ie/v1/");

            var loadMembersResult = await loadDataService.LoadMembers();

            Assert.NotNull(loadMembersResult);
            Assert.False(loadMembersResult.UseLocalFile);
            Assert.NotEmpty(loadMembersResult.ResponseModel.Results);
        }
    }
}