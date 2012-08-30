using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.ComponentModel {
    public interface IReportBreaker {
        IList<string> GetBlocks(string path);
    }
}
