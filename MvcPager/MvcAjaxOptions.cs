using System.Web.Mvc.Ajax;

namespace MvcPager
{
    /// <summary>
    /// MvcAjaxOptions
    /// </summary>
	public class MvcAjaxOptions : AjaxOptions
	{
        /// <summary>
        /// 是否使局部加载 
        /// </summary>
		public bool EnablePartialLoading
		{
			get;
			set;
		}

        /// <summary>
        /// 数据显示模版ID
        /// </summary>
		public string DataFormId
		{
			get;
			set;
		}
	}
}