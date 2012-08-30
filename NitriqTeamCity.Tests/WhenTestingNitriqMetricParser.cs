using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingNitriqMetricParser {
        [Test]
        public void ShouldThrowWhenNameNotPresent() {
            var parser = new MetricParser();
            var block = @"<h2><a alpha=""Henderson Sellers Lack of Cohesion"" /></h2>";
            Assert.Throws<InvalidOperationException>(() => parser.Parse(block));
        }

        [Test]
        public void ShouldExtractMetricFromZeroResultBlock() {
            var parser = new MetricParser();
            var block = @"<h2><a name=""Henderson Sellers Lack of Cohesion"" />The Query ""Henderson Sellers Lack of Cohesion"" returned the following results: </h2>No Results to display<br /><br />";
            var metric = parser.Parse(block);

            Assert.AreEqual("Henderson Sellers Lack of Cohesion", metric.Name);
            Assert.AreEqual(0, metric.Value);
        }

        [Test]
        public void ShouldExtractMetricFromMultipleResultBlock() {
            var parser = new MetricParser();
            var block = @"<h2><a name=""Methods that take or return System.Object"" />The Query ""Methods that take or return System.Object"" returned the following results: </h2><table border=""1""><tr><thead><td><b>MethodId</b></td><td><b>Name</b></td><td><b>FullName</b></td><td><b>TakeObjectParam</b></td><td><b>ReturnsObject</b></td></thead></tr><tr><td class=""numeric"">45</td><td><Load>b__0</td><td>Makemedia.ServerManager.Mappings.AutoMapperModule.<Load>b__0</td><td>False</td><td>True</td></tr><tr><td class=""numeric"">63</td><td>Equals</td><td>.<>f__AnonymousType0`4.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">64</td><td>Equals</td><td>.<>f__AnonymousType2`4.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">65</td><td>Equals</td><td>.<>f__AnonymousType5`1.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">66</td><td>Equals</td><td>.<>f__AnonymousType1`2.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">67</td><td>Equals</td><td>.<>f__AnonymousType3`3.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">69</td><td>Equals</td><td>.<>f__AnonymousType6`1.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">70</td><td>Equals</td><td>.<>f__AnonymousType4`1.Equals</td><td>True</td><td>False</td></tr><tr><td class=""numeric"">205</td><td>System.Collections.IEnumerator.get_Current</td><td>Makemedia.ServerManager.Services.<GetSites>d__0.System.Collections.IEnumerator.get_Current</td><td>False</td><td>True</td></tr></table><br /><br />";
            var metric = parser.Parse(block);

            Assert.AreEqual("Methods that take or return System.Object", metric.Name);
            Assert.AreEqual(9, metric.Value);
        }

        [Test]
        public void ShouldExtractWarningFromWarningResultBlock() {
            var parser = new MetricParser();
            var block = @"<h2><a name=""Avoid namespaces with few types"" />The Query ""Avoid namespaces with few types"" has the following problems: </h2><h3>Warning: More than 0 results were returned</h3><table border=""1""><tr><thead><td><b>NamespaceId</b></td><td><b>FullName</b></td><td><b>Count</b></td></thead></tr>";
            var metric = parser.Parse(block);

            Assert.IsTrue(metric.Warning);
        }

        [Test]
        public void ShouldExtractErrorFromErrorResultBlock() {
            var parser = new MetricParser();
            var block = @"<h2><a name=""Avoid namespaces with few types"" />The Query ""Avoid namespaces with few types"" has the following problems: </h2><h3>Error: More than 0 results were returned</h3><table border=""1""><tr><thead><td><b>NamespaceId</b></td><td><b>FullName</b></td><td><b>Count</b></td></thead></tr>";
            var metric = parser.Parse(block);

            Assert.IsTrue(metric.Error);
        }
    }
}
