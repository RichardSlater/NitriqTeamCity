using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using NitriqTeamCity.ComponentModel;
using NitriqTeamCity.IO;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingXDocumentWriter {
        [Test]
        public void ShouldCallStreamWriter() {
            var streamWriterMock = new Mock<IStreamWriter>();
            var docWriter = new XmlDocumentWriter(streamWriterMock.Object);
            docWriter.WriteXDocument(new XDocument(new XElement("test")), "test-file");
            streamWriterMock.Verify(m => m.WriteStream(It.IsAny<Stream>(), "test-file"), Times.Once());
        }
    }
}
