using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OireachtasAPIBenchmark
{
    public class OldProgramImplementation
    {
        public static string LEGISLATION_DATASET = "legislation.json";
        public static string MEMBERS_DATASET = "members.json";


        public static Func<string, dynamic> load = jfname =>
            JsonConvert.DeserializeObject(new StreamReader(jfname).ReadToEnd());

        /// <summary>
        ///     Return bills sponsored by the member with the specified pId
        /// </summary>
        /// <param name="pId">The pId value for the member</param>
        /// <returns>List of bill records</returns>
        public static List<dynamic> filterBillsSponsoredBy(string pId)
        {
            var leg = load(LEGISLATION_DATASET);
            var mem = load(MEMBERS_DATASET);

            var ret = new List<dynamic>();

            foreach (var res in leg["results"])
            {
                var p = res["bill"]["sponsors"];
                foreach (var i in p)
                {
                    string name = i["sponsor"]["by"]["showAs"];
                    foreach (var result in mem["results"])
                    {
                        string fname = result["member"]["fullName"];
                        string rpId = result["member"]["pId"];
                        if (fname == name && rpId == pId) ret.Add(res["bill"]);
                    }
                }
            }

            return ret;
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
        public static List<dynamic> filterBillsByLastUpdated(DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }
    }
}