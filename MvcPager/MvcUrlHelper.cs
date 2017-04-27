using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPager
{
    public class MvcUrlHelper
    {
        private static UrlRewriterHelper _urlRewriterHelper = new UrlRewriterHelper();

        public static string GenerateUrl(string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues, IEnumerable<string> arrayParameter)
        {
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            RouteValueDictionary values = MergeRouteValues(actionName, controllerName, requestContext.RouteData.Values, routeValues, includeImplicitMvcValues, arrayParameter);
            VirtualPathData data = routeCollection.GetVirtualPathForArea(requestContext, routeName, values);
            if (data == null)
            {
                return null;
            }
            return GenerateClientUrl(requestContext.HttpContext, data.VirtualPath);
        }

        public static RouteValueDictionary MergeRouteValues(string actionName, string controllerName, RouteValueDictionary implicitRouteValues, RouteValueDictionary routeValues, bool includeImplicitMvcValues, IEnumerable<string> arrayParameter)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();
            if (includeImplicitMvcValues)
            {
                object obj2;
                if ((implicitRouteValues != null) && implicitRouteValues.TryGetValue("action", out obj2))
                {
                    dictionary["action"] = obj2;
                }
                if ((implicitRouteValues != null) && implicitRouteValues.TryGetValue("controller", out obj2))
                {
                    dictionary["controller"] = obj2;
                }
            }
            if (routeValues != null)
            {
                foreach (KeyValuePair<string, object> pair in GetRouteValues(routeValues))
                {
                    if (arrayParameter != null && arrayParameter.Contains(pair.Key))
                    {
                        var val = pair.Value.ToString().Split(',');
                        int index = 0;
                        foreach (var item in val)
                        {
                            dictionary[pair.Key + "[" + index + "]"] = item;
                            index++;
                        }
                    }
                    else
                    {
                        dictionary[pair.Key] = pair.Value;
                    }

                }
            }
            if (actionName != null)
            {
                dictionary["action"] = actionName;
            }
            if (controllerName != null)
            {
                dictionary["controller"] = controllerName;
            }
            return dictionary;
        }

        public static RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                return new RouteValueDictionary();
            }
            return new RouteValueDictionary(routeValues);
        }

        public static string GenerateClientUrl(HttpContextBase httpContext, string contentPath)
        {
            string str;
            if (string.IsNullOrEmpty(contentPath))
            {
                return contentPath;
            }
            contentPath = StripQuery(contentPath, out str);
            return (GenerateClientUrlInternal(httpContext, contentPath) + str);
        }

        private static string StripQuery(string path, out string query)
        {
            int index = path.IndexOf('?');
            if (index >= 0)
            {
                query = path.Substring(index);
                return path.Substring(0, index);
            }
            query = null;
            return path;
        }

        private static string GenerateClientUrlInternal(HttpContextBase httpContext, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                return contentPath;
            }
            if (contentPath[0] == '~')
            {
                string virtualPath = VirtualPathUtility.ToAbsolute(contentPath, httpContext.Request.ApplicationPath);
                string str2 = httpContext.Response.ApplyAppPathModifier(virtualPath);
                return GenerateClientUrlInternal(httpContext, str2);
            }
            if (!_urlRewriterHelper.WasRequestRewritten(httpContext))
            {
                return contentPath;
            }
            string relativePath = MakeRelative(httpContext.Request.Path, contentPath);
            return MakeAbsolute(httpContext.Request.RawUrl, relativePath);
        }

        public static string MakeAbsolute(string basePath, string relativePath)
        {
            string str;
            basePath = StripQuery(basePath, out str);
            return VirtualPathUtility.Combine(basePath, relativePath);
        }

        public static string MakeRelative(string fromPath, string toPath)
        {
            string str = VirtualPathUtility.MakeRelative(fromPath, toPath);
            if (!string.IsNullOrEmpty(str) && (str[0] != '?'))
            {
                return str;
            }
            return ("./" + str);
        }

        internal static void ResetUrlRewriterHelper()
        {
            _urlRewriterHelper = new UrlRewriterHelper();
        }
    }

    internal class UrlRewriterHelper
    {
        private object _lockObject = new object();
        private volatile bool _urlRewriterIsTurnedOnCalculated;
        private bool _urlRewriterIsTurnedOnValue;
        private const string UrlRewriterEnabledServerVar = "IIS_UrlRewriteModule";
        private const string UrlWasRewrittenServerVar = "IIS_WasUrlRewritten";

        private bool IsUrlRewriterTurnedOn(HttpContextBase httpContext)
        {
            if (!this._urlRewriterIsTurnedOnCalculated)
            {
                lock (this._lockObject)
                {
                    if (!this._urlRewriterIsTurnedOnCalculated)
                    {
                        NameValueCollection serverVariables = httpContext.Request.ServerVariables;
                        bool flag = (serverVariables != null) && (serverVariables["IIS_UrlRewriteModule"] != null);
                        this._urlRewriterIsTurnedOnValue = flag;
                        this._urlRewriterIsTurnedOnCalculated = true;
                    }
                }
            }
            return this._urlRewriterIsTurnedOnValue;
        }

        public virtual bool WasRequestRewritten(HttpContextBase httpContext)
        {
            return (this.IsUrlRewriterTurnedOn(httpContext) && WasThisRequestRewritten(httpContext));
        }

        private static bool WasThisRequestRewritten(HttpContextBase httpContext)
        {
            NameValueCollection serverVariables = httpContext.Request.ServerVariables;
            return ((serverVariables != null) && (serverVariables["IIS_WasUrlRewritten"] != null));
        }
    }
}
