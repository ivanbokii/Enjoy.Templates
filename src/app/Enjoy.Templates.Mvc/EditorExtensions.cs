using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Enjoy.Mvc
{
    public static class EditorExtensions
    {
        public static HtmlString EditorFor(this HtmlHelper html, object instance)
        {
            var view = html.ViewFor(instance);
            var template = html.TemplateFor(view);
            return template(view);
        }

        public static HtmlString EditorFor<TModel, TMember>
            (this HtmlHelper<TModel> html, Expression<Func<TModel, TMember>> memberExpr)
        {
            var view = html.ViewFor(memberExpr);
            var template = html.TemplateFor(view);
            return template(view);
        }
    }
}