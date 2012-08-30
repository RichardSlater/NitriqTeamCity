using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.IO;
using NitriqTeamCity.TeamCity;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity {
    public static class StaticParser {
        /// <summary>
        /// Static wraper round the parser class, all depenencies are instantiated by this method.
        /// </summary>
        /// <param name="reportPath">Path and filename of the nitriq report.</param>
        /// <param name="outputPath">Path and filename of teamcity-info.xml.</param>
        public static void Execute(string reportPath, string outputPath) {
            var fileReader = new FileReader();
            var reportBreaker = new ReportBreaker(fileReader);
            var metricParser = new MetricParser();
            var reportParser = new ReportParser(metricParser);
            var textFormatter = new StatisticNameFormatter();
            var infoBuilder = new TeamCityInfoBuilder(textFormatter);
            var xmlGenerator = new XmlGenerator();
            var streamWriter = new StreamWriter();
            var documentWriter = new XmlDocumentWriter(streamWriter);

            var parser = new Parser(reportBreaker, reportParser, infoBuilder, xmlGenerator, documentWriter);
            parser.Parse(reportPath, outputPath);
        }
    }
}