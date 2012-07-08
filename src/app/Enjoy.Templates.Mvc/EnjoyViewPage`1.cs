using System.Collections.Generic;
using System.Web.Mvc;
using Enjoy.Web;

namespace Enjoy.Mvc
{
    public abstract class EnjoyViewPage<TModel> : WebViewPage<TModel>
    {
        public new MvcContext<TModel> Html { get; set; }

        /// <remarks>
        /// Make sure this is set through property injection.
        /// </remarks>
        public IEnumerable<IMemberViewProvider> MemberViewProviders { get; set; }

        /// <remarks>
        /// Make sure this is set through property injection.
        /// </remarks>
        public IEnumerable<IObjectViewProvider> ObjectViewProviders { get; set; }

        /// <remarks>
        /// Make sure this is set through property injection.
        /// </remarks>
        public IEnumerable<IHtmlTemplateProvider> TemplateProviders { get; set; }

        public override void InitHelpers()
        {
            Html = new MvcContext<TModel>(ViewContext, this, ObjectViewProviders, MemberViewProviders, TemplateProviders);
        }
    }
}