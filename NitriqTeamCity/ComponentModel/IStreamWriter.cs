using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NitriqTeamCity.ComponentModel {
    public interface IStreamWriter {
        void WriteStream(Stream stream, string path);
    }
}