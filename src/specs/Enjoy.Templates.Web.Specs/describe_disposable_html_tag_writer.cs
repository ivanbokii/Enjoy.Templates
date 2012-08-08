using System.IO;
using Enjoy.Web;
using HtmlTags;
using Machine.Specifications;

namespace Enjoy.Templates.Web.Specs
{
    [Subject(typeof (DisposableHtmlTagWriter))]
    public sealed class describe_disposable_html_tag_writer
    {
        Establish context = () => writer = new StringWriter();

        public sealed class when_disposing
        {
            Establish context = () => htmlTag = new HtmlTag("form");
            Because of = () => new DisposableHtmlTagWriter(writer, htmlTag).Dispose();
            It should_not_write_the_tag = () => writer.ToString().ShouldEqual("<form>\r\n\r\n</form>");
        }

        public sealed class when_writing
        {
            Establish context = () => htmlTag = new HtmlTag("form");
            Because of = () => sut = new DisposableHtmlTagWriter(writer, htmlTag);
            It should_not_write_the_tag = () => writer.ToString().ShouldEqual("<form>\r\n");
        }

        static StringWriter writer;
        static DisposableHtmlTagWriter sut;
        static HtmlTag htmlTag;
    }
}