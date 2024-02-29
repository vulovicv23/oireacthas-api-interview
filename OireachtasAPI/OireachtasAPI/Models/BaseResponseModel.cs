using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OireachtasAPI.Models
{
    public partial class BaseResponseModel<TResults>
    {
        [JsonProperty("head")] public Head Head { get; set; }

        [JsonProperty("results")] public List<TResults> Results { get; set; }
    }

    public partial class Head
    {
        [JsonProperty("counts")] public Counts Counts { get; set; }

        [JsonProperty("dateRange")] public DateRange DateRange { get; set; }

        [JsonProperty("lang")] public string Lang { get; set; }
    }

    public partial class Counts
    {
        [JsonProperty("billCount")] public long BillCount { get; set; }

        [JsonProperty("resultCount")] public long ResultCount { get; set; }
    }

    public partial class DateRange
    {
        [JsonProperty("start")] public DateTime? Start { get; set; }

        [JsonProperty("end")] public DateTime? End { get; set; }
    }
}