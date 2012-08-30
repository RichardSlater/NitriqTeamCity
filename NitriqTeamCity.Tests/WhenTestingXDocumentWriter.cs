using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingXDocumentWriter {
        [Test]
        public void ShouldCallStreamWriter() {
            var streamWriterMock = new Mock<NitriqTeamCity.ComponentModel.IStreamWriter>();
            var docWriter = new NitriqTeamCity.IO.XmlDocumentWriter(streamWriterMock.Object);
            docWriter.WriteXDocument(new XDocument(new XElement("test")), "test-file");
            streamWriterMock.Verify(m => m.WriteStream(It.IsAny<Stream>(), "test-file"), Times.Once());
        }
    }
}
