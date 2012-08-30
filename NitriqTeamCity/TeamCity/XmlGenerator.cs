using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NitriqTeamCity.TeamCity {
    public class XmlGenerator : ITeamCityInfoXmlGenerator {
        public XDocument Generate(TeamCityInfo info) {
            var statusText = info.StatusInfo.Select(si =>
                new XElement("text",
                    new XAttribute("action", "append"),
                    si
                )
            );

            var statisticValue = info.Statistics.Select(stat =>
                new XElement("statisticValue",
                    new XAttribute("key", stat.Key),
                    new XAttribute("value", stat.Value)
                )
            );

            var root = new XElement("build",
                new XElement("statusInfo",
                    new XAttribute("status", info.Status.GetAttributeValue<StatusTextAttribute, string>(t => t.Text)),
                    statusText.ToArray()),
                    statisticValue
            );

            if (!String.IsNullOrWhiteSpace(info.BuildNumber)) {
                root.Add(new XAttribute("number", info.BuildNumber));
            }

            return new XDocument(root);
        }
    }
}
