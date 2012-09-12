using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Core.Helpers
{
    public static class TimeSpanExtenstions
    {
        public static string ToReadableAgeString(this TimeSpan span)
        {
            return string.Format("{0:0}", span.Days / 365.25);
        }

        public static string ToReadableString(this TimeSpan span)
        {
            var days = span.Days;
            if (days > 0)
            {
                if (span.Hours > 23) days++;
                if (days > 1)
                    return string.Format("{0:0} dias atrás", days);
                return "1 dia atrás";
            }

            if (span.Hours > 0)
            {
                if (span.Hours == 1)
                    return "1 hora atrás";
                return string.Format("{0:0} horas atrás", span.Hours);
            }

            if (span.Minutes > 1)
            {
                return string.Format("{0:0} minutos atrás", span.Minutes);
            }

            return "Agora";
        }

        public static string ToExactReadableString(this TimeSpan span)
        {
            var formatted = string.Format("{0}{1}{2}{3}",
                                             span.Days > 0 ? string.Format("{0:0} dias, ", span.Days) : string.Empty,
                                             span.Hours > 0 ? string.Format("{0:0} horas, ", span.Hours) : string.Empty,
                                             span.Minutes > 0 ? string.Format("{0:0} minutos, ", span.Minutes) : string.Empty,
                                             span.Seconds > 0 ? string.Format("{0:0} segundos", span.Seconds) : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            return formatted;
        }
    }
}