using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NitriqTeamCity.ComponentModel {
    public interface IXmlDocumentWriter {
        void WriteXDocument(XDocument document, string path);
    }
}
