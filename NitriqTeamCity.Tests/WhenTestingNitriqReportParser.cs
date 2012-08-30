using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingNitriqReportParser {
        private Mock<NitriqTeamCity.ComponentModel.IMetricParser> _metricParserMock;

        [SetUp]
        public void SetUp() {
            _metricParserMock = new Mock<NitriqTeamCity.ComponentModel.IMetricParser>();
        }

        [Test]
        public void ShouldThrowExceptionIfMetricParserNull() {
            Assert.Throws<ArgumentNullException>(() => new ReportParser(null));
        }

        private ReportParser GetReportParser() {
            return new ReportParser(_metricParserMock.Object);
        }

        [Test]
        public void ShouldCallMetricParserParseOnce() {
            var reportParser = GetReportParser();
            var blocks = new string[] { "block1" };
            reportParser.Parse(blocks).ToList();
            _metricParserMock.Verify(m => m.Parse(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void ShouldCallMetricParserParseTwice() {
            var reportParser = GetReportParser();
            var blocks = new string[] { "block1", "block2" };
            reportParser.Parse(blocks).ToList();
            _metricParserMock.Verify(m => m.Parse(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
