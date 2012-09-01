using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.Console {
    class Program {
        private static Action<int> _exitAction;

        public static Action<int> ExitAction {
            get {
                if (_exitAction == null) {
                    _exitAction = (exitCode) => Environment.Exit(exitCode);
                }
                return _exitAction;
            }
            set { _exitAction = value; }
        }

        static void Main(string[] args) {
            Header();

            if (args.Select(a => a.Trim()).Any(a => a == "-h" || a == "--help")) {
                Usage();
            }

            var format = ExtractArgument("--format", "-f", args);
            var input = ExtractArgument("--input", "-i", args);
            var output = ExtractArgument("--output", "-o", args);

            W("Format: {0}", format);
            W("Input File: {0}", input.Trim('"'));
            W("Output File: {0}", output.Trim('"'));

            NewLine();

            StaticParser.Execute(input, output);

            Exit("Done.", 0);
        }

        private static void Header() {
            W("Nitriq TeamCity Integration; ©2012 Richard Slater.");
            W("http://github.com/RichardSlater/NitriqTeamCity");
            NewLine();
        }

        private static void Usage() {
            W("Usage");
            W("=====");
            NewLine();
            W("Long Format:");
            W("  NitriqTeamCity.Console.exe --format:TeamCityInfoXml --input:<input.html> --output:<teamcity-info.xml>");
            NewLine();
            W("Short Format:");
            W("  NitriqTeamCity.Console.exe -f:TeamCityInfoXml -i:<input.html> -o:<teamcity-info.xml>");
            NewLine();
        }

        private static string ExtractArgument(string argument, string shortArgument, params string[] args) {
            args = args.Select(a => a.Trim()).ToArray();

            Func<string, bool> argSelector = a => a.StartsWith(argument) || a.StartsWith(shortArgument);
            
            var argCount = args.Count(argSelector);

            if (argCount < 1) {
                Usage();
                Exit("{0} not specified", 1, argument);
            }

            if (argCount > 1) {
                Usage();
                Exit("more than one {0} specified, expected only 1.", 1, argument);
            }

            var value = args.Single(argSelector);

            if (!value.Contains(":")) {
                Usage();
                Exit("{0} is malformed.", 1, argument);
            }

            return value.Remove(0, value.IndexOf(":") + 1);
        }

        private static void Exit(string format, int exitCode, params object[] args) {
            W(format, args);
            ExitAction(exitCode);
        }

        private static void W(string format, params object[] args) {
            System.Console.WriteLine(format, args);
        }

        private static void NewLine() {
            System.Console.WriteLine();
        }
    }
}
