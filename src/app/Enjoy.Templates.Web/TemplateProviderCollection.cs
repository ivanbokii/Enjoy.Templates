using System;
using System.Collections.ObjectModel;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    public sealed class HtmlTemplateProviderCollection : Collection<IHtmlTemplateProvider>
    {
    }

    public static class HtmlTemplateProviders
    {
        public static HtmlTemplateProviderCollection Providers
        {
            get { return Collection; }
        }

        public static Maybe<Func<object, string>> For(Type viewType)
        {
            return Providers.For(viewType);
        }

        private static readonly HtmlTemplateProviderCollection Collection = new HtmlTemplateProviderCollection();
    }
}