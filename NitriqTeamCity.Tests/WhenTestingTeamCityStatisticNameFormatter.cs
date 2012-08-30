﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NitriqTeamCity.TeamCity;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingTeamCityStatisticNameFormatter {
        [Test]
        public void ShouldConvertTextToLowercase() {
            var formatter = new StatisticNameFormatter();
            var actual = formatter.Format("AbC");
            Assert.AreEqual("abc", actual);
        }

        [Test]
        public void ShouldReplaceWhitespaceWithDash() {
            var formatter = new StatisticNameFormatter();
            var actual = formatter.Format("the quick brown fox");
            Assert.AreEqual("the-quick-brown-fox", actual);
        }

        [Test]
        public void ShouldReplaceMultipleDashesWithSingle() {
            var formatter = new StatisticNameFormatter();
            var actual = formatter.Format("the--quick");
            Assert.AreEqual("the-quick", actual);
        }

        [Test]
        public void ShouldTrimLeadingAndTrailingSpaces() {
            var formatter = new StatisticNameFormatter();
            var actual = formatter.Format(" the-quick ");
            Assert.AreEqual("the-quick", actual);
        }

        [Test]
        public void ShouldTrimLeadingAndNonAlphaCharacters() {
            var formatter = new StatisticNameFormatter();
            var actual = formatter.Format("(the-quick)");
            Assert.AreEqual("the-quick", actual);
        }
    }
}
