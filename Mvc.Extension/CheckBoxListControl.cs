using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc.Extension
{
    /// <summary>
    /// ckexkboxlist
    /// </summary>
    public static class CheckBoxListControl
    {
        /// <summary>
        /// 枚举转化复选框组数据集
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="selected">选中项</param>
        /// <returns>结果</returns>
        public static IEnumerable<SelectListItem> EnumToListItem(Type type, int selected)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            Array array = Enum.GetValues(type);
            foreach (int item in array)
            {
                string text = Enum.GetName(type, item);
                var selectListItem = new SelectListItem
                {
                    Text = text,
                    Value = item.ToString(CultureInfo.InvariantCulture),
                    Selected = selected != -1 && (item & selected) == item
                };
                list.Add(selectListItem);
            }

            return list;
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="name">name</param>
        /// <param name="selectList">下拉列表集合</param>
        /// <param name="obj">属性</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object obj)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            StringBuilder listItemBuilder = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                TagBuilder input = new TagBuilder("input");
                input.Attributes["type"] = "checkbox";
                input.Attributes["name"] = name;
                if (item.Value != null)
                {
                    input.Attributes["value"] = item.Value;
                }
                if (item.Selected)
                {
                    input.Attributes["checked"] = "checked";
                }

                TagBuilder label = new TagBuilder("label")
                {
                    InnerHtml = input + "&nbsp;" + item.Text
                };
                listItemBuilder.AppendLine(label.ToString(TagRenderMode.Normal));
            }
            TagBuilder tagBuilder = new TagBuilder("div")
            {
                InnerHtml = listItemBuilder.ToString()
            };
            if (obj != null)
            {
                IDictionary<string, object> htmlAttributes = new RouteValueDictionary(obj);
                tagBuilder.MergeAttributes(htmlAttributes);
            }
            tagBuilder.GenerateId(fullName);
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="name">name</param>
        /// <param name="selectList">下拉列表集合</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(htmlHelper, name, selectList, null);
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="selected">选择项目</param>
        /// <param name="obj">其他属性</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, Type type, string name, int selected, object obj)
        {
            IEnumerable<SelectListItem> selectListItem = EnumToListItem(type, selected);
            return CheckBoxList(htmlHelper, name, selectListItem, obj);
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="selected">选择项目</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, Type type, string name, int selected)
        {
            return CheckBoxList(htmlHelper, type, name, selected, null);
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="selected">选择项目</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, Type type, string name, string[] selected)
        {
            int val;
            if (selected == null || selected.Length <= 0)
            {
                val = -1;
            }
            else
            {
                val = selected.Sum(item => Convert.ToInt32(item));
            }

            return CheckBoxList(htmlHelper, type, name, val, null);
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <param name="obj">其他属性</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, Type type, string name, object obj)
        {
            return CheckBoxList(htmlHelper, type, name, -1, obj);
        }

        /// <summary>
        /// 自定义CheckBoxList
        /// </summary>
        /// <param name="htmlHelper">htmlHelper</param>
        /// <param name="type">枚举类型</param>
        /// <param name="name">name属性</param>
        /// <returns>DropDownList</returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, Type type, string name)
        {
            return CheckBoxList(htmlHelper, type, name, -1, null);
        }
    }
}
