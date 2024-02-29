using System;
using OireachtasAPI.Models;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.LoadData
{
    public class LocalFileMeanServiceTest
    {
        [Fact]
        public void Load_WillReturnResults_WhenEmbeddedResourceFound()
        {
            var localFileMeanService = new LocalFileMeanService();

            var data = localFileMeanService.Load<BaseResponseModel<MemberResult>>("members.json");

            Assert.NotNull(data);
        }

        [Fact]
        public void Load_WillThrowArgumentException_WhenEmbeddedResourceNotFound()
        {
            var localFileMeanService = new LocalFileMeanService();

            Assert.Throws<ArgumentException>(() => localFileMeanService.Load<BaseResponseModel<MemberResult>>("rabdom.json"));
        }
    }
}