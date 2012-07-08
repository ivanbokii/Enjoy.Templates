using System;
using System.Collections.Generic;
using Enjoy.Web;
using Wandering.Monads.Maybe;

namespace Enjoy.Mvc
{
    internal static class HtmlTemplateProviderExtensions
    {
        public static Maybe<Func<object, string>> GetEditorTemplate(this IEnumerable<IHtmlTemplateProvider> templateProviders, Type viewType)
        {
            foreach (var templateProvider in templateProviders)
            {
                var template = templateProvider.GetEditorTemplate(viewType);
                if (template.IsJust())
                {
                    return template;
                }
            }
            return new Nothing<Func<object, string>>();
        }
    }
}
