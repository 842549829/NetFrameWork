using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcPager
{
    /// <summary>
    /// The paged list.
    /// </summary>
    /// <typeparam name="T">
    /// T
    /// </typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalItemCount
        {
            get;
            set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount
        {
            get
            {
                return (int)Math.Ceiling(this.TotalItemCount / (double)this.PageSize);
            }
        }

        /// <summary>
        /// 开始记录索引
        /// </summary>
        public int StartRecordIndex
        {
            get
            {
                return ((this.CurrentPageIndex - 1) * this.PageSize) + 1;
            }
        }

        /// <summary>
        /// 结束记录索引
        /// </summary>
        public int EndRecordIndex
        {
            get
            {
                if (this.TotalItemCount <= this.CurrentPageIndex * this.PageSize)
                {
                    return this.TotalItemCount;
                }
                return this.CurrentPageIndex * this.PageSize;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
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
        public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            IList<T> items = (allItems as IList<T>) ?? allItems.ToList();
            this.TotalItemCount = items.Count();
            this.CurrentPageIndex = pageIndex;
            this.AddRange(items.Skip(this.StartRecordIndex - 1).Take(pageSize));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="currentPageItems">
        /// The current page items.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="totalItemCount">
        /// The total item count.
        /// </param>
        public PagedList(IEnumerable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            this.AddRange(currentPageItems);
            this.TotalItemCount = totalItemCount;
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
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
        public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            this.AddRange(allItems.Skip(startIndex).Take(pageSize));
            this.TotalItemCount = allItems.Count();
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="currentPageItems">
        /// The current page items.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="totalItemCount">
        /// The total item count.
        /// </param>
        public PagedList(IQueryable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            this.AddRange(currentPageItems);
            this.TotalItemCount = totalItemCount;
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}
