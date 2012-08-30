using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity.ComponentModel {
    public interface IReportParser {
        IEnumerable<Metric> Parse(IEnumerable<string> blocks);
    }
}
