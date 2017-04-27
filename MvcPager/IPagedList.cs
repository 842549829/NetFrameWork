using System.Collections;

namespace MvcPager
{
    /// <summary>
    /// The PagedList interface.
    /// </summary>
    public interface IPagedList : IEnumerable
	{
        /// <summary>
        /// 当前页
        /// </summary>
		int CurrentPageIndex
		{
			get;
			set;
		}

        /// <summary>
        /// 页大小
        /// </summary>
		int PageSize
		{
			get;
			set;
		}

        /// <summary>
        /// 总条数
        /// </summary>
		int TotalItemCount
		{
			get;
			set;
		}
	}
}