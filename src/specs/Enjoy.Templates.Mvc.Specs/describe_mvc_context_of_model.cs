using System.Web.Mvc;
using Enjoy.Mvc;
using Enjoy.Templates.Mvc.Specs.TestClasses;
using Enjoy.Web;
using Machine.Specifications;

namespace Enjoy.Templates.Mvc.Specs
{
    [Subject(typeof (TemplateExtensions))]
    public sealed class describe_mvc_context_of_model
    {
        Establish context = () =>
            {
                memberViewProvider = new TestMemberViewProvider();
                MemberViewProviders.Providers.Add(memberViewProvider);
                templateProvider = new TestHtmlTemplateProvider();
                HtmlTemplateProviders.Providers.Add(templateProvider);
                sut = new HtmlHelper<Model>(new ViewContext(), new ViewPage());
            };

        public sealed class when_getting_the_view_for_a_member_expression
        {
            Because of = () => result = sut.ViewFor(model => model.String);
            It should_be_the_same_as_the_view_provider_view = () => result.ShouldBeTheSameAs(memberViewProvider.View);
            static object result;
        }

        public sealed class when_showing_editor_for_a_member_expression
        {
            Because of = () => sut.EditorFor(model => model.String);

            It should_pass_the_view_into_the_template_provider =
                () => templateProvider.ViewType.ShouldEqual(memberViewProvider.View.GetType());
        }

        static TestMemberViewProvider memberViewProvider;
        static TestHtmlTemplateProvider templateProvider;
        static HtmlHelper<Model> sut;
    }
}