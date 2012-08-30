using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace NitriqTeamCity.MSBuild {
    public class NitriqTeamCity : Task {
        [Required]
        public string ReportPath { get; set; }

        [Required]
        public string OutputPath { get; set; }

        public override bool Execute() {
            try {
                StaticParser.Execute(ReportPath, OutputPath);
                Log.LogMessage("Successfully created {0}", OutputPath);
                return true;
            } catch (Exception ex) {
                Log.LogErrorFromException(ex, true);
                return false;
            }
        }
    }
}
