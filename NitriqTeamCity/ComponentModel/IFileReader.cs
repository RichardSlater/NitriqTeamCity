using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.ComponentModel {
    public interface IFileReader {
        IEnumerable<string> ReadLines(string path);
    }
}