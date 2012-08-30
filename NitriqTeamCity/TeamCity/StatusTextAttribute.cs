using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.TeamCity {
    public class StatusTextAttribute : Attribute {
        private readonly string _text;

        public StatusTextAttribute(string text) {
            _text = text;
        }

        public string Text {
            get { return _text; }
        }
    }
}
