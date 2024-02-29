using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OireachtasAPI.Services.LoadData
{
    public class HttpMeanService : IHttpMeanService
    {
        private readonly HttpClient _httpClient;

        public HttpMeanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TModel> GetAsync<TModel>(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException("Not valid url");
            }

            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TModel>(responseBody);
        }
    }

    public interface IHttpMeanService
    {
        Task<TModel> GetAsync<TModel>(string uri);
    }
}