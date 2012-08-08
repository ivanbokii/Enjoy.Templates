using System;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    public interface IHtmlTemplateProvider
    {
        // Should I make this return IHtmlString?
        Maybe<Func<object, string>> TemplateFor(Type viewType);
    }
}