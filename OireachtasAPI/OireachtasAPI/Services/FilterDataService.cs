using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OireachtasAPI.Models;
using OireachtasAPI.Services.LoadData;

namespace OireachtasAPI.Services
{
    public class FilterDataService : IFilterDataService
    {
        private readonly ILoadDataService _loadDataService;

        public FilterDataService(ILoadDataService loadDataService)
        {
            _loadDataService = loadDataService;
        }

        public async Task<List<Bill>> FilterBillsSponsoredBy(string pId)
        {
            var loadLegislationDataResult = await _loadDataService.LoadLegislations();
            var loadMembersDataResult = await _loadDataService.LoadMembers();

            var memberDict = new HashSet<string>();
            foreach (var result in loadMembersDataResult.ResponseModel.Results)
            {
                memberDict.Add($"{result.Member.FullName}_{result.Member.PId}");
            }

            var bills = new List<Bill>();

            foreach (var legislation in loadLegislationDataResult.ResponseModel.Results)
            {
                var sponsors = legislation.Bill.Sponsors;
                foreach (var sponsor in sponsors)
                {
                    string shownAsName = sponsor.Sponsor.By.ShowAs;
                    if (memberDict.TryGetValue($"{shownAsName}_{pId}", out string _))
                    {
                        bills.Add(legislation.Bill);
                        break;
                    }
                }
            }

            return bills;
        }

        public async Task<List<Bill>> FilterBillsByLastUpdated(DateTime since, DateTime? until)
        {
            if (until == null || until == DateTime.MinValue)
            {
                until = DateTime.UtcNow;
            }

            if (since > until)
            {
                throw new ArgumentException("Since cannot be greater than until");
            }

            var loadLegislationDataResult = await _loadDataService.LoadLegislations(since, until);

            if (!loadLegislationDataResult.UseLocalFile)
            {
                return loadLegislationDataResult.ResponseModel.Results.Select(s => s.Bill).ToList();
            }

            var bills = new List<Bill>();

            foreach (var legislation in loadLegislationDataResult.ResponseModel.Results)
            {
                if (legislation.Bill.LastUpdated >= since && legislation.Bill.LastUpdated <= until)
                {
                    bills.Add(legislation.Bill);
                }
            }

            return bills;
        }
    }

    public interface IFilterDataService
    {
        /// <summary>
        /// Return bills sponsored by the member with the specified pId
        /// </summary>
        /// <param name="pId">The pId value for the member</param>
        /// <returns>List of bill records</returns>
        Task<List<Bill>> FilterBillsSponsoredBy(string pId);

        /// <summary>
        /// Return bills updated within the specified date range
        /// </summary>
        /// <param name="since">The lastUpdated value for the bill should be greater than or equal to this date</param>
        /// <param name="until">The lastUpdated value for the bill should be less than or equal to this date.If unspecified, until will default to today's date</param>
        /// <returns>List of bill records</returns>
        Task<List<Bill>> FilterBillsByLastUpdated(DateTime since, DateTime? until);
    }
}