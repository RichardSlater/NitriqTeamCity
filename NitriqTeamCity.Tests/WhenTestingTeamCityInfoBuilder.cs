using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using NitriqTeamCity.TeamCity;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingTeamCityInfoBuilder {
        private Mock<NitriqTeamCity.ComponentModel.ITextFormatter> _formatterMock;

        [SetUp]
        public void SetUp() {
            _formatterMock = new Mock<NitriqTeamCity.ComponentModel.ITextFormatter>();
            _formatterMock.Setup(m => m.Format("Test Metric")).Returns("test-metric");
            _formatterMock.Setup(m => m.Format("Second Metric")).Returns("second-metric");
            _formatterMock.Setup(m => m.Format("Third Metric")).Returns("third-metric");
        }

        [Test]
        public void ShouldCallFormatterOnceWhenPassedOneStatistic() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric" }
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);

            _formatterMock.Verify(m => m.Format(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void ShouldCallFormatterOnceWhenPassedTwoStatistics() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric" },
                new Metric { Name = "Second Metric" }
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);

            _formatterMock.Verify(m => m.Format(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void ShouldReturnFormattedStatistics() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric", Value = 7 },
                new Metric { Name = "Second Metric", Value = 1 }
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);

            var actual = builder.GetTeamCityInfo();
            Assert.AreEqual(2, actual.Statistics.Count);
            Assert.AreEqual("test-metric", actual.Statistics.First().Key);
            Assert.AreEqual("second-metric", actual.Statistics.Last().Key);
            Assert.AreEqual(7, actual.Statistics.First().Value);
            Assert.AreEqual(1, actual.Statistics.Last().Value);
        }

        [Test]
        public void ShouldReturnAggregatesInHeader() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric", Error= true, Warning = true },
                new Metric { Name = "Second Metric", Warning = true },
                new Metric { Name = "Third Metric" }
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);
            builder.GenerateStatusInfo();

            var actual = builder.GetTeamCityInfo();
            Assert.AreEqual(3, actual.StatusInfo.Count());
            Assert.AreEqual(" total: 3", actual.StatusInfo.First());
            Assert.AreEqual(" warnings: 2", actual.StatusInfo.Skip(1).First());
            Assert.AreEqual(" errors: 1", actual.StatusInfo.Last());
        }

        [Test]
        public void ShouldBeAFailingBuild() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric", Error= true, Warning = true },
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);
            builder.GenerateStatusInfo();

            var actual = builder.GetTeamCityInfo();
            Assert.AreEqual(BuildStatus.Failure, actual.Status);
        }

        [Test]
        public void ShouldBeASuccessfulBuild() {
            var statistics = new Metric[] {
                new Metric { Name = "Test Metric", Error = false, Warning = false },
            };

            var builder = new TeamCityInfoBuilder(_formatterMock.Object);

            builder.AddStatistics(statistics);
            builder.GenerateStatusInfo();

            var actual = builder.GetTeamCityInfo();
            Assert.AreEqual(BuildStatus.Success, actual.Status);
        }
    }
}
