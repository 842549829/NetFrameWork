using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc.Extension
{
    /// <summary>
    /// 下拉列表控件
    /// </summary>
    public static class DropDownListControl
    {
        /// <summary>
        /// 枚举转化下拉列表数据集
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="selected">选中项</param>
        /// <param name="isFirst">默认第一项</param>
        /// <returns>结果</returns>
        public static IEnumerable<SelectListItem> EnumToListItem(Type type, string selected, bool isFirst)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isFirst)
            {
                list.Add(new SelectListItem { Text = "全部", Value = string.Empty });
            }
            Array array = Enum.GetValues(type);
            foreach (int item in array)
            {
                string text = Enum.GetName(type, item);
                var selectListItem = new SelectListItem
                {
                    Text = text,
                    Value = item.ToString(CultureInfo.InvariantCulture),
                    Selected = !string.IsNullOrEmpty(selected) && selected == text
                };
                list.Add(selectListItem);
            }

            return list;
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="name">name</param>
        /// <param name="selectList">下拉列表集合</param>
        /// <param name="obj">属性</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object obj)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            StringBuilder listItemBuilder = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(item.Text)
                };

                if (item.Value != null)
                {
                    builder.Attributes["value"] = item.Value;
                }

                if (item.Selected)
                {
                    builder.Attributes["selected"] = "selected";
                }

                listItemBuilder.AppendLine(builder.ToString(TagRenderMode.Normal));
            }

            TagBuilder tagBuilder = new TagBuilder("select")
            {
                InnerHtml = listItemBuilder.ToString()
            };

            if (obj != null)
            {
                IDictionary<string, object> htmlAttributes = new RouteValueDictionary(obj);
                tagBuilder.MergeAttributes(htmlAttributes);
            }
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="name">name</param>
        /// <param name="selectList">下拉列表集合</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return DropDownList(htmlHelper, name, selectList, null);
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="selected">选择项目</param>
        /// <param name="obj">其他属性</param>
        /// <param name="isFirst">第一项</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, Type type, string name, string selected, object obj, bool isFirst = true)
        {
            IEnumerable<SelectListItem> selectListItem = EnumToListItem(type, selected, isFirst);
            return DropDownList(htmlHelper, name, selectListItem, obj);
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="selected">选择项目</param>
        /// <param name="isFirst">第一项</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, Type type, string name, string selected, bool isFirst = true)
        {
            return DropDownList(htmlHelper, type, name, selected, null, isFirst);
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="obj">其他属性</param>
        /// <param name="isFirst">第一项</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, Type type, string name, object obj, bool isFirst = true)
        {
            return DropDownList(htmlHelper, type, name, null, obj, isFirst);
        }

        /// <summary>
        /// 自定义DropDownList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="isFirst">第一项</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, Type type, string name, bool isFirst = true)
        {
            return DropDownList(htmlHelper, type, name, null, null, isFirst);
        }
    }
}
