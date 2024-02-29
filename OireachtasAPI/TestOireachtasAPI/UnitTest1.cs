using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OireachtasAPI;

namespace TestOireachtasAPI
{
    [TestClass]
    public class LoadDatasetTest
    {
        private dynamic expected;

        [TestInitialize]
        public void SetUp()
        {
            using (var r = new StreamReader(Program.MEMBERS_DATASET))
            {
                var json = r.ReadToEnd();
                expected = JsonConvert.DeserializeObject(json);
            }
        }

        [TestMethod]
        public async Task TestLoadFromFile()
        {
            var loaded = await Program.load(Program.MEMBERS_DATASET);
            Assert.AreEqual(loaded["results"].Count, expected["results"].Count);
        }

        [TestMethod]
        public async Task TestLoadFromUrl()
        {
            var loaded = await Program.load("https://api.oireachtas.ie/v1/members?limit=50");
            Assert.AreEqual(loaded["results"].Count, expected["results"].Count);
        }
    }

    [TestClass]
    public class FilterBillsSponsoredByTest
    {
        [TestMethod]
        public async Task TestSponsor()
        {
            var results = await Program.filterBillsSponsoredBy("IvanaBacik");
            Assert.IsTrue(results.Count >= 2);
        }
    }

    [TestClass]
    public class FilterBillsByLastUpdatedTest
    {
        [TestMethod]
        public async Task Testlastupdated()
        {
            var expected = new List<string>
            {
                "77", "101", "58", "141", "55", "133", "132", "131", "111", "135", "134", "91", "129", "103", "138",
                "106", "139"
            }.OrderBy(o => o).ToList();
            var received = new List<string>();

            var since = new DateTime(2018, 12, 1);
            var until = new DateTime(2019, 1, 1);

            foreach (var bill in await Program.filterBillsByLastUpdated(since, until))
                received.Add(bill["billNo"].ToString());

            received = received.OrderBy(o => o).ToList();

            CollectionAssert.AreEqual(expected, received);
        }
    }
}