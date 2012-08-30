using System;
using System.Collections.Generic;
using System.Linq;

namespace NitriqTeamCity.TeamCity {
    public static class StatusTextAttributeHelper {
        public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression) {
            var attribute = enumeration.GetType()
                .GetMember(enumeration.ToString())[0]
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>().SingleOrDefault();

            if (attribute == null) {
                return default(Expected);
            }
            return expression(attribute);
        }
    }
}
