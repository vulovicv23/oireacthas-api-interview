using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OireachtasAPI.Models
{
    public  class MemberResult
    {
        [JsonProperty("member")] public Member Member { get; set; }
    }

    public  class Member
    {
        [JsonProperty("memberCode")] public string MemberCode { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }

        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("gender")] public string Gender { get; set; }

        [JsonProperty("dateOfDeath")] public object DateOfDeath { get; set; }

        [JsonProperty("fullName")] public string FullName { get; set; }

        [JsonProperty("pId")] public string PId { get; set; }

        [JsonProperty("wikiTitle")] public object WikiTitle { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("memberships")] public List<MembershipElement> Memberships { get; set; }
    }

    public  class MembershipElement
    {
        [JsonProperty("membership")] public MembershipMembership Membership { get; set; }
    }

    public  class MembershipMembership
    {
        [JsonProperty("represents")] public List<RepresentElement> Represents { get; set; }

        [JsonProperty("dateRange")] public DateRange DateRange { get; set; }

        [JsonProperty("offices")] public List<OfficeElement> Offices { get; set; }

        [JsonProperty("parties")] public List<PartyElement> Parties { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("house")] public House House { get; set; }
    }

    public  class OfficeElement
    {
        [JsonProperty("office")] public OfficeOffice Office { get; set; }
    }

    public  class OfficeOffice
    {
        [JsonProperty("officeName")] public OfficeName OfficeName { get; set; }

        [JsonProperty("dateRange")] public DateRange DateRange { get; set; }
    }

    public  class OfficeName
    {
        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public object Uri { get; set; }
    }

    public  class PartyElement
    {
        [JsonProperty("party")] public PartyParty Party { get; set; }
    }

    public  class PartyParty
    {
        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("dateRange")] public DateRange DateRange { get; set; }

        [JsonProperty("partyCode")] public string PartyCode { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }
    }

    public  class RepresentElement
    {
        [JsonProperty("represent")] public RepresentRepresent Represent { get; set; }
    }

    public  class RepresentRepresent
    {
        [JsonProperty("representCode")] public string RepresentCode { get; set; }

        [JsonProperty("showAs")] public string ShowAs { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("representType")] public string RepresentType { get; set; }
    }
}