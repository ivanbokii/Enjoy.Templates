using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Wandering.Monads.Maybe;

namespace Enjoy
{
    public sealed class MemberViewProviderCollection : Collection<IMemberViewProvider>
    {
    }

    public static class MemberViewProviders
    {
        public static MemberViewProviderCollection Providers
        {
            get { return Collection; }
        }

        public static Maybe<object> For<TClass, TMember>(TClass instance, Expression<Func<TClass, TMember>> memberExpr)
        {
            return Providers.For(instance, memberExpr);
        }

        private static readonly MemberViewProviderCollection Collection = new MemberViewProviderCollection();
    }
}