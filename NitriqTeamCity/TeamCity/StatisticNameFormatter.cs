using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.TeamCity {
    public class StatisticNameFormatter : ITextFormatter {
        private readonly Regex invalidCharacters = new Regex("[^a-z]", RegexOptions.Compiled);
        private readonly Regex multipleDashes = new Regex("-[-]+", RegexOptions.Compiled);

        public string Format(string input) {
            var output = input
                .ToLowerInvariant()
                .Trim();

            output = invalidCharacters.Replace(output, "-");

            output = multipleDashes.Replace(output, "-");

            output = output.Trim('-');

            return output;
        }
    }
}
