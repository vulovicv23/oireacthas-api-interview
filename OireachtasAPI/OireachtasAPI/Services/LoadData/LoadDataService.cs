using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OireachtasAPI.Models;

namespace OireachtasAPI.Services.LoadData
{
    public sealed class LoadDataService : ILoadDataService
    {
        private readonly IHttpMeanService _httpMeanService;
        private readonly ILocalFileMeanService _localFileMeanService;
        private readonly bool _useLocalFiles;
        private readonly string _oireachtasApi;

        public LoadDataService(IHttpMeanService httpMeanService, ILocalFileMeanService localFileMeanService,
            bool useLocalFiles, string oireachtasApi)
        {
            _httpMeanService = httpMeanService;
            _localFileMeanService = localFileMeanService;
            _useLocalFiles = useLocalFiles;
            _oireachtasApi = oireachtasApi;
        }

        public async Task<LoadDataResult<LegislationResult>> LoadLegislations(DateTime? lastUpdatedSince = null,
            DateTime? lastUpdatedUntil = null)
        {
            string input;

            if (_useLocalFiles)
            {
                input = "legislation.json";
            }
            else
            {
                var uriBuilder = new StringBuilder();
                uriBuilder.Append(_oireachtasApi);
                uriBuilder.Append("legislation?limit=50");

                if (lastUpdatedSince != null && lastUpdatedUntil != null)
                {
                    uriBuilder.Append($"&date_start={((DateTime)lastUpdatedSince).ToString("yyyy-MM-dd")}");

                    uriBuilder.Append($"&date_end={((DateTime)lastUpdatedUntil).ToString("yyyy-MM-dd")}");
                }

                input = uriBuilder.ToString();
            }

            var data = await LoadData<BaseResponseModel<LegislationResult>>(input);

            return new LoadDataResult<LegislationResult>(data, _useLocalFiles);
        }

        public async Task<LoadDataResult<MemberResult>> LoadMembers()
        {
            string input;

            if (_useLocalFiles)
            {
                input = "members.json";
            }
            else
            {
                var uriBuilder = new StringBuilder();
                uriBuilder.Append(_oireachtasApi);
                uriBuilder.Append("members?limit=50");

                input = uriBuilder.ToString();
            }

            var data = await LoadData<BaseResponseModel<MemberResult>>(input);

            return new LoadDataResult<MemberResult>(data, _useLocalFiles);
        }

        private async Task<TModel> LoadData<TModel>(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input is empty");
            }

            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                try
                {
                    var apiResult = await _httpMeanService.GetAsync<TModel>(input);

                    return apiResult;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }

            var fileResult = _localFileMeanService.Load<TModel>(input);

            return fileResult;
        }
    }

    public interface ILoadDataService
    {
        /// <summary>
        /// Load legislation data for the given input
        /// </summary>
        /// <param name="lastUpdatedSince">Parameter for automatically filtering. This will trigger only for results returned from web.</param>
        /// <param name="lastUpdatedUntil">Parameter for automatically filtering. This will trigger only for results returned from web.</param>
        /// <returns>Response from the given input</returns>
        Task<LoadDataResult<LegislationResult>> LoadLegislations(DateTime? lastUpdatedSince = null,
            DateTime? lastUpdatedUntil = null);

        Task<LoadDataResult<MemberResult>> LoadMembers();
    }
}