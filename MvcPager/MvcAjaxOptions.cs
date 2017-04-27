using System.Web.Mvc.Ajax;

namespace MvcPager
{
    /// <summary>
    /// MvcAjaxOptions
    /// </summary>
	public class MvcAjaxOptions : AjaxOptions
	{
        /// <summary>
        /// �Ƿ�ʹ�ֲ����� 
        /// </summary>
		public bool EnablePartialLoading
		{
			get;
			set;
		}

        /// <summary>
        /// ������ʾģ��ID
        /// </summary>
		public string DataFormId
		{
			get;
			set;
		}
	}
}