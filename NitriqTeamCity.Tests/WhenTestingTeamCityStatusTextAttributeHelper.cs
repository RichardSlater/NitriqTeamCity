using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NitriqTeamCity.TeamCity;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    public class WhenTestingTeamCityStatusTextAttributeHelper {
        public enum TestEnum {
            [StatusText("Test")]
            TestValue,

            NullValue
        }

        [Test]
        public void ShouldGetTextProperty() {
            var test = TestEnum.TestValue;
            var actual = test.GetAttributeValue<StatusTextAttribute, string>(v => v.Text);
            Assert.AreEqual("Test", actual);
        }

        [Test]
        public void ShouldFailToGetAttributeGracefully() {
            var test = TestEnum.NullValue;
            var actual = test.GetAttributeValue<StatusTextAttribute, string>(v => v.Text);
            Assert.AreEqual(default(string), actual);
        }
    }
}
