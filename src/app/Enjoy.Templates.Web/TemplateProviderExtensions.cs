using System;
using System.Collections.Generic;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    public static class TemplateProviderExtensions
    {
        public static Maybe<Func<object, string>> For(this IEnumerable<IHtmlTemplateProvider> templateProviders, Type viewType)
        {
            foreach (var templateProvider in templateProviders)
            {
                var template = templateProvider.TemplateFor(viewType);
                if (template.IsJust())
                {
                    return template;
                }
            }
            return new Nothing<Func<object, string>>();
        }
    }
}