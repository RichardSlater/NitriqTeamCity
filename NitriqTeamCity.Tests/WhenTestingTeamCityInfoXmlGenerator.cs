using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingTeamCityInfoXmlGenerator {
        private static XDocument GetXML(NitriqTeamCity.TeamCity.TeamCityInfo teamCityInfo) {
            if (teamCityInfo.Statistics == null) {
                teamCityInfo.Statistics = new Dictionary<string, int>();
            }
            if (teamCityInfo.StatusInfo == null) {
                teamCityInfo.StatusInfo = new List<string>();
            }
            var gen = new NitriqTeamCity.TeamCity.XmlGenerator();
            return gen.Generate(teamCityInfo);
        }

        private static bool XPathExists(XDocument output, string path) {
            return Convert.ToBoolean(output.XPathEvaluate(String.Format("boolean({0})", path)));
        }

        private static int XPathCount(XDocument output, string path) {
            return Convert.ToInt32(output.XPathEvaluate(String.Format("count({0})", path)));
        }

        [Test]
        public void RootNodeShouldBeBuild() {
            var output = GetXML(new NitriqTeamCity.TeamCity.TeamCityInfo { BuildNumber = "xxx" });
            Assert.AreEqual("build", output.Root.Name.LocalName);
            Assert.IsTrue(XPathExists(output, "/build[@number='xxx']"));
        }

        [Test]
        public void StatusInfoNodeShouldExistBelowRootNode() {
            var output = GetXML(new NitriqTeamCity.TeamCity.TeamCityInfo { Status = NitriqTeamCity.TeamCity.BuildStatus.Success });
            Assert.IsTrue(XPathExists(output, "/build/statusInfo"));
            Assert.IsTrue(XPathExists(output, "/build/statusInfo[@status='SUCCESS']"));
        }

        [Test]
        public void ShouldContainThreeTextElements() {
            var output = GetXML(new NitriqTeamCity.TeamCity.TeamCityInfo { StatusInfo = new List<string> { "total", "error", "warning" } });
            Assert.AreEqual(3, XPathCount(output, "/build/statusInfo/text"));
            Assert.AreEqual(3, XPathCount(output, "/build/statusInfo/text[@action='append']"));
            Assert.IsTrue(XPathExists(output, "/build/statusInfo[text='total']"));
            Assert.IsTrue(XPathExists(output, "/build/statusInfo[text='error']"));
            Assert.IsTrue(XPathExists(output, "/build/statusInfo[text='warning']"));
        }

        [Test]
        public void ShouldContainThreeStatisticValueElements() {
            var output = GetXML(new NitriqTeamCity.TeamCity.TeamCityInfo { Statistics = new Dictionary<string, int> { { "stat-1", 1 }, { "stat-2", 2 }, { "stat-3", 3 } } });
            Assert.AreEqual(3, XPathCount(output, "/build/statisticValue"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[1][@value=1]"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[2][@value=2]"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[3][@value=3]"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[1][@key='stat-1']"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[2][@key='stat-2']"));
            Assert.IsTrue(XPathExists(output, "/build/statisticValue[3][@key='stat-3']"));
        }
    }
}
