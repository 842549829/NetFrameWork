using System.Collections.Generic;

namespace Common.PNR
{
    /// <summary>
    /// 航站楼信息格式化
    /// </summary>
    public class FormatTerminalBuilding
    {
        /// <summary>
        /// 特殊航空公司列表
        /// </summary>
        private static List<string> specialAirCodeList = new List<string>();
        // private static List<string> specialAirCodeList = new List<string> { "SZX" };

        /// <summary>
        /// 返回航站楼起始、到达信息
        /// </summary>
        /// <param name="startAirCode">起始城市三字码</param>
        /// <param name="endAirCode">到达城市三字码</param>
        /// <param name="strBuilding">航站楼字符串</param>
        /// <param name="childSeatClass">子舱位信息</param>
        /// <returns>返回List,0:起始航站楼，1：到达航站楼</returns>
        public static List<string> GetTerminalBuiding(string startAirCode, string endAirCode, string strBuilding, string childSeatClass)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(childSeatClass))
            {
                strBuilding = strBuilding.Substring(0, strBuilding.LastIndexOf(childSeatClass));
            }

            // 空格替换
            strBuilding = strBuilding.Replace(" ", string.Empty);
            int length = strBuilding.Length;
            if (length > 4 || length < 2)
            {
                return list;
            }

            string startBuilding = string.Empty; // 起始航站楼
            string endBuilding = string.Empty; // 到达航站楼
            if (length == 4)
            {
                // 长度为4，判断只要有深圳，则置为空
                if ((specialAirCodeList.Contains(startAirCode.ToUpper()) || specialAirCodeList.Contains(endAirCode.ToUpper())) && !strBuilding.Contains("--"))
                {
                    startBuilding = string.Empty;
                    endBuilding = string.Empty;
                }
                else
                {
                    startBuilding = strBuilding.Substring(0, 2);
                    endBuilding = strBuilding.Substring(2, 2);
                }
            }
            else if (length == 3)
            {
                if (specialAirCodeList.Contains(startAirCode.ToUpper()))
                {
                    // 航司单独处理
                    startBuilding = strBuilding.Substring(0, 1);
                    endBuilding = strBuilding.Substring(1, 2);
                }
                else if (specialAirCodeList.Contains(endAirCode.ToUpper()))
                {
                    // 航司单独处理
                    startBuilding = strBuilding.Substring(0, 2);
                    endBuilding = strBuilding.Substring(length - 1, 1);
                }
                else
                {
                    if (startAirCode == "CKG")
                    {
                        // 航司单独处理  如DT1
                        startBuilding = strBuilding.Substring(0, 1);
                        endBuilding = strBuilding.Substring(1, 2);
                    }
                    else if (endAirCode == "CKG")
                    {
                        // 航司单独处理  如T1D
                        startBuilding = strBuilding.Substring(0, 2);
                        endBuilding = strBuilding.Substring(length - 1, 1);
                    }
                }
            }
            else if (length == 2)
            {
                if (specialAirCodeList.Contains(startAirCode.ToUpper()))
                {
                    // 航司单独处理
                    startBuilding = strBuilding.Substring(0, 1);
                    endBuilding = strBuilding.Substring(1, 1);
                }
                else if (specialAirCodeList.Contains(endAirCode.ToUpper()))
                {
                    // 航司单独处理
                    startBuilding = strBuilding.Substring(0, 1);
                    endBuilding = strBuilding.Substring(length - 1, 1);
                }
            }

            startBuilding = startBuilding.Contains("-") ? string.Empty : startBuilding;
            endBuilding = endBuilding.Contains("-") ? string.Empty : endBuilding;
            if (startAirCode == "CKG")
            {
                startBuilding = string.Empty;
            }

            if (endAirCode == "CKG")
            {
                endBuilding = string.Empty;
            }

            list.Add(startBuilding);
            list.Add(endBuilding);
            return list;
        }
    }
}
