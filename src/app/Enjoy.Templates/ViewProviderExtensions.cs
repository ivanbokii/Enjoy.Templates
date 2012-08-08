using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Enjoy.Util;
using Own.Failure;
using Wandering.Monads.Maybe;

namespace Enjoy
{
    public static class ViewProviderExtensions
    {
        public static Maybe<object> For<TClass>(this IObjectViewProvider provider, TClass instance)
        {
            Requires.NotNull(provider, "provider");
            return new[] {provider}.For(instance);
        }

        public static Maybe<object> For<TClass>(this IEnumerable<IObjectViewProvider> providers, TClass instance)
        {
            Requires.NotNull(providers, "providers");
            foreach (var provider in providers)
            {
                var view = provider.For(instance);
                if (view.IsJust())
                {
                    return view;
                }
            }
            return new Nothing<object>();
        }

        public static Maybe<object> For<TClass, TMember>
            (this IMemberViewProvider provider, TClass instance, Expression<Func<TClass, TMember>> memberExpr)
        {
            Requires.NotNull(provider, "provider");
            return new[] {provider}.For(instance, memberExpr);
        }

        public static Maybe<object> For<TClass, TMember>
            (this IEnumerable<IMemberViewProvider> providers, TClass instance,
             Expression<Func<TClass, TMember>> memberExpr)
        {
            var member = memberExpr.GetMemberInfo();
            return providers.For(instance, member);
        }

        public static Maybe<object> For<TClass>(this IMemberViewProvider provider, TClass instance, MemberInfo member)
        {
            Requires.NotNull(provider, "provider");
            return new[] {provider}.For(instance, member);
        }

        public static Maybe<object> For<TClass>(this IEnumerable<IMemberViewProvider> providers, TClass instance, MemberInfo member)
        {
            Requires.NotNull(providers, "providers");
            // TODO Requires.NotNull(model, "model") should not have class constraint.

            Func<object> modelAccessor = () => instance;
            foreach (var provider in providers)
            {
                var view = provider.ForMember(member, modelAccessor);
                if (view.IsJust())
                {
                    return view;
                }
            }
            return new Nothing<object>();
        }
    }
}