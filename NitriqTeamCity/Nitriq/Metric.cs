using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.Nitriq {
    public class Metric {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Warning { get; set; }
        public bool Error{ get; set; }
    }
}
