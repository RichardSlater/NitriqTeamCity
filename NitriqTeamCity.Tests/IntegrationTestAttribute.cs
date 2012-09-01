using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NitriqTeamCity.Tests {
    public class IntegrationTestAttribute : CategoryAttribute {
        public IntegrationTestAttribute()
            : base("IntegrationTest") { }
    }
}

