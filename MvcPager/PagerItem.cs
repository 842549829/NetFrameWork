namespace MvcPager
{
    /// <summary>
    /// The pager item.
    /// </summary>
    internal class PagerItem
	{
	    /// <summary>
	    /// Gets or sets the text.
	    /// </summary>
	    internal string Text
		{
			get;
			set;
		}

	    /// <summary>
	    /// Gets or sets the page index.
	    /// </summary>
	    internal int PageIndex
		{
			get;
			set;
		}

	    /// <summary>
	    /// Gets or sets a value indicating whether disabled.
	    /// </summary>
	    internal bool Disabled
		{
			get;
			set;
		}

	    /// <summary>
	    /// Gets or sets the type.
	    /// </summary>
	    internal PagerItemType Type
		{
			get;
			set;
		}

	    /// <summary>
	    /// Initializes a new instance of the <see cref="PagerItem"/> class.
	    /// </summary>
	    /// <param name="text">
	    /// The text.
	    /// </param>
	    /// <param name="pageIndex">
	    /// The page index.
	    /// </param>
	    /// <param name="disabled">
	    /// The disabled.
	    /// </param>
	    /// <param name="type">
	    /// The type.
	    /// </param>
	    public PagerItem(string text, int pageIndex, bool disabled, PagerItemType type)
		{
			this.Text = text;
			this.PageIndex = pageIndex;
			this.Disabled = disabled;
			this.Type = type;
		}
	}
}