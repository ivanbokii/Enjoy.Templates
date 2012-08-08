using System;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlTags;
using Own.Failure;

namespace Enjoy.Mvc
{
    public static class LinkExtensions
    {
        public static HtmlTag Link(this HtmlHelper html, string text, string actionName)
        {
            return html.Link(text, actionName, null, new RouteValueDictionary());
        }

        public static HtmlTag Link(this HtmlHelper html, string text, string actionName, object routeValues)
        {
            return html.Link(text, actionName, null, new RouteValueDictionary(routeValues));
        }

        public static HtmlTag Link
            (this HtmlHelper html, string text, string actionName, RouteValueDictionary routeValues)
        {
            return html.Link(text, actionName, null, routeValues);
        }

        public static HtmlTag Link(this HtmlHelper html, string text, string actionName, string controllerName)
        {
            return html.Link(text, actionName, controllerName, new RouteValueDictionary());
        }

        public static HtmlTag Link
            (this HtmlHelper html, string text, string actionName, string controllerName, object routeValues)
        {
            return html.Link(text, actionName, controllerName, new RouteValueDictionary(routeValues));
        }

        public static HtmlTag Link
            (this HtmlHelper html, string text, string actionName, string controllerName,
             RouteValueDictionary routeValues)
        {
            Requires.False(text.IsBlank(), "Text must not be blank.");
            return BuildLink
                (html.ViewContext.RequestContext, html.RouteCollection, text, null, actionName, controllerName,
                 routeValues);
        }

        private static HtmlTag BuildLink
            (RequestContext requestContext, RouteCollection routeCollection, string text, string routeName,
             string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return BuildLink
                (requestContext, routeCollection, text, routeName, actionName, controllerName, null, null,
                 null, routeValues);
        }

        private static HtmlTag BuildLink
            (RequestContext requestContext, RouteCollection routeCollection, string text,
             string routeName, string actionName, string controllerName, string protocol, string hostName,
             string fragment, RouteValueDictionary routeValues, bool includeImplicitMvcValues = true)
        {
            var url = UrlHelper.GenerateUrl
                (routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection,
                 requestContext, includeImplicitMvcValues);
            return new LinkTag(text, url);
        }
    }
}