using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VerifyArgs;
using NitriqTeamCity.ComponentModel;

namespace NitriqTeamCity.Nitriq {
    public class ReportBreaker : IReportBreaker {
        private readonly IFileReader _fileReader;

        public ReportBreaker(IFileReader fileReader) {
            Verify.Args(new { fileReader }).NotNull();

            _fileReader = fileReader;
        }

        public IList<string> GetBlocks(string path) {
            var lines = _fileReader.ReadLines(path);
            var buffer = new StringBuilder();
            var blocks = new List<string>();

            foreach (var line in lines) {
                if (line.ToLowerInvariant().StartsWith("<h2>")) {
                    if (buffer.ToString().ToLowerInvariant().StartsWith("<h2>")) {
                        blocks.Add(buffer.ToString());
                    }
                    buffer.Clear();
                }

                buffer.AppendLine(line);
            }

            blocks.Add(buffer.ToString());

            return blocks;
        }
    }
}
