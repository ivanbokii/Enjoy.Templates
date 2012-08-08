using System;
using System.IO;
using HtmlTags;

namespace Enjoy.Web
{
    public static class HtmlTagExtensions
    {
        public static IDisposable Begin(this HtmlTag htmlTag, TextWriter writer)
        {
            return new DisposableHtmlTagWriter(writer, htmlTag);
        }
    }
}