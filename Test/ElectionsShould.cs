using System.Collections.Generic;
using Domain;
using NFluent;
using NUnit.Framework;

namespace Test
{
    internal class ElectionsShould
    {
        [Test]
        public void RunWithoutDistricts()
        {
            var list = new Dictionary<string, List<string>>
            {
                ["District 1"] = new List<string> {"Bob", "Anna", "Jess", "July"},
                ["District 2"] = new List<string> {"Jerry", "Simon"},
                ["District 3"] = new List<string> {"Johnny", "Matt", "Carole"}
            };

            var elections = new Elections(list, false);
            elections.AddCandidate("Michel");
            elections.AddCandidate("Jerry");
            elections.AddCandidate("Johnny");

            elections.VoteFor("Bob", "Jerry", "District 1");
            elections.VoteFor("Jerry", "Jerry", "District 2");
            elections.VoteFor("Anna", "Johnny", "District 1");
            elections.VoteFor("Johnny", "Johnny", "District 3");
            elections.VoteFor("Matt", "Donald", "District 3");
            elections.VoteFor("Jess", "Joe", "District 1");
            elections.VoteFor("Simon", "", "District 2");
            elections.VoteFor("Carole", "", "District 3");

            var results = elections.Results();

            var expectedResults = new Dictionary<string, string>
            {
                ["Michel"] = "0,00%",
                ["Jerry"] = "50,00%",
                ["Johnny"] = "50,00%",
                ["Blank"] = "25,00%",
                ["Null"] = "25,00%",
                ["Abstention"] = "11,11%"
            };

            Check.That(results).ContainsExactly(expectedResults);
        }

        [Test]
        public void RunWithDistricts()
        {
            var list = new Dictionary<string, List<string>>
            {
                ["District 1"] = new List<string> {"Bob", "Anna", "Jess", "July"},
                ["District 2"] = new List<string> {"Jerry", "Simon"},
                ["District 3"] = new List<string> {"Johnny", "Matt", "Carole"}
            };
            var elections = new Elections(list, true);
            elections.AddCandidate("Michel");
            elections.AddCandidate("Jerry");
            elections.AddCandidate("Johnny");

            elections.VoteFor("Bob", "Jerry", "District 1");
            elections.VoteFor("Jerry", "Jerry", "District 2");
            elections.VoteFor("Anna", "Johnny", "District 1");
            elections.VoteFor("Johnny", "Johnny", "District 3");
            elections.VoteFor("Matt", "Donald", "District 3");
            elections.VoteFor("Jess", "Joe", "District 1");
            elections.VoteFor("July", "Jerry", "District 1");
            elections.VoteFor("Simon", "", "District 2");
            elections.VoteFor("Carole", "", "District 3");

            var results = elections.Results();

            var expectedResults = new Dictionary<string, string>
            {
                ["Michel"] = "0,00%",
                ["Jerry"] = "66,67%",
                ["Johnny"] = "33,33%",
                ["Blank"] = "22,22%",
                ["Null"] = "22,22%",
                ["Abstention"] = "0,00%"
            };


            Check.That(results).ContainsExactly(expectedResults);
        }
    }
}