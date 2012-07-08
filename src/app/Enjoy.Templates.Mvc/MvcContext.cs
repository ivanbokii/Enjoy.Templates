using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Enjoy.Mvc.Util;
using Enjoy.Web;

namespace Enjoy.Mvc
{
    public class MvcContext
    {
        public MvcContext(ViewContext viewContext, IViewDataContainer viewDataContainer, IEnumerable<IHtmlTemplateProvider> templateProviders)
        {
            this.templateProviders = new[] {new EngineTemplateProvider(viewContext, viewDataContainer.ViewData)};
            if (templateProviders != null)
            {
                this.templateProviders = this.templateProviders.Concat(templateProviders).ToList();
            }
        }

        public Func<object, string> FindEditorTemplate(Type viewType)
        {
            var views = viewType.GetTypeAncestry().ToList();
            foreach (var view in views)
            {
                var template = templateProviders.GetEditorTemplate(view);
                if (template.IsJust())
                {
                    return template.FromJust();
                }
            }
            throw new InvalidOperationException
                (string.Format
                     ("Could not find editor template for {0}.", string.Join(" or ", views.Select(type => type.Name))));
        }

        private readonly IList<IHtmlTemplateProvider> templateProviders;
    }
}