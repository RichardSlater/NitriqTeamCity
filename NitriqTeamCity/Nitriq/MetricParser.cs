using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NitriqTeamCity.ComponentModel;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity {
    public class MetricParser : IMetricParser {
        private readonly Regex nameBreaker = new Regex("name=\"([^\"]+)\"", RegexOptions.Multiline & RegexOptions.Compiled);
        private readonly Regex resultCounter = new Regex("(<td class=\"numeric\">)+", RegexOptions.Multiline & RegexOptions.Compiled);

        private string ExtractName(string block) {
            var nameMatch = nameBreaker.Match(block);

            if (!nameMatch.Success) {
                throw new InvalidOperationException("Block does not contain a name attribute.");
            }

            var name = nameMatch.Groups[1].Value;
            return name;
        }

        public Metric Parse(string block) {
            var metric = new Metric {
                Name = ExtractName(block),
                Value = resultCounter.Matches(block).Count,
                Warning = block.Contains("<h3>Warning:"),
                Error = block.Contains("<h3>Error:")
            };

            return metric;
        }
    }
}