using System;
using System.Net.Http;
using OireachtasAPI.Models;
using OireachtasAPI.Services.LoadData;
using Xunit;

namespace TestOireachtasAPI.Services.LoadData
{
    public class HttpMeanServiceTest
    {
        [Fact]
        public void GetAsync_WillReturnResults_WhenUriCorrect()
        {
            var httpMeanService = new HttpMeanService(new HttpClient());

            var data = httpMeanService.GetAsync<BaseResponseModel<MemberResult>>(
                "https://api.oireachtas.ie/v1/members?limit=50");

            Assert.NotNull(data);
        }

        [Fact]
        public void GetAsync_WillThrowArgumentException_WhenUriNotCorrect()
        {
            var httpMeanService = new HttpMeanService(new HttpClient());

            Assert.ThrowsAsync<ArgumentException>(() => httpMeanService.GetAsync<BaseResponseModel<MemberResult>>(
                "://api.oireachtas.ie/v1/members?limit=50"));
        }
    }
}