using System;
using System.Collections.Generic;

namespace MvcPager
{
    /// <summary>
    /// PagerOptions
    /// </summary>
    public class PagerOptions
    {
        /// <summary>
        /// 分页控件html容器标签名，默认为div
        /// </summary>
        private string containerTagName;

        /// <summary>
        /// 首页使用的路由名称（无页索引参数）
        /// </summary>
        public string FirstPageRouteName
        {
            get;
            set;
        }

        /// <summary>
        /// 当总页数只有一页时是否自动隐藏
        /// </summary>
        public bool AutoHide
        {
            get;
            set;
        }

        /// <summary>
        /// 页索引超出范围时显示的错误消息
        /// </summary>
        public string PageIndexOutOfRangeErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 页索引无效时显示的错误消息
        /// </summary>
        public string InvalidPageIndexErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// url中页索引参数的名称
        /// </summary>
        public string PageIndexParameterName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示页索引输入出选择框
        /// </summary>
        public bool ShowPageIndexBox
        {
            get;
            set;
        }

        /// <summary>
        /// 页索引输入或选择框的类型
        /// </summary>
        public PageIndexBoxType PageIndexBoxType
        {
            get;
            set;
        }

        /// <summary>
        /// 页索引下拉框中显示的最大页索引条数，该属性仅当PageIndexBoxType设为PageIndexBoxType.DropDownList时有效
        /// </summary>
        public int MaximumPageIndexItems
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示跳转按钮
        /// </summary>
        public bool ShowGoButton
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转按钮上的文本
        /// </summary>
        public string GoButtonText
        {
            get;
            set;
        }

        /// <summary>
        /// 数字页索引格式字符串
        /// </summary>
        public string PageNumberFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页索引格式字符串
        /// </summary>
        public string CurrentPageNumberFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 分页控件html容器标签名，默认为div
        /// </summary>
        public string ContainerTagName
        {
            get
            {
                return this.containerTagName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ContainerTagName不能为null或空字符串", "ContainerTagName");
                }
                this.containerTagName = value;
            }
        }

        /// <summary>
        /// 包容数字页、当前页及上、下、前、后分页元素的html文本格式字符串
        /// </summary>
        public string PagerItemWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容数字页索引分页元素的html文本格式字符串
        /// </summary>
        public string NumericPagerItemWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容当前页分页元素的html文本格式字符串
        /// </summary>
        public string CurrentPagerItemWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容上页、下页、首页和尾首四个分页元素的html文本格式字符串
        /// </summary>
        public string NavigationPagerItemWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容更多页分页元素的html文本格式字符串
        /// </summary>
        public string MorePagerItemWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容页索引输入或选择框的html文本格式字符串
        /// </summary>
        public string PageIndexBoxWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// 包容页索引框及跳转按钮所在区域的html文本框字符串
        /// </summary>
        public string GoToPageSectionWrapperFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// whether or not show first and last numeric page number
        /// </summary>
        public bool AlwaysShowFirstLastPageNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 显示的最大数字页索引按钮数
        /// </summary>
        public int NumericPagerItemCount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示上页和下页
        /// </summary>
        public bool ShowPrevNext
        {
            get;
            set;
        }

        /// <summary>
        /// 上一页文本
        /// </summary>
        public string PrevPageText
        {
            get;
            set;
        }

        /// <summary>
        /// 下一页文本
        /// </summary>
        public string NextPageText
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示数字页索引按钮及更多页按钮
        /// </summary>
        public bool ShowNumericPagerItems
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示第一页和最后一页
        /// </summary>
        public bool ShowFirstLast
        {
            get;
            set;
        }

        /// <summary>
        /// 第一页文本
        /// </summary>
        public string FirstPageText
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一页文本
        /// </summary>
        public string LastPageText
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示更多页按钮
        /// </summary>
        public bool ShowMorePagerItems
        {
            get;
            set;
        }

        /// <summary>
        /// 更多页按钮文本
        /// </summary>
        public string MorePageText
        {
            get;
            set;
        }

        /// <summary>
        /// 包含分页控件的父容器标签的ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 水平对齐方式
        /// </summary>
        public string HorizontalAlign
        {
            get;
            set;
        }

        /// <summary>
        /// CSS样式类
        /// </summary>
        public string CssClass
        {
            get;
            set;
        }

        /// <summary>
        /// whether or not show disabled navigation buttons
        /// </summary>
        public bool ShowDisabledPagerItems
        {
            get;
            set;
        }

        /// <summary>
        /// 分页元素之间的分隔符，默认为两个html空格（  ）
        /// </summary>
        public string PagerItemsSeperator
        {
            get;
            set;
        }

        /// <summary>
        /// 数组参数
        /// </summary>
        public IEnumerable<string> ArrayParameter { get; set; }

        /// <summary>
        /// 限制显示的最大页数，默认值为0，即根据总记录数算出的总页数
        /// </summary>
        public int MaxPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptions"/> class.
        /// </summary>
        public PagerOptions()
        {
            this.AutoHide = true;
            this.PageIndexParameterName = "PageIndex";
            this.NumericPagerItemCount = 5;
            this.AlwaysShowFirstLastPageNumber = false;
            this.ShowPrevNext = true;
            this.PrevPageText = "上一页";
            this.NextPageText = "下一页";
            this.ShowNumericPagerItems = true;
            this.ShowFirstLast = true;
            this.FirstPageText = "首页";
            this.LastPageText = "尾页";
            this.ShowMorePagerItems = true;
            this.MorePageText = "...";
            this.ShowDisabledPagerItems = true;
            this.PagerItemsSeperator = "&nbsp;&nbsp;";
            this.ShowPageIndexBox = false;
            this.ShowGoButton = false;
            this.PageIndexBoxType = PageIndexBoxType.TextBox;
            this.MaximumPageIndexItems = 80;
            this.GoButtonText = "跳转";
            this.ContainerTagName = "div";
            this.InvalidPageIndexErrorMessage = "页索引无效";
            this.PageIndexOutOfRangeErrorMessage = "页索引超出范围";
            this.MaxPageIndex = 0;
            this.FirstPageRouteName = null;
            this.CssClass = "pager";
            this.HorizontalAlign = "right";
        }
    }
}
