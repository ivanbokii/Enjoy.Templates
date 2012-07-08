using System;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    public interface IHtmlTemplateProvider
    {
        Maybe<Func<object, string>> GetDisplayTemplate(Type type);
        Maybe<Func<object, string>> GetEditorTemplate(Type type);
    }
}