using System;

namespace Common.Extension
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>如果 value 参数为 null 或空字符串 ("")，则为 true；否则为 false。</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns> 如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        public static bool IsEmptySpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 验证指定字符串是否为success
        /// </summary>
        /// <param name="value">指定字符串</param>
        /// <returns><c>true</c>如果 value 参数为 success ，则为 true；否则为 false。</returns>
        public static bool IsSuccess(this string value)
        {
            return value.ToLower() == "success";
        }

        #region 拆分字符串
        /// <summary>
        /// 根据字符串拆分字符串
        /// </summary>
        /// <param name="source">要拆分的字符串</param>
        /// <param name="separator">拆分符</param>
        /// <returns>数组</returns>
        public static string[] Split(this string source, string separator)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (separator == null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            string[] strtmp = new string[1];
            // ReSharper disable once StringIndexOfIsCultureSpecific.2
            int index = source.IndexOf(separator, 0);
            if (index < 0)
            {
                strtmp[0] = source;
                return strtmp;
            }

            strtmp[0] = source.Substring(0, index);
            return Split(source.Substring(index + separator.Length), separator, strtmp);
        }

        /// <summary>
        /// 采用递归将字符串分割成数组
        /// </summary>
        /// <param name="source">要拆分的字符串</param>
        /// <param name="separator">拆分符</param>
        /// <param name="attachArray">attachArray</param>
        /// <returns>string[]</returns>
        private static string[] Split(string source, string separator, string[] attachArray)
        {
            // while循环的方式
            while (true)
            {
                string[] strtmp = new string[attachArray.Length + 1];
                attachArray.CopyTo(strtmp, 0);

                // ReSharper disable once StringIndexOfIsCultureSpecific.2
                int index = source.IndexOf(separator, 0);
                if (index < 0)
                {
                    strtmp[attachArray.Length] = source;
                    return strtmp;
                }

                strtmp[attachArray.Length] = source.Substring(0, index);
                source = source.Substring(index + separator.Length);
                attachArray = strtmp;
            }

            // 递归的方式
            /*
            string[] strtmp = new string[attachArray.Length + 1];
            attachArray.CopyTo(strtmp, 0);

            // ReSharper disable once StringIndexOfIsCultureSpecific.2
            int index = source.IndexOf(separator, 0);
            if (index < 0)
            {
                strtmp[attachArray.Length] = source;
                return strtmp;
            }
            else
            {
                strtmp[attachArray.Length] = source.Substring(0, index);
                return Split(source.Substring(index + separator.Length), separator, strtmp);
            }*/
        }

        #endregion
    }
}