using System;
using System.Collections.Generic;
using System.Linq;
using VerifyArgs;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity {
    public class ReportParser : IReportParser {
        private readonly IMetricParser _metricParser;

        public ReportParser(IMetricParser metricParser) {
            Verify.Args(new { metricParser }).NotNull();

            _metricParser = metricParser;
        }

        public IEnumerable<NitriqTeamCity.Nitriq.Metric> Parse(IEnumerable<string> blocks) {
            foreach (var block in blocks) {
                yield return _metricParser.Parse(block);
            }
        }
    }
}
