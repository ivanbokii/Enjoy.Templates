using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Enjoy.Util;
using Enjoy.Web;
using Fasterflect;

namespace Enjoy.Mvc
{
    public class MvcContext<TModel> : MvcContext
    {
        public MvcContext
            (ViewContext viewContext, IViewDataContainer viewDataContainer,
             IEnumerable<IObjectViewProvider> objectViewProviders, IEnumerable<IMemberViewProvider> memberViewProviders,
             IEnumerable<IHtmlTemplateProvider> templateProviders)
            : base(viewContext, viewDataContainer, templateProviders)
        {
            viewData = new ViewDataDictionary<TModel>(viewDataContainer.ViewData);
            this.objectViewProviders = objectViewProviders;
            this.memberViewProviders = memberViewProviders;
        }

        /// <summary>
        /// Returns Enjoy.Templates.View (if found), or the Model.
        /// </summary>
        public object GetTemplateView()
        {
            // First, try to find a View using the providers.
            var view = objectViewProviders.For(viewData.Model);
            if (view.IsJust())
            {
                return view.FromJust();
            }

            // Otherwise, just return the Model.
            return viewData.Model;
        }

        /// <summary>
        /// Returns Enjoy.Templates.View (if found), or the Member value.
        /// </summary>
        public object GetTemplateView<TMember>(Expression<Func<TModel, TMember>> memberExpr)
        {
            // First, try to find a View using the providers.
            var view = memberViewProviders.For(viewData.Model, memberExpr);
            if (view.IsJust())
            {
                return view.FromJust();
            }

            // Otherwise, just return the Member value.
            var member = memberExpr.GetMemberInfo();
            return member.Get(viewData.Model);
        }

        private readonly IEnumerable<IMemberViewProvider> memberViewProviders;
        private readonly IEnumerable<IObjectViewProvider> objectViewProviders;
        private readonly ViewDataDictionary<TModel> viewData;
    }
}