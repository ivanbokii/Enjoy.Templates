using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Enjoy.Web.Util
{
    internal static class LinguisticExtensions
    {
        public static string FirstCharToUpper(this string value)
        {
            return string.Format("{0}{1}", value[0].ToString(CultureInfo.InvariantCulture).ToUpper(), value.Substring(1));
        }

        public static string[] SplitIdentifier(this string identifier)
        {
            identifier = identifier.Trim('_');
            var phrases = identifier.Split('_');
            var words = phrases.Aggregate
                (Enumerable.Empty<string>(), (array, phrase) => array.Concat(phrase.SplitCamel()));
            return words.ToArray();
        }

        private static IEnumerable<string> SplitCamel(this string identifier)
        {
            return
                new Regex(@"([A-Z]+)|([A-Z][a-z]+)|([a-z]+)", RegexOptions.RightToLeft).Matches(identifier).Cast<Match>
                    ().Select(match => match.Value).Reverse().ToArray();
        }
    }
}