using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.IO {
    public class StreamWriter : IStreamWriter {
        public void WriteStream(Stream stream, string path) {
            using (var fileStream = new FileStream(path, FileMode.Create)) {
                stream.CopyTo(fileStream);
            }
        }
    }
}
