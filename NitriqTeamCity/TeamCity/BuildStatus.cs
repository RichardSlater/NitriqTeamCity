using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.TeamCity {
    public enum BuildStatus {
        [StatusText("FAILURE")]
        Failure,

        [StatusText("SUCCESS")]
        Success
    }
}
