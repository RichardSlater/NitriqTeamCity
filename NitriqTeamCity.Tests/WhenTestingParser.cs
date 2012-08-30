using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Xml.Linq;
using NUnit.Framework;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingParser {
        private Mock<IReportBreaker> _reportBreakerMock;
        private Mock<IReportParser> _nitriqReportParserMock;
        private Mock<ITeamCityInfoBuilder> _teamCityInfoBuilderMock;
        private Mock<ITeamCityInfoXmlGenerator> _teamCityInfoXmlGeneratorMock;
        private Mock<IXmlDocumentWriter> _documentWriterMock;
        private IParser _parser;

        [Test]
        public void SetUp() {
            _reportBreakerMock = new Mock<IReportBreaker>();
            _nitriqReportParserMock = new Mock<IReportParser>();
            _teamCityInfoBuilderMock = new Mock<ITeamCityInfoBuilder>();
            _teamCityInfoXmlGeneratorMock = new Mock<ITeamCityInfoXmlGenerator>();
            _documentWriterMock = new Mock<IXmlDocumentWriter>();
            _parser = new Parser(_reportBreakerMock.Object,
                          _nitriqReportParserMock.Object,
                          _teamCityInfoBuilderMock.Object,
                          _teamCityInfoXmlGeneratorMock.Object,
                          _documentWriterMock.Object);
            _parser.Parse("report", "output");
        }

        [Test]
        public void ShouldCallFileReaderReadLines() {
            _reportBreakerMock.Verify(m => m.GetBlocks("report"), Times.Once());
        }

        [Test]
        public void ShouldCallReportParserParse() {
            _nitriqReportParserMock.Verify(m => m.Parse(It.IsAny<IEnumerable<string>>()), Times.Once());
        }

        [Test]
        public void ShouldCallInfoBuilderMethods() {
            _teamCityInfoBuilderMock.Verify(m => m.AddStatistics(It.IsAny<IEnumerable<NitriqTeamCity.Nitriq.Metric>>()), Times.Once());
            _teamCityInfoBuilderMock.Verify(m => m.GenerateStatusInfo(), Times.Once());
            _teamCityInfoBuilderMock.Verify(m => m.GetTeamCityInfo(), Times.Once());
        }

        [Test]
        public void ShouldCallXmlGeneratorGenerate() {
            _teamCityInfoXmlGeneratorMock.Verify(m => m.Generate(It.IsAny<NitriqTeamCity.TeamCity.TeamCityInfo>()));
        }

        [Test]
        public void ShouldCallDocumentWriterWriteXDocument() {
            _documentWriterMock.Verify(m => m.WriteXDocument(It.IsAny<XDocument>(), "output"), Times.Once());
        }
    }
}
