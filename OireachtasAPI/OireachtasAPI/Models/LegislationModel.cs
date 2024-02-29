using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OireachtasAPI.Models
{
    public class LegislationResult
    {
        [JsonProperty("bill")] public Bill Bill { get; set; }

        [JsonProperty("billSort")] public BillSort BillSort { get; set; }

        [JsonProperty("contextDate")] public DateTime ContextDate { get; set; }
    }

    public class Bill
    {
        [JsonProperty("act")] public Act Act { get; set; }

        [JsonProperty("amendmentLists")] public List<AmendmentListElement> AmendmentLists { get; set; }

        [JsonProperty("billNo")] public string BillNo { get; set; }

        [JsonProperty("billType")] public string BillType { get; set; }

        [JsonProperty("billTypeURI")] public Uri BillTypeUri { get; set; }

        [JsonProperty("billYear")] public string BillYear { get; set; }

        [JsonProperty("debates")] public List<Debate> Debates { get; set; }

        [JsonProperty("events")] public List<MostRecentStage> Events { get; set; }

        [JsonProperty("lastUpdated")] public DateTime? LastUpdated { get; set; }

        [JsonProperty("longTitleEn")] public string LongTitleEn { get; set; }

        [JsonProperty("longTitleGa")] public string LongTitleGa { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("methodURI")] public Uri MethodUri { get; set; }

        [JsonProperty("mostRecentStage")] public MostRecentStage MostRecentStage { get; set; }

        [JsonProperty("originHouse")] public OriginHouse OriginHouse { get; set; }

        [JsonProperty("originHouseURI")] public Uri OriginHouseUri { get; set; }

        [JsonProperty("relatedDocs")] public List<RelatedDoc> RelatedDocs { get; set; }

        [JsonProperty("shortTitleEn")] public string ShortTitleEn { get; set; }

        [JsonProperty("shortTitleGa")] public string ShortTitleGa { get; set; }

        [JsonProperty("source")] public string Source { get; set; }

        [JsonProperty("sourceURI")] public Uri SourceUri { get; set; }

        [JsonProperty("sponsors")] public List<SponsorElement> Sponsors { get; set; }

        [JsonProperty("stages")] public List<MostRecentStage> Stages { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("statusURI")] public Uri StatusUri { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("versions")] public List<Version> Versions { get; set; }
    }

    public class Act
    {
        [JsonProperty("actNo")] public string ActNo { get; set; }

        [JsonProperty("actYear")] public string ActYear { get; set; }

        [JsonProperty("dateSigned")] public DateTime? DateSigned { get; set; }

        [JsonProperty("longTitleEn")] public string LongTitleEn { get; set; }

        [JsonProperty("longTitleGa")] public string LongTitleGa { get; set; }

        [JsonProperty("shortTitleEn")] public string ShortTitleEn { get; set; }

        [JsonProperty("shortTitleGa")] public string ShortTitleGa { get; set; }

        [JsonProperty("statutebookURI")] public Uri StatutebookUri { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class AmendmentListElement
    {
        [JsonProperty("amendmentList")] public AmendmentListAmendmentList AmendmentList { get; set; }
    }

    public class AmendmentListAmendmentList
    {
        [JsonProperty("amendmentTypeUri")] public AmendmentTypeUri AmendmentTypeUri { get; set; }

        [JsonProperty("chamber")] public OriginHouse Chamber { get; set; }

        [JsonProperty("date")] public DateTime? Date { get; set; }

        [JsonProperty("formats")] public Formats Formats { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("stage")] public OriginHouse Stage { get; set; }

        [JsonProperty("stageNo")] public string StageNo { get; set; }
    }

    public class AmendmentTypeUri
    {
        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class OriginHouse
    {
        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class Formats
    {
        [JsonProperty("pdf")] public AmendmentTypeUri Pdf { get; set; }

        [JsonProperty("xml")] public object Xml { get; set; }
    }

    public class Debate
    {
        [JsonProperty("chamber")] public OriginHouse Chamber { get; set; }

        [JsonProperty("date")] public DateTime? Date { get; set; }

        [JsonProperty("debateSectionId")] public string DebateSectionId { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class MostRecentStage
    {
        [JsonProperty("event")] public Event Event { get; set; }
    }

    public class Event
    {
        [JsonProperty("chamber")] public Chamber Chamber { get; set; }

        [JsonProperty("dates")] public List<Date> Dates { get; set; }

        [JsonProperty("house")] public House House { get; set; }

        [JsonProperty("progressStage", NullValueHandling = NullValueHandling.Ignore)]
        public long? ProgressStage { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("stageCompleted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? StageCompleted { get; set; }

        [JsonProperty("stageOutcome")] public string StageOutcome { get; set; }

        [JsonProperty("stageURI", NullValueHandling = NullValueHandling.Ignore)]
        public Uri StageUri { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("eventURI", NullValueHandling = NullValueHandling.Ignore)]
        public Uri EventUri { get; set; }
    }

    public class Chamber
    {
        [JsonProperty("chamberCode")] public string ChamberCode { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class Date
    {
        [JsonProperty("date")] public DateTime? DateDate { get; set; }
    }

    public class House
    {
        [JsonProperty("chamberCode")] public string ChamberCode { get; set; }

        [JsonProperty("chamberType")] public string ChamberType { get; set; }

        [JsonProperty("houseCode")] public string HouseCode { get; set; }

        [JsonProperty("houseNo")] public string HouseNo { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class RelatedDoc
    {
        [JsonProperty("relatedDoc")] public VersionClass RelatedDocRelatedDoc { get; set; }
    }

    public class VersionClass
    {
        [JsonProperty("date")] public DateTime? Date { get; set; }

        [JsonProperty("docType")] public string DocType { get; set; }

        [JsonProperty("formats")] public Formats Formats { get; set; }

        [JsonProperty("lang")] public string Lang { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public class SponsorElement
    {
        [JsonProperty("sponsor")] public SponsorSponsor Sponsor { get; set; }
    }

    public class SponsorSponsor
    {
        [JsonProperty("as")] public OriginHouse As { get; set; }

        [JsonProperty("by")] public OriginHouse By { get; set; }
    }

    public class Version
    {
        [JsonProperty("version")] public VersionClass VersionVersion { get; set; }
    }

    public class BillSort
    {
        [JsonProperty("actNoSort")] public long? ActNoSort { get; set; }

        [JsonProperty("actShortTitleEnSort")] public string ActShortTitleEnSort { get; set; }

        [JsonProperty("actShortTitleGaSort")] public string ActShortTitleGaSort { get; set; }

        [JsonProperty("actYearSort")] public long? ActYearSort { get; set; }

        [JsonProperty("billNoSort")] public long BillNoSort { get; set; }

        [JsonProperty("billShortTitleEnSort")] public string BillShortTitleEnSort { get; set; }

        [JsonProperty("billShortTitleGaSort")] public string BillShortTitleGaSort { get; set; }

        [JsonProperty("billYearSort")] public long BillYearSort { get; set; }
    }
}