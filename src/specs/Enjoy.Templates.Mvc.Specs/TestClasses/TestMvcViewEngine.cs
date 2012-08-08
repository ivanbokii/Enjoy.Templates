using System;
using System.Web.Mvc;

namespace Enjoy.Templates.Mvc.Specs.TestClasses
{
    internal sealed class TestMvcViewEngine : IViewEngine
    {
        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            ControllerContext = controllerContext;
            PartialViewName = partialViewName;
            View = new TestMvcView();
            return new ViewEngineResult(View, this);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            throw new NotImplementedException();
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            throw new NotImplementedException();
        }

        public ControllerContext ControllerContext { get; set; }
        public string PartialViewName { get; set; }
        public TestMvcView View { get; set; }
    }
}
