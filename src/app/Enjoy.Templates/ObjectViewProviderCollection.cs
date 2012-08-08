using System.Collections.ObjectModel;
using Wandering.Monads.Maybe;

namespace Enjoy
{
    public sealed class ObjectViewProviderCollection : Collection<IObjectViewProvider>
    {
    }

    public static class ObjectViewProviders
    {
        public static ObjectViewProviderCollection Providers
        {
            get { return Collection; }
        }

        public static Maybe<object> For<TClass>(TClass instance)
        {
            return Providers.For(instance);
        }

        private static readonly ObjectViewProviderCollection Collection = new ObjectViewProviderCollection();
    }
}