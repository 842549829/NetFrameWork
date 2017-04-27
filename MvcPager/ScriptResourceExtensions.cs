using System.Web.Mvc;
using System.Web.UI;

namespace MvcPager
{
    /// <summary>
    /// The script resource extensions.
    /// </summary>
    public static class ScriptResourceExtensions
	{
        /// <summary>
        /// The register mvc pager script resource.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        public static void RegisterMvcPagerScriptResource(this HtmlHelper html)
		{
			Page page = html.ViewContext.HttpContext.CurrentHandler as Page;
			string scriptUrl = (page ?? new Page()).ClientScript.GetWebResourceUrl(typeof(PagerHelper), "Webdiyer.WebControls.Mvc.MvcPager.min.js");
			html.ViewContext.Writer.Write("<script type=\"text/javascript\" src=\"" + scriptUrl + "\"></script>");
		}
	}
}
