using System.Collections.Generic;
using System.Web.Mvc;
using Enjoy.Web;

namespace Enjoy.Mvc
{
    public abstract class EnjoyViewPage : WebViewPage
    {
        public new MvcContext Html { get; set; }

        /// <remarks>
        /// Make sure this is set through property injection.
        /// </remarks>
        public IEnumerable<IHtmlTemplateProvider> TemplateProviders { get; set; }

        public override void InitHelpers()
        {
            Html = new MvcContext(ViewContext, this, TemplateProviders);
        }
    }
}