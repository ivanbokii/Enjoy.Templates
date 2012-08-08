using System;
using Enjoy.Web;
using Wandering.Monads.Maybe;

namespace Enjoy.Templates.Mvc.Specs.TestClasses
{
    public sealed class TestHtmlTemplateProvider : IHtmlTemplateProvider
    {
        public Type ViewType { get; set; }

        public Maybe<Func<object, string>> TemplateFor(Type viewType)
        {
            ViewType = viewType;
            return new Just<Func<object, string>>(instance => string.Empty);
        }
    }
}