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
        private readonly IDictionary<string, int> _statusInfo = new Dictionary<string, int>();
        private bool _success;

        public void AddStatistics(IEnumerable<Metric> stats) {
            foreach (var stat in stats) {
                var name = _textFormatter.Format(stat.Name);
                _statistics.Add(name, stat);
            }
        }

        public void GenerateStatusInfo() {
            _success = !_statistics.Any(x => x.Value.Error);
            _statusInfo.Add("total", _statistics.Count);
            _statusInfo.Add("warnings", _statistics.Count(s => s.Value.Warning));
            _statusInfo.Add("errors", _statistics.Count(s => s.Value.Error));
        }

        public TeamCityInfo GetTeamCityInfo() {
            return new TeamCityInfo {
                Status = _success ? BuildStatus.Success : BuildStatus.Failure,
                Statistics = _statistics.ToDictionary(s => s.Key, s => s.Value.Value),
                StatusInfo = _statusInfo.Select(s => String.Format(" {0}: {1}", s.Key.ToLowerInvariant(), s.Value))
            };
        }
    }
}