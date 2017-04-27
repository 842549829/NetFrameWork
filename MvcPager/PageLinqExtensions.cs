using System.Collections.Generic;
using System.Linq;

namespace MvcPager
{
    /// <summary>
    /// PageLinqExtensions
    /// </summary>
	public static class PageLinqExtensions
	{
        /// <summary>
        /// The to paged list.
        /// </summary>
        /// <param name="allItems">
        /// The all items.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The 
        /// </returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
		{
			if (pageIndex < 1)
			{
				pageIndex = 1;
			}
			int itemIndex = (pageIndex - 1) * pageSize;
			int totalItemCount = allItems.Count();
			while (totalItemCount <= itemIndex && pageIndex > 1)
			{
				itemIndex = (--pageIndex - 1) * pageSize;
			}
			IQueryable<T> pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
			return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
		}

        /// <summary>
        /// The to paged list.
        /// </summary>
        /// <param name="allItems">
        /// The all items.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The 
        /// </returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
		{
			return allItems.AsQueryable().ToPagedList(pageIndex, pageSize);
		}
	}
}