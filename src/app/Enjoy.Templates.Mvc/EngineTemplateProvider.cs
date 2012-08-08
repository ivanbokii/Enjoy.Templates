using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Enjoy.Web;
using Wandering.Monads.Maybe;

namespace Enjoy.Mvc
{
    public sealed class EngineTemplateProvider : IHtmlTemplateProvider
    {
        public EngineTemplateProvider
            (ViewContext mvcViewContext, ViewDataDictionary mvcViewData, IEnumerable<IViewEngine> mvcViewEngines)
        {
            this.mvcViewContext = mvcViewContext;
            this.mvcViewData = mvcViewData;
            this.mvcViewEngines = new ViewEngineCollection(mvcViewEngines.ToList());
        }

        public Maybe<Func<object, string>> TemplateFor(Type viewType)
        {
            var viewEngineResult = mvcViewEngines.FindPartialView(mvcViewContext, viewType.Name);

            if (viewEngineResult.View == null)
            {
                return new Nothing<Func<object, string>>();
            }

            return new Just<Func<object, string>>
                (view =>
                    {
                        var viewDataType = typeof (ViewDataDictionary<>).MakeGenericType(viewType);
                        var owned = (ViewDataDictionary) Activator.CreateInstance(viewDataType, view);
                        foreach (var pair in mvcViewData)
                        {
                            owned.Add(pair);
                        }
                        using (var writer = new StringWriter(CultureInfo.InvariantCulture))
                        {
                            viewEngineResult.View.Render
                                (new ViewContext
                                     (mvcViewContext, viewEngineResult.View, owned, mvcViewContext.TempData, writer),
                                 writer);
                            return writer.ToString();
                        }
                    });
        }

        private readonly ViewContext mvcViewContext;
        private readonly ViewDataDictionary mvcViewData;
        private readonly ViewEngineCollection mvcViewEngines;
    }
}