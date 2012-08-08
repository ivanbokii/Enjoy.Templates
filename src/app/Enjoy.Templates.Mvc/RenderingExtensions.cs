using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Enjoy.Mvc
{
    public static class RenderingExtensions
    {
        private const string CapturesKey = "_EnjoyMvcCaptures";

        public static RenderingContext CaptureIn(this HtmlHelper html, string context)
        {
            var page = html.ViewDataContainer as WebPageBase;
            if (page == null)
            {
                throw new InvalidOperationException("We currently cannot support views not deriving from WebPageBase.");
            }
            return new RenderingContext(page.OutputStack, html.Captures().GetOrAdd(context, s => new Stack<string>()));
        }

        public static HtmlString RenderCapture(this HtmlHelper html, string context)
        {
            var output = html.Captures().GetOrAdd(context, s => new Stack<string>());
            var sb = new StringBuilder();
            foreach (var str in output)
            {
                sb.Append(str);
            }
            return new HtmlString(sb.ToString());
        }

        private static ConcurrentDictionary<string, Stack<string>> Captures(this HtmlHelper html)
        {
            object value;
            if (!html.ViewData.TryGetValue(CapturesKey, out value))
            {
                value = new ConcurrentDictionary<string, Stack<string>>();
                html.ViewData[CapturesKey] = value;
            }

            if (!(value is ConcurrentDictionary<string, Stack<string>>))
            {
                throw new InvalidOperationException
                    (string.Format("Please do not manually set {0} in ViewData.", CapturesKey));
            }

            return (ConcurrentDictionary<string, Stack<string>>) value;
        }
    }
}