using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.ComponentModel {
    public interface IParser {
        void Parse(string reportPath, string outputPath);
    }
}
