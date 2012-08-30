using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity {
    public class Parser : IParser {
        private readonly IReportBreaker _reportBreaker;
        private readonly IReportParser _reportParser;
        private readonly ITeamCityInfoBuilder _infoBuilder;
        private readonly ITeamCityInfoXmlGenerator _xmlGenerator;
        private readonly IXmlDocumentWriter _documentWriter;

        public Parser(IReportBreaker reportBreaker, IReportParser reportParser, ITeamCityInfoBuilder infoBuilder, ITeamCityInfoXmlGenerator xmlGenerator, IXmlDocumentWriter documentWriter) {
            _reportBreaker = reportBreaker;
            _reportParser = reportParser;
            _infoBuilder = infoBuilder;
            _xmlGenerator = xmlGenerator;
            _documentWriter = documentWriter;
        }

        public void Parse(string reportPath, string outputPath) {
            var blocks = _reportBreaker.GetBlocks(reportPath);
            var metrics = _reportParser.Parse(blocks);
            _infoBuilder.AddStatistics(metrics);
            _infoBuilder.GenerateStatusInfo();
            var info = _infoBuilder.GetTeamCityInfo();
            var xmlDocument = _xmlGenerator.Generate(info);
            _documentWriter.WriteXDocument(xmlDocument, outputPath);
        }
    }
}