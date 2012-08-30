using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.Console {
    class Program {
        static void Main(string[] args) {
            Header();

            if (args.Select(a => a.Trim()).Any(a => a == "-h" || a == "--help")) {
                Usage();
            }

            var input = ExtractArgument("-input", args);
            var output = ExtractArgument("-output", args);

            W("Input File: " + input.Trim('"'));
            W("Output File: " + output.Trim('"'));
            NewLine();

            StaticParser.Execute(input, output);

            Exit("Done.");
        }

        private static void Header() {
            W("Nitriq TeamCity Integration; ©2012 Richard Slater.");
            W("http://github.com/RichardSlater/NitriqTeamCity");
            NewLine();
        }

        private static void Usage() {
            W("Usage:");
            W("  NitriqTeamCity.Console.exe -input:<input.html> -output:<teamcity-info.xml>");
            NewLine();
        }

        private static string ExtractArgument(string argument, params string[] args) {
            args = args.Select(a => a.Trim()).ToArray();

            if (args.Count(a => a.StartsWith(argument)) != 1) {
                Usage();
                Exit(argument + " not specified correctly.", 1);
            }

            var value = args.Single(a => a.StartsWith(argument));

            if (!value.Contains(":")) {
                Usage();
                Exit(String.Format("  ERROR: {0} is malformed.", argument));
            }

            return value.Remove(0, value.IndexOf(":") + 1);
        }

        private static void Exit(string value, int exitCode = 0) {
            W(value);
            Environment.Exit(exitCode);
        }

        private static void W(string value){
            System.Console.WriteLine(value);
        }

        private static void NewLine() {
            System.Console.WriteLine();
        }
    }
}
