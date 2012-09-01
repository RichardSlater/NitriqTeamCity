using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace NitriqTeamCity.Tests {
    [TestFixture]
    [IntegrationTest]
    public class WhenTestingStreamWriter {
        [Test]
        public void ShouldWriteTestDataToFile() {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("test data"));
            var streamWriter = new NitriqTeamCity.IO.StreamWriter();
            streamWriter.WriteStream(stream, "should-write-stream-to-file.txt");
            var content = File.ReadAllText("should-write-stream-to-file.txt");
            Assert.AreEqual("test data", content);
            File.Delete("should-read-three-lines-from-file.txt");
        }
    }
}

