using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OireachtasAPI
{
    public class Program
    {
        public static string LEGISLATION_DATASET = "legislation.json";
        public static string MEMBERS_DATASET = "members.json";

        public static Func<string, Task<dynamic>> load = async jfname =>
        {
            if (Uri.IsWellFormedUriString(jfname, UriKind.Absolute))
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(jfname);
                        response.EnsureSuccessStatusCode();

                        var responseBody = await response.Content.ReadAsStringAsync();
                        return JObject.Parse(responseBody);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return null;
                }

            return JsonConvert.DeserializeObject(new StreamReader(jfname).ReadToEnd());
        };

        private static void Main(string[] args)
        {
        }

        /// <summary>
        ///     Return bills sponsored by the member with the specified pId
        /// </summary>
        /// <param name="pId">The pId value for the member</param>
        /// <returns>List of bill records</returns>
        public static async Task<List<dynamic>> filterBillsSponsoredBy(string pId)
        {
            var legislations = await load(LEGISLATION_DATASET);
            var members = await load(MEMBERS_DATASET);

            var memberHashSet = new HashSet<string>();
            foreach (var result in members["results"])
            {
                string firstName = result["member"]["fullName"];
                string rpId = result["member"]["pId"];
                memberHashSet.Add($"{firstName}_{rpId}");
            }

            var bills = new List<dynamic>();

            foreach (var legislation in legislations["results"])
            {
                var sponsors = legislation["bill"]["sponsors"];
                foreach (var sponsor in sponsors)
                {
                    string shownAsName = sponsor["sponsor"]["by"]["showAs"];
                    if (memberHashSet.TryGetValue($"{shownAsName}_{pId}", out var _))
                    {
                        bills.Add(legislation["bill"]);
                        break;
                    }
                }
            }

            return bills;
        }

        /// <summary>
        ///     Return bills updated within the specified date range
        /// </summary>
        /// <param name="since">The lastUpdated value for the bill should be greater than or equal to this date</param>
        /// <param name="until">
        ///     The lastUpdated value for the bill should be less than or equal to this date.If unspecified, until
        ///     will default to today's date
        /// </param>
        /// <returns>List of bill records</returns>
        public static async Task<List<dynamic>> filterBillsByLastUpdated(DateTime since, DateTime? until)
        {
            if (until == null || until == DateTime.MinValue) until = DateTime.UtcNow;

            if (since > until) throw new ArgumentException("Since cannot be greater than until");

            var legislations = await load(LEGISLATION_DATASET);

            var bills = new List<dynamic>();

            foreach (var legislation in legislations["results"])
            {
                if (!DateTime.TryParse(legislation["bill"]["lastUpdated"].ToString(), out DateTime lastUpdated))
                    continue;

                if (lastUpdated >= since && lastUpdated <= until) bills.Add(legislation["bill"]);
            }

            return bills;
        }
    }
}