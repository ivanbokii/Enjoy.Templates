using System.Web.Mvc;
using Enjoy.Mvc;
using Enjoy.Templates.Mvc.Specs.TestClasses;
using Machine.Specifications;

namespace Enjoy.Templates.Mvc.Specs
{
    [Subject(typeof (EngineTemplateProvider))]
    public sealed class describe_engine_template_provider
    {
        Establish context = () =>
            {
                mvcViewContext = new ViewContext();
                mvcViewData = new ViewDataDictionary();
                mvcViewEngine = new TestMvcViewEngine();
                view = new object();
                sut = new EngineTemplateProvider(mvcViewContext, mvcViewData, new[] {mvcViewEngine});
            };

        public sealed class when_getting_the_template_for_a_view_type
        {
            Because of = () => sut.TemplateFor(view.GetType());

            It should_pass_the_mvc_view_context_into_the_view_engine = () => mvcViewEngine.ControllerContext.ShouldBeTheSameAs(mvcViewContext);

            It should_pass_the_view_type_name_into_the_view_engine =
                () => mvcViewEngine.PartialViewName.ShouldEqual(view.GetType().Name);
        }

        public sealed class when_running_the_template_for_a_view_type
        {
            Because of = () => result = sut.TemplateFor(view.GetType()).FromJust()(view);

            It should_pass_the_mvc_view_context_into_the_view =
                () => mvcViewEngine.View.ViewContext.ShouldBeTheSameAs(mvcViewContext);

            It should_equal_the_mvc_view_output = () => result.ShouldEqual(mvcViewEngine.View.Output);

            static string result;
        }

        static ViewContext mvcViewContext;
        static ViewDataDictionary mvcViewData;
        static TestMvcViewEngine mvcViewEngine;
        static object view;
        static EngineTemplateProvider sut;
    }
}