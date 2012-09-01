using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    [IntegrationTest]
    public class WhenTestingFileReader {
        [Test]
        public void SholdReadThreeLinesFromFile() {
            File.WriteAllLines("should-read-three-lines-from-file.txt", new string[] { "line-1", "line-2", "line-3" });
            var reader = new NitriqTeamCity.IO.FileReader();
            var lines = reader.ReadLines("should-read-three-lines-from-file.txt").ToArray();
            Assert.AreEqual(3, lines.Count());
            Assert.AreEqual("line-1", lines[0]);
            Assert.AreEqual("line-2", lines[1]);
            Assert.AreEqual("line-3", lines[2]);
            File.Delete("should-read-three-lines-from-file.txt");
        }
    }
}