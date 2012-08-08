using System.IO;
using System.Web.UI;
using HtmlTags;

namespace Enjoy.Web
{
    internal class DisposableHtmlTagWriter : HtmlTextWriter
    {
        public DisposableHtmlTagWriter(TextWriter writer, HtmlTag htmlTag) : base(writer)
        {
            originalWriter = writer;
            htmlTag.ToString(this);
            originalWriter.Write(forBeginTag.ToString());
        }

        public override void RenderBeginTag(HtmlTextWriterTag tagKey)
        {
            InnerWriter = forBeginTag;
            base.RenderBeginTag(tagKey);
        }

        public override void RenderEndTag()
        {
            InnerWriter = forEndTag;
            base.RenderEndTag();
        }

        protected override void Dispose(bool disposing)
        {
            originalWriter.Write(forEndTag.ToString());
        }

        private readonly StringWriter forBeginTag = new StringWriter();
        private readonly StringWriter forEndTag = new StringWriter();
        private readonly TextWriter originalWriter;
    }
}