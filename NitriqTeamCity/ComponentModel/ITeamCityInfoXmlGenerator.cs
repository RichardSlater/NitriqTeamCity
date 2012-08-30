using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NitriqTeamCity.TeamCity;

namespace NitriqTeamCity {
    public interface ITeamCityInfoXmlGenerator {
        XDocument Generate(TeamCityInfo info);
    }
}
