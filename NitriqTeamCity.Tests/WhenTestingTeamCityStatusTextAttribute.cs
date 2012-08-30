using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingTeamCityStatusTextAttribute {
        [Test]
        public void ShouldReturnTextPassedInConstructor() {
            var attr = new NitriqTeamCity.TeamCity.StatusTextAttribute("test text");
            Assert.AreEqual("test text", attr.Text);
        }
    }
}
