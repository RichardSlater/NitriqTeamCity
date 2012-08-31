using System;
using System.Collections.Generic;
using System.Linq;
using NitriqTeamCity.ComponentModel;
using NitriqTeamCity.Nitriq;

namespace NitriqTeamCity.TeamCity {
    public class TeamCityInfoBuilder : ITeamCityInfoBuilder {
        private readonly ITextFormatter _textFormatter;

        public TeamCityInfoBuilder(ITextFormatter textFormatter) {
            _textFormatter = textFormatter;
        }

        private readonly IDictionary<string, Metric> _statistics = new Dictionary<string, Metric>();
        private string _statusInfo;
        private bool _success;

        public void AddStatistics(IEnumerable<Metric> stats) {
            foreach (var stat in stats) {
                var name = _textFormatter.Format(stat.Name);
                _statistics.Add(name, stat);
            }
        }

        public void GenerateStatusInfo() {
            _success = !_statistics.Any(x => x.Value.Error);

            int total = _statistics.Count;
            int warnings = _statistics.Count(s => s.Value.Warning);
            int errors = _statistics.Count(s => s.Value.Error);
            _statusInfo = String.Format("Code metrics: {0} total, {1} warnings, {2} errors.", total, warnings, errors);
        }

        public TeamCityInfo GetTeamCityInfo() {
            return new TeamCityInfo {
                Status = _success ? BuildStatus.Success : BuildStatus.Failure,
                Statistics = _statistics.ToDictionary(s => s.Key, s => s.Value.Value),
                StatusInfo = new string[] { _statusInfo }
            };
        }
    }
}