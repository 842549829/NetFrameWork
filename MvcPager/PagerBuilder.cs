using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPager
{
    /// <summary>
    /// The pager builder.
    /// </summary>
    internal class PagerBuilder
	{
        /// <summary>
        /// The _html.
        /// </summary>
        private readonly HtmlHelper _html;

        /// <summary>
        /// The _ajax.
        /// </summary>
        private readonly AjaxHelper _ajax;

        /// <summary>
        /// The _action name.
        /// </summary>
        private readonly string _actionName;

        /// <summary>
        /// The _controller name.
        /// </summary>
        private readonly string _controllerName;

        /// <summary>
        /// The _total page count.
        /// </summary>
        private readonly int _totalPageCount = 1;

        /// <summary>
        /// The _page index.
        /// </summary>
        private readonly int _pageIndex;

        /// <summary>
        /// The _pager options.
        /// </summary>
        private readonly PagerOptions _pagerOptions;

        /// <summary>
        /// The _route values.
        /// </summary>
        private readonly RouteValueDictionary _routeValues;

        /// <summary>
        /// The _route name.
        /// </summary>
        private readonly string _routeName;

        /// <summary>
        /// The _start page index.
        /// </summary>
        private readonly int _startPageIndex = 1;

        /// <summary>
        /// The _end page index.
        /// </summary>
        private readonly int _endPageIndex = 1;

        /// <summary>
        /// The _ajax paging enabled.
        /// </summary>
        private readonly bool _ajaxPagingEnabled;

        /// <summary>
        /// The _ajax options.
        /// </summary>
        private readonly MvcAjaxOptions _ajaxOptions;

        /// <summary>
        /// The _html attributes.
        /// </summary>
        private IDictionary<string, object> _htmlAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerBuilder"/> class.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="ajax">
        /// The ajax.
        /// </param>
        /// <param name="pagerOptions">
        /// The pager options.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
		{
			if (pagerOptions == null)
			{
				pagerOptions = new PagerOptions();
			}
			this._html = html;
			this._ajax = ajax;
			this._pagerOptions = pagerOptions;
			this._htmlAttributes = htmlAttributes;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerBuilder"/> class.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="ajax">
        /// The ajax.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="totalPageCount">
        /// The total page count.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
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
        /// <param name="ajaxOptions">
        /// The ajax options.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			this._ajaxPagingEnabled = (ajax != null);
			if (pagerOptions == null)
			{
				pagerOptions = new PagerOptions();
			}
			this._html = html;
			this._ajax = ajax;
			this._actionName = actionName;
			this._controllerName = controllerName;
			if (pagerOptions.MaxPageIndex == 0 || pagerOptions.MaxPageIndex > totalPageCount)
			{
				this._totalPageCount = totalPageCount;
			}
			else
			{
				this._totalPageCount = pagerOptions.MaxPageIndex;
			}
			this._pageIndex = pageIndex;
			this._pagerOptions = pagerOptions;
			this._routeName = routeName;
			this._routeValues = routeValues;
			this._ajaxOptions = ajaxOptions;
			this._htmlAttributes = htmlAttributes;
			this._startPageIndex = pageIndex - pagerOptions.NumericPagerItemCount / 2;
			if (this._startPageIndex + pagerOptions.NumericPagerItemCount > this._totalPageCount)
			{
				this._startPageIndex = this._totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
			}
			if (this._startPageIndex < 1)
			{
				this._startPageIndex = 1;
			}
			this._endPageIndex = this._startPageIndex + this._pagerOptions.NumericPagerItemCount - 1;
			if (this._endPageIndex > this._totalPageCount)
			{
				this._endPageIndex = this._totalPageCount;
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerBuilder"/> class.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="totalPageCount">
        /// The total page count.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
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
        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, null, htmlAttributes)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerBuilder"/> class.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="totalPageCount">
        /// The total page count.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
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
        /// <param name="ajaxOptions">
        /// The ajax options.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        internal PagerBuilder(AjaxHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, MvcAjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) : this(null, helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
		{
		}

        /// <summary>
        /// The add previous.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddPrevious(ICollection<PagerItem> results)
		{
			PagerItem item = new PagerItem(this._pagerOptions.PrevPageText, this._pageIndex - 1, this._pageIndex == 1, PagerItemType.PrevPage);
			if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
			{
				results.Add(item);
			}
		}

        /// <summary>
        /// The add first.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddFirst(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.FirstPageText, 1, this._pageIndex == 1, PagerItemType.FirstPage);
			if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
			{
				results.Add(item);
			}
		}

        /// <summary>
        /// The add more before.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddMoreBefore(ICollection<PagerItem> results)
		{
			if (this._startPageIndex > 1 && this._pagerOptions.ShowMorePagerItems)
			{
				int index = this._startPageIndex - 1;
				if (index < 1)
				{
					index = 1;
				}
				PagerItem item = new PagerItem(this._pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
				results.Add(item);
			}
		}

        /// <summary>
        /// The add page numbers.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddPageNumbers(ICollection<PagerItem> results)
		{
			for (int pageIndex = this._startPageIndex; pageIndex <= this._endPageIndex; pageIndex++)
			{
				string text = pageIndex.ToString(CultureInfo.InvariantCulture);
				if (pageIndex == this._pageIndex && !string.IsNullOrEmpty(this._pagerOptions.CurrentPageNumberFormatString))
				{
					text = string.Format(this._pagerOptions.CurrentPageNumberFormatString, text);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._pagerOptions.PageNumberFormatString))
					{
						text = string.Format(this._pagerOptions.PageNumberFormatString, text);
					}
				}
				PagerItem item = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
				results.Add(item);
			}
		}

        /// <summary>
        /// The add more after.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddMoreAfter(ICollection<PagerItem> results)
		{
			if (this._endPageIndex < this._totalPageCount)
			{
				int index = this._startPageIndex + this._pagerOptions.NumericPagerItemCount;
				if (index > this._totalPageCount)
				{
					index = this._totalPageCount;
				}
				PagerItem item = new PagerItem(this._pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
				results.Add(item);
			}
		}

        /// <summary>
        /// The add next.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddNext(ICollection<PagerItem> results)
		{
			PagerItem item = new PagerItem(this._pagerOptions.NextPageText, this._pageIndex + 1, this._pageIndex >= this._totalPageCount, PagerItemType.NextPage);
			if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
			{
				results.Add(item);
			}
		}

        /// <summary>
        /// The add last.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void AddLast(ICollection<PagerItem> results)
		{
			PagerItem item = new PagerItem(this._pagerOptions.LastPageText, this._totalPageCount, this._pageIndex >= this._totalPageCount, PagerItemType.LastPage);
			if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
			{
				results.Add(item);
			}
		}

        /// <summary>
        /// The generate url.
        /// </summary>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateUrl(int pageIndex)
		{
			ViewContext viewContext = (this._ajax == null) ? this._html.ViewContext : this._ajax.ViewContext;
			if (pageIndex > this._totalPageCount || pageIndex == this._pageIndex)
			{
				return null;
			}
			RouteValueDictionary routeValues = new RouteValueDictionary(viewContext.RouteData.Values);
			this.AddQueryStringToRouteValues(routeValues, viewContext);
			if (this._routeValues != null && this._routeValues.Count > 0)
			{
				foreach (KeyValuePair<string, object> de in this._routeValues)
				{
					if (!routeValues.ContainsKey(de.Key))
					{
						routeValues.Add(de.Key, de.Value);
					}
					else
					{
						routeValues[de.Key] = de.Value;
					}
				}
			}
			object pageValue = viewContext.RouteData.Values[this._pagerOptions.PageIndexParameterName];
			string routeName = this._routeName;
			if (pageIndex == 0)
			{
				routeValues[this._pagerOptions.PageIndexParameterName] = "__" + this._pagerOptions.PageIndexParameterName + "__";
			}
			else
			{
				if (pageIndex == 1)
				{
					if (!string.IsNullOrWhiteSpace(this._pagerOptions.FirstPageRouteName))
					{
						routeName = this._pagerOptions.FirstPageRouteName;
						routeValues.Remove(this._pagerOptions.PageIndexParameterName);
						viewContext.RouteData.Values.Remove(this._pagerOptions.PageIndexParameterName);
					}
					else
					{
						Route curRoute = viewContext.RouteData.Route as Route;
						if (curRoute != null && (curRoute.Defaults[this._pagerOptions.PageIndexParameterName] == UrlParameter.Optional || !curRoute.Url.Contains("{" + this._pagerOptions.PageIndexParameterName + "}")))
						{
							routeValues.Remove(this._pagerOptions.PageIndexParameterName);
							viewContext.RouteData.Values.Remove(this._pagerOptions.PageIndexParameterName);
						}
						else
						{
							routeValues[this._pagerOptions.PageIndexParameterName] = pageIndex;
						}
					}
				}
				else
				{
					routeValues[this._pagerOptions.PageIndexParameterName] = pageIndex;
				}
			}
			RouteCollection routes = (this._ajax == null) ? this._html.RouteCollection : this._ajax.RouteCollection;
			string url;
			if (!string.IsNullOrEmpty(routeName))
			{
				url = UrlHelper.GenerateUrl(routeName, this._actionName, this._controllerName, routeValues, routes, viewContext.RequestContext, false);
			}
			else
			{
				url = MvcUrlHelper.GenerateUrl(null, this._actionName, this._controllerName, routeValues, routes, viewContext.RequestContext, false, this._pagerOptions.ArrayParameter);
			}
			if (pageValue != null)
			{
				viewContext.RouteData.Values[this._pagerOptions.PageIndexParameterName] = pageValue;
			}
			return url;
		}

        /// <summary>
        /// The render pager.
        /// </summary>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        internal MvcHtmlString RenderPager()
        {
            if (this._totalPageCount <= 1 && this._pagerOptions.AutoHide)
			{
				return MvcHtmlString.Create("\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ © 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n");
			}
			if ((this._pageIndex > this._totalPageCount && this._totalPageCount > 0) || this._pageIndex < 1)
			{
				return MvcHtmlString.Create(string.Format("{0}<div style=\"color:red;font-weight:bold\">{1}</div>{0}", "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ © 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n", this._pagerOptions.PageIndexOutOfRangeErrorMessage));
			}
			List<PagerItem> pagerItems = new List<PagerItem>();
			if (this._pagerOptions.ShowFirstLast)
			{
				this.AddFirst(pagerItems);
			}
			if (this._pagerOptions.ShowPrevNext)
			{
				this.AddPrevious(pagerItems);
			}
			if (this._pagerOptions.ShowNumericPagerItems)
			{
				if (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._startPageIndex > 1)
				{
					pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
				}
				if (this._pagerOptions.ShowMorePagerItems && ((!this._pagerOptions.AlwaysShowFirstLastPageNumber && this._startPageIndex > 1) || (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._startPageIndex > 2)))
				{
					this.AddMoreBefore(pagerItems);
				}
				this.AddPageNumbers(pagerItems);
				if (this._pagerOptions.ShowMorePagerItems && ((!this._pagerOptions.AlwaysShowFirstLastPageNumber && this._endPageIndex < this._totalPageCount) || (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._totalPageCount > this._endPageIndex + 1)))
				{
					this.AddMoreAfter(pagerItems);
				}
				if (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._endPageIndex < this._totalPageCount)
				{
					pagerItems.Add(new PagerItem(this._totalPageCount.ToString(CultureInfo.InvariantCulture), this._totalPageCount, false, PagerItemType.NumericPage));
				}
			}
			if (this._pagerOptions.ShowPrevNext)
			{
				this.AddNext(pagerItems);
			}
			if (this._pagerOptions.ShowFirstLast)
			{
				this.AddLast(pagerItems);
			}
			StringBuilder sb = new StringBuilder();
			if (this._ajaxPagingEnabled)
			{
				using (List<PagerItem>.Enumerator enumerator = pagerItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PagerItem item = enumerator.Current;
						sb.Append(this.GenerateAjaxPagerElement(item));
					}
					goto IL_245;
				}
			}
			foreach (PagerItem item2 in pagerItems)
			{
				sb.Append(this.GeneratePagerElement(item2));
			}
			IL_245:
			TagBuilder tb = new TagBuilder(this._pagerOptions.ContainerTagName);
			if (!string.IsNullOrEmpty(this._pagerOptions.Id))
			{
				tb.GenerateId(this._pagerOptions.Id);
			}
			if (!string.IsNullOrEmpty(this._pagerOptions.CssClass))
			{
				tb.AddCssClass(this._pagerOptions.CssClass);
			}
			if (!string.IsNullOrEmpty(this._pagerOptions.HorizontalAlign))
			{
				string strAlign = "text-align:" + this._pagerOptions.HorizontalAlign.ToLower();
				if (this._htmlAttributes == null)
				{
					this._htmlAttributes = new RouteValueDictionary
					{

						{
							"style",
							strAlign
						}
					};
				}
				else
				{
					if (this._htmlAttributes.Keys.Contains("style"))
					{
						IDictionary<string, object> htmlAttributes;
						(htmlAttributes = this._htmlAttributes)["style"] = htmlAttributes["style"] + ";" + strAlign;
					}
				}
			}
			tb.MergeAttributes<string, object>(this._htmlAttributes, true);
			if (this._ajaxPagingEnabled)
			{
				IDictionary<string, object> attrs = this._ajaxOptions.ToUnobtrusiveHtmlAttributes();
				attrs.Remove("data-ajax-url");
				attrs.Remove("data-ajax-mode");
				if (this._ajaxOptions.EnablePartialLoading)
				{
					attrs.Add("data-ajax-partialloading", "true");
				}
				if (this._pageIndex > 1)
				{
					attrs.Add("data-ajax-currentpage", this._pageIndex);
				}
				if (!string.IsNullOrWhiteSpace(this._ajaxOptions.DataFormId))
				{
					attrs.Add("data-ajax-dataformid", "#" + this._ajaxOptions.DataFormId);
				}
				this.AddDataAttributes(attrs);
				tb.MergeAttributes<string, object>(attrs, true);
			}
			if (this._pagerOptions.ShowPageIndexBox)
			{
				if (!this._ajaxPagingEnabled)
				{
					Dictionary<string, object> attrs2 = new Dictionary<string, object>();
					this.AddDataAttributes(attrs2);
					tb.MergeAttributes<string, object>(attrs2, true);
				}
				sb.Append(this.BuildGoToPageSection());
			}
			else
			{
				sb.Length -= this._pagerOptions.PagerItemsSeperator.Length;
			}
			tb.InnerHtml = sb.ToString();
			return MvcHtmlString.Create("\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ © 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n" + tb.ToString(TagRenderMode.Normal) + "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ © 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n");
		}

        /// <summary>
        /// The add data attributes.
        /// </summary>
        /// <param name="attrs">
        /// The attrs.
        /// </param>
        private void AddDataAttributes(IDictionary<string, object> attrs)
		{
			attrs.Add("data-urlformat", this.GenerateUrl(0));
			attrs.Add("data-mvcpager", "true");
			if (this._pageIndex > 1)
			{
				attrs.Add("data-firstpage", this.GenerateUrl(1));
			}
			attrs.Add("data-pageparameter", this._pagerOptions.PageIndexParameterName);
			attrs.Add("data-maxpages", this._totalPageCount);
			if (this._pagerOptions.ShowPageIndexBox && this._pagerOptions.PageIndexBoxType == PageIndexBoxType.TextBox)
			{
				attrs.Add("data-outrangeerrmsg", this._pagerOptions.PageIndexOutOfRangeErrorMessage);
				attrs.Add("data-invalidpageerrmsg", this._pagerOptions.InvalidPageIndexErrorMessage);
			}
		}

        /// <summary>
        /// The build go to page section.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string BuildGoToPageSection()
		{
			StringBuilder piBuilder = new StringBuilder();
			if (this._pagerOptions.PageIndexBoxType == PageIndexBoxType.DropDownList)
			{
				int startIndex = this._pageIndex - this._pagerOptions.MaximumPageIndexItems / 2;
				if (startIndex + this._pagerOptions.MaximumPageIndexItems > this._totalPageCount)
				{
					startIndex = this._totalPageCount + 1 - this._pagerOptions.MaximumPageIndexItems;
				}
				if (startIndex < 1)
				{
					startIndex = 1;
				}
				int endIndex = startIndex + this._pagerOptions.MaximumPageIndexItems - 1;
				if (endIndex > this._totalPageCount)
				{
					endIndex = this._totalPageCount;
				}
				piBuilder.AppendFormat("<select data-pageindexbox=\"true\"{0}>", this._pagerOptions.ShowGoButton ? "" : " data-autosubmit=\"true\"");
				for (int i = startIndex; i <= endIndex; i++)
				{
					piBuilder.AppendFormat("<option value=\"{0}\"", i);
					if (i == this._pageIndex)
					{
						piBuilder.Append(" selected=\"selected\"");
					}
					piBuilder.AppendFormat(">{0}</option>", i);
				}
				piBuilder.Append("</select>");
			}
			else
			{
				piBuilder.AppendFormat("<input type=\"text\" value=\"{0}\" data-pageindexbox=\"true\"{1}/>", this._pageIndex, this._pagerOptions.ShowGoButton ? "" : " data-autosubmit=\"true\"");
			}
			string outHtml;
			if (!string.IsNullOrEmpty(this._pagerOptions.PageIndexBoxWrapperFormatString))
			{
				outHtml = string.Format(this._pagerOptions.PageIndexBoxWrapperFormatString, piBuilder);
				piBuilder = new StringBuilder(outHtml);
			}
			if (this._pagerOptions.ShowGoButton)
			{
				piBuilder.AppendFormat("<input type=\"button\" data-submitbutton=\"true\" value=\"{0}\"/>", this._pagerOptions.GoButtonText);
			}
			if (!string.IsNullOrEmpty(this._pagerOptions.GoToPageSectionWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
			{
				outHtml = string.Format(this._pagerOptions.GoToPageSectionWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, piBuilder);
			}
			else
			{
				outHtml = piBuilder.ToString();
			}
			return outHtml;
		}

        /// <summary>
        /// The generate ajax anchor.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateAjaxAnchor(PagerItem item)
		{
			string url = this.GenerateUrl(item.PageIndex);
			if (string.IsNullOrWhiteSpace(url))
			{
				return HttpUtility.HtmlEncode(item.Text);
			}
			TagBuilder tag = new TagBuilder("a")
			{
				InnerHtml = item.Text
			};
			tag.MergeAttribute("href", url);
			tag.MergeAttribute("data-pageindex", item.PageIndex.ToString(CultureInfo.InvariantCulture));
			return tag.ToString(TagRenderMode.Normal);
		}

        /// <summary>
        /// The generate pager element.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        private MvcHtmlString GeneratePagerElement(PagerItem item)
		{
			string url = this.GenerateUrl(item.PageIndex);
			if (item.Disabled)
			{
				return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
			}
			return this.CreateWrappedPagerElement(item, string.IsNullOrEmpty(url) ? HttpUtility.HtmlEncode(item.Text) : string.Format("<a href=\"{0}\">{1}</a>", url, item.Text));
		}

        /// <summary>
        /// The generate ajax pager element.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        private MvcHtmlString GenerateAjaxPagerElement(PagerItem item)
		{
			if (item.Disabled)
			{
				return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
			}
			return this.CreateWrappedPagerElement(item, this.GenerateAjaxAnchor(item));
		}

        /// <summary>
        /// The create wrapped pager element.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="el">
        /// The el.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
		{
			string navStr = el;
			switch (item.Type)
			{
			case PagerItemType.FirstPage:
			case PagerItemType.NextPage:
			case PagerItemType.PrevPage:
			case PagerItemType.LastPage:
				if (!string.IsNullOrEmpty(this._pagerOptions.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
				{
					navStr = string.Format(this._pagerOptions.NavigationPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
				}
				break;
			case PagerItemType.MorePage:
				if (!string.IsNullOrEmpty(this._pagerOptions.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
				{
					navStr = string.Format(this._pagerOptions.MorePagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
				}
				break;
			case PagerItemType.NumericPage:
				if (item.PageIndex == this._pageIndex && (!string.IsNullOrEmpty(this._pagerOptions.CurrentPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString)))
				{
					navStr = string.Format(this._pagerOptions.CurrentPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
					{
						navStr = string.Format(this._pagerOptions.NumericPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
					}
				}
				break;
			}
			return MvcHtmlString.Create(navStr + this._pagerOptions.PagerItemsSeperator);
		}

        /// <summary>
        /// The add query string to route values.
        /// </summary>
        /// <param name="routeValues">
        /// The route values.
        /// </param>
        /// <param name="viewContext">
        /// The view context.
        /// </param>
        private void AddQueryStringToRouteValues(RouteValueDictionary routeValues, ViewContext viewContext)
		{
			if (routeValues == null)
			{
				routeValues = new RouteValueDictionary();
			}
			NameValueCollection rq = viewContext.HttpContext.Request.QueryString;
			if (rq != null && rq.Count > 0)
			{
				string[] invalidParams = new string[]
				{
					"x-requested-with",
					"xmlhttprequest",
					this._pagerOptions.PageIndexParameterName.ToLower()
				};
				foreach (string key in rq.Keys)
				{
					if (!string.IsNullOrEmpty(key) && Array.IndexOf<string>(invalidParams, key.ToLower()) < 0)
					{
						string kv = rq[key];
						routeValues[key] = kv;
					}
				}
			}
		}
	}
}
