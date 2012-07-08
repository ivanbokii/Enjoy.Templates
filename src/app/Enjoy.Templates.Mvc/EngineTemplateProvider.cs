using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Enjoy.Web;
using Wandering.Monads.Maybe;

namespace Enjoy.Mvc
{
    public sealed class EngineTemplateProvider : IHtmlTemplateProvider
    {
        public EngineTemplateProvider(ViewContext viewContext, ViewDataDictionary viewData)
        {
            this.viewContext = viewContext;
            this.viewData = viewData;
        }

        public Maybe<Func<object, string>> GetDisplayTemplate(Type type)
        {
            return GetTemplate(type, "DisplayTemplates");
        }

        public Maybe<Func<object, string>> GetEditorTemplate(Type type)
        {
            return GetTemplate(type, "EditorTemplates");
        }

        private Maybe<Func<object, string>> GetTemplate(Type type, string templateDir)
        {
            var partialViewName = string.Format("{0}/{1}", templateDir, type.Name);
            var viewEngineResult = ViewEngines.Engines.FindPartialView(viewContext, partialViewName);

            if (viewEngineResult.View == null)
            {
                return new Nothing<Func<object, string>>();
            }

            return new Just<Func<object, string>>
                (model =>
                    {
                        var viewDataType = typeof (ViewDataDictionary<>).MakeGenericType(type);
                        var owned = (ViewDataDictionary) Activator.CreateInstance(viewDataType, model);
                        foreach (var pair in viewData)
                        {
                            owned.Add(pair);
                        }

                        using (var writer = new StringWriter(CultureInfo.InvariantCulture))
                        {
                            viewEngineResult.View.Render
                                (new ViewContext
                                     (viewContext, viewEngineResult.View, owned, viewContext.TempData, writer), writer);
                            return writer.ToString();
                        }
                    });
        }

        private readonly ViewContext viewContext;
        private readonly ViewDataDictionary viewData;
    }
}