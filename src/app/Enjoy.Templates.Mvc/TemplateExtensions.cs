using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Enjoy.Mvc.Util;
using Enjoy.Util;
using Enjoy.Web;
using Fasterflect;

namespace Enjoy.Mvc
{
    public static class TemplateExtensions
    {
        /// <summary>
        /// Returns Enjoy.Templates.View (if found), or the instance.
        /// </summary>
        public static object ViewFor(this HtmlHelper html, object instance)
        {
            var view = ObjectViewProviders.For(instance);
            return view.IsJust() ? view.FromJust() : instance;
        }

        /// <summary>
        /// Returns Enjoy.Templates.View (if found), or the Member value.
        /// </summary>
        public static object ViewFor<TModel, TMember>
            (this HtmlHelper<TModel> html, Expression<Func<TModel, TMember>> memberExpr)
        {
            var model = html.ViewData.Model;

            // First, try to find a View using the providers.
            var view = MemberViewProviders.For(model, memberExpr);
            if (view.IsJust())
            {
                return view.FromJust();
            }

            // Otherwise, just return the Member value.
            var member = memberExpr.GetMemberInfo();
            return member.Get(model);
        }

        public static Func<object, HtmlString> TemplateFor(this HtmlHelper context, object view)
        {
            // NOTE: This may not be the best way to solve the problem of how
            // to get these pieces of data, but it is the simplest for now.
            var templateProviders =
                new[] {new EngineTemplateProvider(context.ViewContext, context.ViewData, ViewEngines.Engines)}.Concat
                    (HtmlTemplateProviders.Providers).ToArray();

            var viewTypes = view.GetType().GetTypeAncestry().ToList();
            foreach (var viewType in viewTypes)
            {
                var template = templateProviders.For(viewType);
                if (template.IsJust())
                {
                    return instance => new HtmlString(template.FromJust()(instance));
                }
            }
            throw new InvalidOperationException
                (string.Format
                     ("Could not find editor template for {0}.",
                      string.Join(" or ", viewTypes.Select(type => type.Name))));
        }
    }
}