using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.Nitriq;
using NitriqTeamCity.TeamCity;

namespace NitriqTeamCity.ComponentModel {
    public interface ITeamCityInfoBuilder {
        void AddStatistics(IEnumerable<Metric> stats);
        void GenerateStatusInfo();
        TeamCityInfo GetTeamCityInfo();
    }
}
