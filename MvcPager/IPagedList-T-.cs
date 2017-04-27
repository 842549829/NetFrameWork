using System.Collections.Generic;

namespace MvcPager
{
    /// <summary>
    /// IPagedList
    /// </summary>
    /// <typeparam name="T">T</typeparam>
	public interface IPagedList<out T> : IEnumerable<T>, IPagedList
	{
	}
}