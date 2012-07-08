using System;
using System.Linq.Expressions;
using System.Web;

namespace Enjoy.Mvc
{
    public static class MvcExtensions
    {
        public static HtmlString EditorForModel<TModel>(this MvcContext<TModel> context)
        {
            var view = context.GetTemplateView();
            var template = context.FindEditorTemplate(view.GetType());
            return new HtmlString(template(view));
        }

        public static HtmlString EditorFor<TModel, TMember>
            (this MvcContext<TModel> context, Expression<Func<TModel, TMember>> memberExpr)
        {
            // 1. Find a View; fall-through to MemberValue.
            var view = context.GetTemplateView(memberExpr);

            // 2. Find an EditorTemplate.
            var template = context.FindEditorTemplate(view.GetType());

            // 3. Call the Template.
            return new HtmlString(template(view));
        }
    }
}