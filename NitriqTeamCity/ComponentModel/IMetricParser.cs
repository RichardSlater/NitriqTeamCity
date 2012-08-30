using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity.ComponentModel {
    public interface IMetricParser {
        Metric Parse(string block);
    }
}
