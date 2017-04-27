using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MvcPager
{
    /// <summary>
    /// The display name for extension.
    /// </summary>
    public static class DisplayNameForExtension
	{
        /// <summary>
        /// The display name for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IPagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
		{
			return DisplayNameForExtension.GetDisplayName<TModel, TValue>(expression);
		}

        /// <summary>
        /// The display name for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<PagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
		{
			return DisplayNameForExtension.GetDisplayName<TModel, TValue>(expression);
		}

        /// <summary>
        /// The get display name.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        private static MvcHtmlString GetDisplayName<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, new ViewDataDictionary<TModel>());
			string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
			string arg_3F_0;
			if ((arg_3F_0 = metadata.DisplayName) == null && (arg_3F_0 = metadata.PropertyName) == null)
			{
				arg_3F_0 = htmlFieldName.Split(new char[]
				{
					'.'
				}).Last<string>();
			}
			string resolvedDisplayName = arg_3F_0;
			return new MvcHtmlString(HttpUtility.HtmlEncode(resolvedDisplayName));
		}
	}
}
