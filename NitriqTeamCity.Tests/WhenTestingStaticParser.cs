using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingStaticParser {
        [Test]
        public void ShouldParseNitriqHtmlProducingTeamCityInfo() {
            var basePath = @"..\..\Artifacts";
            var reportPath = Path.Combine(basePath, "Nitriq.html");
            var outputPath = Path.Combine(basePath, "teamcity-info.xml");
            var refxmlPath = Path.Combine(basePath, "reference-teamcity-info.xml");
            var schemaPath = Path.Combine(basePath, "teamcity-info.xsd");

            if (File.Exists(outputPath)) {
                File.Delete(outputPath);
            }

            Assert.IsFalse(File.Exists(outputPath));

            StaticParser.Execute(reportPath, outputPath);

            Assert.IsTrue(File.Exists(outputPath));

            var doc = XDocument.Load(outputPath);
            var schemas = new XmlSchemaSet();
            schemas.Add(XmlSchema.Read(new FileStream(schemaPath, FileMode.Open), null));
            doc.Validate(schemas, (sender, e) => {
                Assert.Fail(String.Format("Validation failed with message '{0}'", e.Message));
            });

            var refXml = File.ReadAllText(refxmlPath);
            var testXml = File.ReadAllText(outputPath);

            Assert.AreEqual(refXml, testXml);
        }
    }
}
