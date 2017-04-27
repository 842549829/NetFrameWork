using System.Collections;

namespace MvcPager
{
    /// <summary>
    /// The PagedList interface.
    /// </summary>
    public interface IPagedList : IEnumerable
	{
        /// <summary>
        /// ��ǰҳ
        /// </summary>
		int CurrentPageIndex
		{
			get;
			set;
		}

        /// <summary>
        /// ҳ��С
        /// </summary>
		int PageSize
		{
			get;
			set;
		}

        /// <summary>
        /// ������
        /// </summary>
		int TotalItemCount
		{
			get;
			set;
		}
	}
}