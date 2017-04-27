using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPager
{
    /// <summary>
    /// The pager helper.
    /// </summary>
    public static class PagerHelper
	{
	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="totalItemCount">
	    /// The total item count.
	    /// </param>
	    /// <param name="pageSize">
	    /// The page size.
	    /// </param>
	    /// <param name="pageIndex">
	    /// The page index.
	    /// </param>
	    /// <param name="actionName">
	    /// The action name.
	    /// </param>
	    /// <param name="controllerName">
	    /// The controller name.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
		{
			int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
			PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
			return builder.RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="totalItemCount">
	    /// The total item count.
	    /// </param>
	    /// <param name="pageSize">
	    /// The page size.
	    /// </param>
	    /// <param name="pageIndex">
	    /// The page index.
	    /// </param>
	    /// <param name="actionName">
	    /// The action name.
	    /// </param>
	    /// <param name="controllerName">
	    /// The controller name.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
			PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, htmlAttributes);
			return builder.RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
		{
			return new PagerBuilder(helper, null, pagerOptions, htmlAttributes).RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList)
		{
			if (pagedList == null)
			{
                return PagerHelper.Pager(helper, null, new Dictionary<string, object>());
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, null);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, null);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, null);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, htmlAttributes);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, null);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, null);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, pagerOptions, htmlAttributes);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, object routeValues, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, null, new RouteValueDictionary(htmlAttributes));
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="helper">
	    /// The helper.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(helper, null, htmlAttributes);
			}
			return helper.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="totalItemCount">
	    /// The total item count.
	    /// </param>
	    /// <param name="pageSize">
	    /// The page size.
	    /// </param>
	    /// <param name="pageIndex">
	    /// The page index.
	    /// </param>
	    /// <param name="actionName">
	    /// The action name.
	    /// </param>
	    /// <param name="controllerName">
	    /// The controller name.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, MvcAjaxOptions ajaxOptions, object htmlAttributes)
		{
			int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
			PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), ajaxOptions, new RouteValueDictionary(htmlAttributes));
			return builder.RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="totalItemCount">
	    /// The total item count.
	    /// </param>
	    /// <param name="pageSize">
	    /// The page size.
	    /// </param>
	    /// <param name="pageIndex">
	    /// The page index.
	    /// </param>
	    /// <param name="actionName">
	    /// The action name.
	    /// </param>
	    /// <param name="controllerName">
	    /// The controller name.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
			PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes);
			return builder.RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    private static MvcHtmlString Pager(AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
		{
			return new PagerBuilder(null, ajax, pagerOptions, htmlAttributes).RenderPager();
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, MvcAjaxOptions ajaxOptions)
		{
			if (pagedList != null)
			{
				return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, ajaxOptions, null);
			}
            return PagerHelper.Pager(ajax, null, new Dictionary<string, object>());
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
		{
			if (pagedList != null)
			{
				return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, null);
			}
			return PagerHelper.Pager(ajax, pagerOptions, null);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, pagerOptions, htmlAttributes);
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, MvcAjaxOptions ajaxOptions, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, null, new RouteValueDictionary(htmlAttributes));
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, null, htmlAttributes);
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, object htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
		}

	    /// <summary>
	    /// The pager.
	    /// </summary>
	    /// <param name="ajax">
	    /// The ajax.
	    /// </param>
	    /// <param name="pagedList">
	    /// The paged list.
	    /// </param>
	    /// <param name="routeName">
	    /// The route name.
	    /// </param>
	    /// <param name="routeValues">
	    /// The route values.
	    /// </param>
	    /// <param name="pagerOptions">
	    /// The pager options.
	    /// </param>
	    /// <param name="ajaxOptions">
	    /// The ajax options.
	    /// </param>
	    /// <param name="htmlAttributes">
	    /// The html attributes.
	    /// </param>
	    /// <returns>
	    /// The <see cref="MvcHtmlString"/>.
	    /// </returns>
	    public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (pagedList == null)
			{
				return PagerHelper.Pager(ajax, pagerOptions, htmlAttributes);
			}
			return ajax.Pager(pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
		}
	}
}