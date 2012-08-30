using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.IO {
    public class FileReader : IFileReader {
        public IEnumerable<string> ReadLines(string path) {
            return File.ReadLines(path);
        }
    }
}


