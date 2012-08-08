using System.IO;
using System.Web.Mvc;

namespace Enjoy.Templates.Mvc.Specs.TestClasses
{
    internal sealed class TestMvcView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            ViewContext = viewContext;
            Output = "Hi there!";
            writer.Write(Output);
        }

        public ViewContext ViewContext { get; set; }
        public string Output { get; set; }
    }
}
