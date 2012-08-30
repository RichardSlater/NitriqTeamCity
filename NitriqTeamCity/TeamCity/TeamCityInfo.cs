using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.TeamCity {
    public class TeamCityInfo {
        public string BuildNumber { get; set; }
        public BuildStatus Status { get; set; }
        public IDictionary<string, int> Statistics { get; set; }
        public IEnumerable<string> StatusInfo { get; set; }
    }
}