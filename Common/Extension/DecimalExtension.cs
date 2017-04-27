using System;

namespace Common.Extension
{
    /// <summary>
    /// Decimal扩展
    /// </summary>
    public static class DecimalExtension
    {
        /// <summary>
        /// 获取decimal类型小数点后面位数
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>结果</returns>
        public static int DecimalLeftLength(this decimal value)
        {
            string strValue = Convert.ToString(value);
            int index = strValue.IndexOf('.');
            return index >= 0 ? strValue.Substring(strValue.IndexOf('.') + 1).Length : 0;
        }

        /// <summary>
        /// 元转换为分
        /// </summary>
        /// <param name="price">元</param>
        /// <returns>分</returns>
        public static string ConvertPoints(this decimal price)
        {
            return (price * 100).ToString("0.##");
        }
    }
}