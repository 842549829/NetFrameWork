namespace MvcPager
{
    /// <summary>
    /// The pager item type.
    /// </summary>
    internal enum PagerItemType : byte
	{
	    /// <summary>
	    /// The first page.
	    /// </summary>
	    FirstPage,

	    /// <summary>
	    /// The next page.
	    /// </summary>
	    NextPage,

	    /// <summary>
	    /// The prev page.
	    /// </summary>
	    PrevPage,

	    /// <summary>
	    /// The last page.
	    /// </summary>
	    LastPage,

	    /// <summary>
	    /// The more page.
	    /// </summary>
	    MorePage,

	    /// <summary>
	    /// The numeric page.
	    /// </summary>
	    NumericPage
	}
}