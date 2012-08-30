using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using NitriqTeamCity.Nitriq;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingNitriqReportBreaker {
        private Mock<IFileReader> _fileReaderMock;

        [SetUp]
        public void SetUp() {
            _fileReaderMock = new Mock<IFileReader>();
        }

        private ReportBreaker GetReportBreaker() {
            return new ReportBreaker(_fileReaderMock.Object);
        }

        [Test]
        public void ShouldThrowExceptionWhenFileReaderNull() {
            Assert.Throws<ArgumentNullException>(() => new ReportBreaker(null));
        }

        [Test]
        public void ShouldNotThrowExceptionWhenProvidedFileReader() {
            Assert.DoesNotThrow(() => GetReportBreaker());
        }

        private void SetupReadLines() {
            var lines = new List<string> {
                                "<h2>test</h2>",
                                "<p>test1</p>",
                                "<h2>test</h2>",
                                "<p>test2</p>",
                                "<h2>test</h2>",
                                "<p>test3</p>"
                            };
            _fileReaderMock.Setup(m => m.ReadLines("sample-path")).Returns(lines);
        }

        [Test]
        public void ShouldCallIntoFileReaderReadLinesWhenGetBlocks() {
            SetupReadLines();
            GetReportBreaker().GetBlocks("sample-path");
            _fileReaderMock.Verify(m => m.ReadLines("sample-path"), Times.Once());
        }

        [Test]
        public void ShouldReturnThreeBlocksWhenPassedThreeH2s() {
            SetupReadLines();
            var blocks = GetReportBreaker().GetBlocks("sample-path");
            Assert.AreEqual(3, blocks.Count);
            Assert.IsTrue(blocks.All(b => b.StartsWith("<h2>")));
        }

        [Test]
        public void ShouldReturnThreeBlocksWhenPassedThreeH2sEvenWhenPrefixed() {
            var lines = new List<string> { "<h1>Gibberish Data</h1>", "<h2>test</h2>", "<p>test1</p>", "<h2>test</h2>", "<p>test2</p>", "<h2>test</h2>", "<p>test3</p>" };
            _fileReaderMock.Setup(m => m.ReadLines("sample-path")).Returns(lines);
            var blocks = GetReportBreaker().GetBlocks("sample-path");
            Assert.AreEqual(3, blocks.Count);
            Assert.IsTrue(blocks.All(b => b.StartsWith("<h2>")));
        }
    }
}
