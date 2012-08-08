using System.Web.Mvc;
using Enjoy.Mvc;
using Enjoy.Templates.Mvc.Specs.TestClasses;
using Enjoy.Web;
using Machine.Specifications;

namespace Enjoy.Templates.Mvc.Specs
{
    [Subject(typeof (TemplateExtensions))]
    public sealed class describe_mvc_context
    {
        Establish context = () =>
            {
                objectViewProvider = new TestObjectViewProvider();
                ObjectViewProviders.Providers.Add(objectViewProvider);
                memberViewProvider = new TestMemberViewProvider();
                MemberViewProviders.Providers.Add(memberViewProvider);
                templateProvider = new TestHtmlTemplateProvider();
                HtmlTemplateProviders.Providers.Add(templateProvider);
                sut = new HtmlHelper(mvcViewContext, mvcViewDataContainer);
                instance = new object();
            };

        public sealed class when_getting_the_view_for_an_object
        {
            Because of = () => result = sut.ViewFor(instance);
            It should_be_the_same_as_the_view_provider_view = () => result.ShouldBeTheSameAs(memberViewProvider.View);
            static object result;
        }

        public sealed class when_showing_editor_for_an_object
        {
            Because of = () => sut.EditorFor(instance);

            It should_pass_the_instance_into_the_view_provider =
                () => objectViewProvider.Instance.ShouldBeTheSameAs(instance);

            It should_pass_the_view_into_the_template_provider =
                () => templateProvider.ViewType.ShouldEqual(objectViewProvider.View.GetType());
        }

        static ViewContext mvcViewContext;
        static ViewPage mvcViewDataContainer;
        static TestMvcViewEngine mvcViewEngine;
        static TestObjectViewProvider objectViewProvider;
        static TestMemberViewProvider memberViewProvider;
        static TestHtmlTemplateProvider templateProvider;
        static HtmlHelper sut;
        static object instance;
    }
}