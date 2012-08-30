using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.IO {
    public class XmlDocumentWriter : IXmlDocumentWriter {
        private readonly IStreamWriter _streamWriter;

        public XmlDocumentWriter(IStreamWriter streamWriter) {
            _streamWriter = streamWriter;
        }
        
        public void WriteXDocument(XDocument document, string path) {
            using (var memStream = new MemoryStream()) {
                document.Save(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                _streamWriter.WriteStream(memStream, path);
            }
        }
    }
}
