using System;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlTags;

namespace Enjoy.Mvc
{
    public static class FormExtensions
    {
        public static HtmlTag Form(this HtmlHelper html)
        {
            var rawUrl = html.ViewContext.HttpContext.Request.RawUrl;
            return BuildForm(rawUrl, HttpVerbs.Post);
        }

        public static HtmlTag Form(this HtmlHelper html, object routeValues)
        {
            return html.Form(null, null, new RouteValueDictionary(routeValues), HttpVerbs.Post);
        }

        public static HtmlTag Form(this HtmlHelper html, RouteValueDictionary routeValues)
        {
            return html.Form(null, null, routeValues, HttpVerbs.Post);
        }

        public static HtmlTag Form(this HtmlHelper html, string actionName, string controllerName)
        {
            return html.Form(actionName, controllerName, new RouteValueDictionary(), HttpVerbs.Post);
        }

        public static HtmlTag Form
            (this HtmlHelper html, string actionName, string controllerName, object routeValues)
        {
            return html.Form(actionName, controllerName, new RouteValueDictionary(routeValues), HttpVerbs.Post);
        }

        public static HtmlTag Form
            (this HtmlHelper html, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return html.Form(actionName, controllerName, routeValues, HttpVerbs.Post);
        }

        public static HtmlTag Form(this HtmlHelper html, string actionName, string controllerName, HttpVerbs method)
        {
            return html.Form(actionName, controllerName, new RouteValueDictionary(), method);
        }

        public static HtmlTag Form
            (this HtmlHelper html, string actionName, string controllerName, object routeValues, HttpVerbs method)
        {
            return html.Form(actionName, controllerName, new RouteValueDictionary(routeValues), method);
        }

        public static HtmlTag Form
            (this HtmlHelper html, string actionName, string controllerName, RouteValueDictionary routeValues,
             HttpVerbs method)
        {
            var action = UrlHelper.GenerateUrl
                (null, actionName, controllerName, routeValues, html.RouteCollection,
                 html.ViewContext.RequestContext, true);
            return BuildForm(action, method);
        }

        private static HtmlTag BuildForm(string url, HttpVerbs method)
        {
            var tag = new FormTag(url);
            return tag.Method(method);
        }

        private static HtmlTag Method(this FormTag tag, HttpVerbs method)
        {
            switch (method)
            {
                case HttpVerbs.Get:
                    return tag.Method("GET");
                case HttpVerbs.Post:
                    return tag.Method("POST");
                case HttpVerbs.Put:
                case HttpVerbs.Delete:
                case HttpVerbs.Head:
                    return tag.Method("POST").MethodOverride(method);
            }
            throw new InvalidOperationException
                (string.Format("The {0} form method is not currently supported.", method));
        }

        private static HtmlTag MethodOverride(this HtmlTag tag, HttpVerbs method)
        {
            var hidden = new HiddenTag().Attr("name", "X-HTTP-Method-Override");
            switch (method)
            {
                case HttpVerbs.Put:
                    return tag.Append(hidden.Attr("value", "PUT"));
                case HttpVerbs.Delete:
                    return tag.Append(hidden.Attr("value", "DELETE"));
                case HttpVerbs.Head:
                    return tag.Append(hidden.Attr("value", "HEAD"));
            }
            throw new InvalidOperationException(string.Format("There is no need for an override for {0}.", method));
        }
    }
}