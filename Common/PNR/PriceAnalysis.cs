namespace Common.PNR
{
    /// <summary>
    /// 价格分析类
    /// </summary>
    public class PriceAnalysis
    {
        /// <summary>
        /// 价格
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static decimal ConvertDecimal(string dec)
        {
            decimal temp;
            if (!decimal.TryParse(dec, out temp))
            {
                temp = 0M;
            }

            return temp;
        }
    }
}