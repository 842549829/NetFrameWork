using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.PNR
{
    /// <summary>
    /// pnr内容解析
    /// </summary>
    public class PNRContentAnalysis
    {
        /// <summary>
        /// 编码内容解析
        /// </summary>
        /// <param name="pnrContentStr">Pnr内容</param>
        /// <returns>结果</returns>
        public MPnrContent PnrAnalyse(string pnrContentStr)
        {
            return this.PnrAnalyse(pnrContentStr, false);
        }

        /// <summary>
        /// 编码内容解析
        /// </summary>
        /// <param name="pnrContentStr">Pnr内容</param>
        /// <param name="isIgnoreError">是否忽略解析错误</param>
        /// <returns>结果</returns>
        public MPnrContent PnrAnalyse(string pnrContentStr, bool isIgnoreError)
        {
            MPnrContent pnrModel = new MPnrContent();
            pnrModel.PNRContentStr = pnrContentStr;

            /***********************************编码状态******************************************/
            PNRStatus status = PNRStatus.Normal;
            if (Regex.IsMatch(pnrContentStr, @"NO PNR|NO NAME|FLIGHT NUMBER|PNR不存在"))
            {
                status = PNRStatus.NoPNR;
            }

            ////已取消
            if (Regex.IsMatch(pnrContentStr, @"CANCELLED"))
            {
                status = PNRStatus.Cancelled;
            }

            ////已出票
            if (Regex.IsMatch(pnrContentStr, @"((TN/)|(\*\*ELECTRONIC TICKET PNR\*\*)|(SSR TKNE)|(AND PRINTED)|(ETICKET PNR)|(OSI 1E \w{2}ET TN))"))
            {
                status = PNRStatus.Outed;
            }
            pnrModel.Status = status;

            /***********************************大编码******************************************/
            ////大编码:14.RMK CA/LL8RC
            ////Regex reg = new Regex(@"(?:\d{1,2}\.RMK CA/)(\w{5})", RegexOptions.IgnoreCase);
            ////PNR升位，DLF测试新加,大编码中不知道有无空格，是否也升级到6位，现在是默认支持5到6位，包括空格
            Regex reg = new Regex(@"(?:\d{1,2}\.RMK CA/)(\w{5,6})", RegexOptions.IgnoreCase);
            Match matBigPnr = reg.Match(pnrContentStr);

            if (matBigPnr.Success && matBigPnr.Groups.Count >= 2)
            {
                pnrModel.BigPnr = matBigPnr.Groups[1].Value;
            }

            /***********************************office号******************************************/
            Match matOffice = Regex.Match(pnrContentStr, @"(?:\d{1,2}\.)(?<OFFICE>[A-Z]{3}\d{3})", RegexOptions.RightToLeft);
            if (matOffice.Success)
            {
                pnrModel.OfficeCode = matOffice.Groups["OFFICE"].Value;
            }
            else
            {
                // 20130417 C系统内容如果末尾项没有Office号则再从时间项中取Office
                // eg. 6.TL/2330/08APR/NKG151
                matOffice = Regex.Match(pnrContentStr, @"\d{1,2}\.TL\/\d{4}\/\d{2}[A-Z]{3}\/(?<OFFICE>[A-Z]{3}\d{3})");
                if (matOffice.Success)
                {
                    pnrModel.OfficeCode = matOffice.Groups["OFFICE"].Value;
                }
            }

            /**********************************SSR FOID 项数量*****************************************/
            reg = new Regex(@"\d{1,2}\.(SSR FOID|SSR INFT)");
            MatchCollection matsSSR = reg.Matches(pnrContentStr);
            pnrModel.SSRFOIDCount = matsSSR.Count;

            /***************************是否为团队票,团队票以"0."开头**********************************/
            if (Regex.IsMatch(pnrContentStr, @"^\s*0\."))
            {
                pnrModel.IsTeam = true;
            }

            /***************************解析乘客**********************************/
            pnrModel.Passengers.AddRange(GetAdultPassenger(pnrModel, pnrContentStr, isIgnoreError));
            // 处理婴儿  
            pnrModel.Passengers.AddRange(this.GetBabys(pnrContentStr));

            /***************************解析航段舱位**********************************/
            pnrModel.Seats.AddRange(this.GetSeats(pnrContentStr));

            /***************************解析价格**********************************/
            pnrModel.Pice.AddRange(this.AnalysePrice(pnrContentStr));

            return pnrModel;
        }

        /// <summary>
        /// 获得成人乘客
        /// </summary>
        /// <param name="pnrModel">pnrModel</param>
        /// <param name="pnrContentStr">pnrContentStr</param>
        /// <param name="isIgnoreError">是否忽略解析错误</param>
        /// <returns>乘客集合</returns>
        public List<MPnrMPassenger> GetAdultPassenger(MPnrContent pnrModel, string pnrContentStr, bool isIgnoreError)
        {
            string pnrCode;

            List<MPnrMPassenger> passengers = new List<MPnrMPassenger>();

            ////是否为团队票 PNR升位，DLF测试新加
            Match isTeam = Regex.Match(pnrContentStr, @"0\.(?<YDRS>\d{1,3}).{1,20}\sNM(?<SJRS>\d{1,3}) +(?<PNR>(\w{5,6}))");

            Match mat;
            if (isTeam.Success)
            {
                ////团队编码
                Regex reg = new Regex(@"(?<NAME>(?:1\.)(?:.|\n)+?)\s+\d{1,2}\. (?:\*| |[A-Z])", RegexOptions.IgnoreCase);
                mat = reg.Match(pnrContentStr);
                ////PNR
                pnrCode = isTeam.Groups["PNR"].Value;
                if (pnrModel != null)
                {
                    pnrModel.IsTeam = true;
                }
            }
            else
            {
                //// 散客编码PNR升位，DLF测试新加 2011-11-8 内容导入用户会把RT(PNR)这一行输入 导致编码中带有1的会被当做姓名内容匹配到
                Regex reg = new Regex(@"(?<NAME>(?:1?\.|0\.)(?:.|\n)+?) (?<PNR>(\w{5,6}))(?:/[A-Z0-9]{2})?\s+\d{1,2}\. ", RegexOptions.IgnoreCase);
                mat = reg.Match(pnrContentStr);
                if (!mat.Success)
                {
                    //// 解决pnr与名字之间没有空格的而导致不能匹配的问题
                    reg = new Regex(@"(?<NAME>(?:1?\.|0\.)(?:.|\n)+?)(?:| )(?<PNR>(\w{1,6}))(?:/[A-Z0-9]{2})?\s+\d{1,2}\. ", RegexOptions.IgnoreCase);
                    mat = reg.Match(pnrContentStr);
                }

                pnrCode = mat.Groups["PNR"].Value.Replace(" ", string.Empty);

                if (Regex.IsMatch(pnrCode, "[^A-Za-z0-9]"))
                {
                    System.Exception ex = new System.Exception("从PNR内容中获取乘客信息失败。");
                    throw ex;
                }

                ////编码换行特殊处理
                if (pnrCode.Trim().Length < 6)
                {
                    string nameStr = Regex.Replace(mat.Value.Trim(), "\n|\r", string.Empty);
                    mat = reg.Match(nameStr + " ");////补空格
                    pnrCode = mat.Groups["PNR"].Value.Replace(" ", string.Empty);
                }
            }

            if (pnrModel != null)
            {
                pnrModel.Pnr = pnrCode;
            }
            if (!mat.Success && isIgnoreError == false)
            {
                System.Exception ex = new System.Exception("从PNR内容中获取乘客信息失败。");
                throw ex;
            }

            ////乘客姓名
            string passengerNames = Regex.Replace(mat.Groups["NAME"].Value, "\n|\r| \n| \r|\t", string.Empty).Trim();

            passengerNames = passengerNames + " ";
            ////如果名字后面没有空格，则直接加入空格。
            passengerNames = Regex.Replace(
                passengerNames,
                @"([A-Z|\u4e00-\u9fa5])(\d)",
                delegate(Match match)
                {
                    return match.Value.Insert(1, " ");
                });
            MatchCollection matches = Regex.Matches(
                passengerNames,
                @"(?:(?<NUM>\d{1,2})\.?)?(?<NAME>(?:[A-Z]{1,35}/([A-Z]| ){1,35})(?= |\d)|(?:(?:[\u2E80-\u9FFF]|[A-Z]){1,50}) )",
                RegexOptions.IgnoreCase);

            ////乘客编号
            int i = 1;
            ////最后一个乘机人NUM
            int lastIndex = 0;
            ////乘客类型
            PassengerType passengerType;
            ////证件类型
            string credentialsType;
            ////结算号
            string settle;
            ////乘客MODEL
            MPnrMPassenger passengerModel;

            ////是否有手机号
            //Regex regex = new Regex(@"\d{1,2}\.OSI (?<AIRCODE>\w{2}).+(?<PHONE>\d{11})");
            Regex regex = new Regex(@"\d{1,2}\.OSI (?<AIRCODE>\w{2}) CT[^\d]*(?<PHONE>\d{0,})(?:|/P\d{1,2})\s+");

            MatchCollection mats = regex.Matches(pnrContentStr);
            string mobile = string.Empty;
            if (mats.Count > 0)
            {
                foreach (Match m in mats)
                {
                    ////0开头被认为是座机，仍需补手机号
                    if (!m.Groups["PHONE"].Value.StartsWith("0") && m.Groups["PHONE"].Value.Trim().Length == 11)
                    {
                        mobile = m.Groups["PHONE"].Value;
                    }
                    else if (m.Groups["PHONE"].Value.Trim().Length == 0)
                    {
                        mobile = "13800000000"; //随便给一个号，客户端就会当做有手机号了 (配置有时提不出手机号)
                    }

                    ////获取到了正确的手机号
                    if (mobile.Length > 0 && !mobile.StartsWith("0"))
                    {
                        break;
                    }
                }
            }
            else
            {
                ////2011-10-17 只要有OSI CT就算补过手机号了
                regex = new Regex(@"\d{1,2}\.OSI CT");
                Match mm = regex.Match(pnrContentStr);
                if (mm.Success)
                {
                    mobile = "13800000000"; //随便给一个号，客户端就会当做有手机号了
                }
            }

            foreach (Match match in matches)
            {
                if (match.Groups["NAME"].Value.Trim().Length == 0)
                {
                    continue;
                }

                passengerModel = new MPnrMPassenger();
                passengerModel.SequenceNO = i;
                ////乘客名
                passengerModel.Name = this.GetName(match.Groups["NAME"].Value.Trim(), out passengerType);
                passengerModel.Type = passengerType;
                ////证件
                passengerModel.CredID = this.GetCredentialsCode(pnrContentStr, passengerModel.SequenceNO, out credentialsType);
                //passengerModel.CardID = credentialsType;
                ////票号
                passengerModel.TicketCode = this.GetTeckitCode(pnrContentStr, passengerModel.SequenceNO, out settle, PassengerType.成人);
                passengerModel.SettleCode = settle;
                ////手机号
                passengerModel.MobileNo = mobile;
                ////添加乘客
                passengers.Add(passengerModel);
                i++;
                int.TryParse(match.Groups["NUM"].Value, out lastIndex);
            }

            if (lastIndex != 0 && lastIndex != passengers.Count)
            {
                System.Exception ex = new System.Exception("乘机人数解析异常,解析到的人数和实际人数不一致");
                throw ex;
            }

            if (passengers.Count == 0 && isIgnoreError == false)
            {
                System.Exception ex = new System.Exception("没有解析到任何乘机人");
                throw ex;
            }

            return passengers;
        }

        /// <summary>
        /// 获得乘机人名字和类型
        /// </summary>
        /// <param name="initName">原始串如:王小丽CHD</param>
        /// <param name="type">乘机人类型</param>
        /// <returns>乘机人名</returns>
        protected string GetName(string initName, out PassengerType type)
        {
            ////初始值:成人           
            type = PassengerType.成人;

            if (Regex.IsMatch(initName, @"^[a-zA-Z][\u2E80-\u9FFF]+"))
            {
                System.Exception ex = new System.Exception("PNR内容中乘客信息异常。");
                throw ex;
            }

            ////如果没有CHD,则认为是成人,直接返回
            if (!Regex.IsMatch(initName, @"(CHD|\))$"))
            {
                return initName;
            }

            ////如果为 WANG/HUIXIONGCHD  则认为是成人
            ////if (Regex.IsMatch(initName, @"[A-Z|a-z]CHD$"))
            ////    return initName;
            ////解析出人名
            Regex reg = new Regex(@"(?<NAME>.+?)(?:CHD|\()");
            Match mat = reg.Match(initName);
            if (mat.Success)
            {
                type = PassengerType.儿童;
                return mat.Groups["NAME"].Value.Trim();
            }

            return initName;
        }

        /// <summary>
        /// 获得证件号
        /// </summary>
        /// <param name="pnrContentStr">pnrContentStr</param>
        /// <param name="index">乘客号</param>
        /// <param name="credentialsType">证件类型</param>
        /// <returns>证件号</returns>
        protected string GetCredentialsCode(string pnrContentStr, int index, out string credentialsType)
        {
            ////匹配证件号及证件类型
            Regex reg = new Regex(@"\d{1,2}\.SSR FOID \w{2}\s+[A-Z]{2}\d*\s+(?<TYPE>[A-Z]{2})(?<NUM>(?:\w|-|\(|\))+)/P" + index.ToString());
            Match mat = reg.Match(pnrContentStr);
            ////证件号
            string credentialsCode = string.Empty;
            credentialsType = string.Empty;

            if (!mat.Success)
            {
                return credentialsCode;
            }

            ////证件号
            credentialsCode = mat.Groups["NUM"].Value;
            ////证件类型
            credentialsType = mat.Groups["TYPE"].Value;

            return credentialsCode;
        }

        /// <summary>
        /// 机票号
        /// </summary>
        /// <param name="pnrContentStr">pnr内容</param>
        /// <param name="index">乘客序号</param>
        /// <param name="settle">结算码</param>
        /// <param name="type">乘客类型</param>
        /// <returns>票号</returns>
        protected string GetTeckitCode(string pnrContentStr, int index, out string settle, PassengerType type)
        {
            ////23.TN/999-6970423706/P2
            ////10.SSR TKNE CZ HK1 SYXPEK 6715 Y24OCT 7842424958462/1/P2
            settle = string.Empty;

            ////B系统的票号
            ////婴儿票号
            ////8.SSR TKNE 3U HK1 LXACTU 8902 L24OCT INF8763155770390/1/P1 
            string babyTag = string.Empty;
            if (type == PassengerType.婴儿)
            {
                babyTag = "INF";
            }

            Match mat = Regex.Match(pnrContentStr, @"\d{1,3}\.SSR TKNE.+ " + babyTag + @"(?<SETTLE>\d{3})(?<TICKET>\d{10})/1/P" + index.ToString() + "[^0-9]");
            if (mat.Success)
            {
                settle = mat.Groups["SETTLE"].Value;
                return mat.Groups["TICKET"].Value;
            }
            else
            {
                ////C系统
                if (type == PassengerType.婴儿)
                {
                    babyTag = @"IN/";
                }

                mat = Regex.Match(pnrContentStr, @"TN/" + babyTag + @"(?<SETTLE>\d{3})-(?<TICKET>\d{10})/P" + index.ToString());
                if (mat.Success)
                {
                    settle = mat.Groups["SETTLE"].Value;
                    return mat.Groups["TICKET"].Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 解析婴儿
        /// </summary>
        /// <param name="pnrContentStr">pnr内容</param>
        /// <returns>婴儿集合</returns>
        protected List<MPnrMPassenger> GetBabys(string pnrContentStr)
        {
            List<MPnrMPassenger> babyPassengers = new List<MPnrMPassenger>();

            MatchCollection mats = Regex.Matches(pnrContentStr, @"\d{1,2}\.(OVF\/)?XN/IN/(?<NAME>.+)\s?INF\s?\([A-Z]{3}\d{2}\)/P(?<INDEX>\d{1,3})");
            foreach (Match mat in mats)
            {
                var passenger = new MPnrMPassenger();
                ////索引
                int indexNum;
                if (int.TryParse(mat.Groups["INDEX"].Value, out indexNum))
                {
                    passenger.SequenceNO = indexNum;
                }

                passenger.Name = mat.Groups["NAME"].Value;
                passenger.Type = PassengerType.婴儿;
                ////证件号
                passenger.CredID = this.GetBabyCredentialsCode(pnrContentStr, passenger.SequenceNO);
                //passenger.CardType = string.Empty;

                ////票号
                string settle;
                passenger.TicketCode = this.GetTeckitCode(pnrContentStr, passenger.SequenceNO, out settle, PassengerType.婴儿);
                passenger.SettleCode = settle;
                babyPassengers.Add(passenger);
            }

            return babyPassengers;
        }

        /// <summary>
        /// 婴儿证件号
        /// </summary>
        /// <param name="pnrContentStr">pnrContentStr</param>
        /// <param name="SequenceNO">索引号</param>
        /// <returns>证件号</returns>
        protected string GetBabyCredentialsCode(string pnrContentStr, int SequenceNO)
        {
            string credentialsCode = string.Empty;
            Match mat = Regex.Match(pnrContentStr, @"\d{1,2}\.SSR INFT.+(?<DATE>\d{2}[A-Z]{3}\d{2})/P" + SequenceNO.ToString());
            if (mat.Success)
            {
                credentialsCode = DateTime.Parse(mat.Groups["DATE"].Value, DateTimeFormatInfo.InvariantInfo).ToString("yyyy-MM-dd");
            }
            return credentialsCode;
        }

        /// <summary>
        /// 解析航程舱位
        /// </summary>
        /// <returns>航程集合</returns>
        public List<MPnrSeat> GetSeats(string pnrContentStr)
        {
            ////航程: 2.  3U8881 G   TH25SEP  CTUPEK HK1   0720 0940          E --T3
            List<MPnrSeat> seats = new List<MPnrSeat>();

            ////解决联程换行匹配不到的问题
            Regex reg = new Regex(@"\d{1,2}\. (\r\n| |)+\*?\w{2}\d{2,5}(?:\n|.)+?(?=\d{1,2}\.)");
            MatchCollection mats = reg.Matches(pnrContentStr);
            if (mats.Count == 0)
            {
                //// 解决航班字母与数字之间有空格的问题
                reg = new Regex(@"\d{1,2}\. (\r\n| |)+\*?\w{2}\s*\d{2,5}(?:\n|.)+?(?=\d{1,2}\.)");
                mats = reg.Matches(pnrContentStr);
            }

            MPnrSeat seat;

            ////匹配航程信息
            string voyagePattern = @"(?<HB>\*?\w{2}\s*[A-Z0-9]{2,6}).*(?<CW>[A-Z])\d?\W+\w{2}(?<DAY>\d{2})(?<MONTH>[A-Z]{3})(?:\W*|\d{2})(?<FCITY>[A-Z]{3})(?<TCITY>[A-Z]{3})\W*(?<STATUS>[A-Z]{2})\W*\d*\W*(?<FTIME>\d{4})\W*(?<TTIME>\d{4})";
            voyagePattern += @"\s*[\+|\-]?\s*\d?\W*E?\s*(?<BUILD>[A-Z0-9\-\s*]*)"; // 航站楼解决

            if (mats.Count == 0)
            {
                System.Exception ex = new System.Exception("从PNR内容中获取航程信息失败。");
                throw ex;
            }

            foreach (Match mat in mats)
            {
                seat = new MPnrSeat();
                reg = new Regex(voyagePattern);

                string voyageInfo = Regex.Replace(mat.Value, @"\n|\r", string.Empty).Trim();
                Match matVoyage = reg.Match(voyageInfo);

                if (!matVoyage.Success)
                {
                    System.Exception ex = new System.Exception("分析航程信息出错。");
                    throw ex;
                }

                ////航班 删除航班中间空格
                this.AnalyseScheduledFlight(voyageInfo, matVoyage.Groups["HB"].Value.Replace(" ", string.Empty), ref seat);
                ////舱位
                seat.SeatCode = matVoyage.Groups["CW"].Value;

                // 处理子舱位
                string childSeatClass = string.Empty;

                Match matChild = Regex.Match(voyageInfo, string.Format(@" (?<ChildSeat>{0}\d)(?: |$)", seat.SeatCode));
                if (matChild.Success)
                {
                    seat.SeatCode = matChild.Groups["ChildSeat"].Value;
                    childSeatClass = seat.SeatCode; // 子舱位
                }

                ////出发时间及到达时间
                this.ComposeTime(matVoyage.Groups["MONTH"].Value, matVoyage.Groups["DAY"].Value, matVoyage.Groups["FTIME"].Value, matVoyage.Groups["TTIME"].Value, ref seat);
                ////状态
                try
                {
                    seat.Status = (SeatStatus)Enum.Parse(typeof(SeatStatus), matVoyage.Groups["STATUS"].Value, true);
                }
                catch
                {
                    seat.Status = SeatStatus.OTHER;
                }
                ////出发城市
                seat.FromCity = matVoyage.Groups["FCITY"].Value;
                ////到达城市
                seat.ToCity = matVoyage.Groups["TCITY"].Value;

                try
                {
                    // 航站楼起始到达
                    string strBuilding = matVoyage.Groups["BUILD"].Value;
                    List<string> buildList = FormatTerminalBuilding.GetTerminalBuiding(seat.FromCity, seat.ToCity, strBuilding, childSeatClass);
                    if (buildList.Count > 0)
                    {
                        seat.DepartureTerm = buildList[0]; // 出发航站楼
                        seat.ArrivalTerm = buildList[1]; // 到达航站楼
                    }
                }
                catch
                {
                    // ignored
                }

                seats.Add(seat);
            }
            ////行程类型
            ////SetTravel();

            return seats;
        }

        /// <summary>
        /// 分析航班
        /// </summary>
        /// <param name="voyage">整个航程信息</param>
        /// <param name="scheduledFlight">航班号如:*MU8966 或者 8C8290</param>
        /// <param name="seat">航程MODEL</param>
        protected void AnalyseScheduledFlight(string voyage, string scheduledFlight, ref MPnrSeat seat)
        {
            ////将承运人和航班拆分
            Regex reg = new Regex(@"\*?(?<CYR>\w{2})(?<HBH>[A-Z0-9]{2,6})");
            Match mat = reg.Match(voyage);

            if (!mat.Success)
            {
                return;
            }

            ////航班号
            seat.FlightNO = mat.Groups["CYR"].Value + mat.Groups["HBH"].Value;

            ////如果有 "*"则认为是共享航班
            if (Regex.IsMatch(scheduledFlight, @"\*"))
            {
                seat.IsShareFlight = true;
                ////匹配共享航班
                reg = new Regex(@"(?<CYR>\w{2})(?<HBH>[A-Z0-9]{2,6})$");
                mat = reg.Match(voyage);
                if (mat.Success)
                {
                    seat.ShareFlightNO = mat.Groups["CYR"].Value + mat.Groups["HBH"].Value;
                }
            }
        }

        /// <summary>
        /// 把解析到的时间组合成DATETIME,出发及到达时间
        /// </summary>
        /// <param name="month">月SEP</param>
        /// <param name="day">日</param>
        /// <param name="departureTime">出发时间</param>
        /// <param name="arriveTime">到达时间</param>
        /// <param name="seat">航程对象</param>
        protected void ComposeTime(string month, string day, string departureTime, string arriveTime, ref MPnrSeat seat)
        {
            ////以数字表示的月份
            string monthNum = "01";

            ////翻译月份
            switch (month)
            {
                case "JAN":
                    monthNum = "01";
                    break;
                case "FEB":
                    monthNum = "02";
                    break;
                case "MAR":
                    monthNum = "03";
                    break;
                case "APR":
                    monthNum = "04";
                    break;
                case "MAY":
                    monthNum = "05";
                    break;
                case "JUN":
                    monthNum = "06";
                    break;
                case "JUL":
                    monthNum = "07";
                    break;
                case "AUG":
                    monthNum = "08";
                    break;
                case "SEP":
                    monthNum = "09";
                    break;
                case "OCT":
                    monthNum = "10";
                    break;
                case "NOV":
                    monthNum = "11";
                    break;
                case "DEC":
                    monthNum = "12";
                    break;
            }

            ////当前年
            DateTime now = DateTime.Now;
            int year = now.Year;

            ////如果预订的月份小于当前月份则为跨年
            if (now.Month > int.Parse(monthNum))
            {
                year++;
            }

            ////组合时间字符串
            StringBuilder dateTime = new StringBuilder();

            dateTime.Append(year.ToString());
            dateTime.Append("-");
            dateTime.Append(monthNum);
            dateTime.Append("-");
            dateTime.Append(day);
            dateTime.Append(" ");

            string date = dateTime.ToString();

            dateTime.Append(departureTime.Insert(2, ":"));

            ////组合后的时间
            DateTime composeTime;

            ////出发时间
            if (!DateTime.TryParse(dateTime.ToString(), out composeTime))
            {
                composeTime = now;
            }

            seat.DepartureTime = composeTime;

            dateTime = new StringBuilder(date);
            dateTime.Append(arriveTime.Insert(2, ":"));

            ////到达时间
            if (!DateTime.TryParse(dateTime.ToString(), out composeTime))
            {
                composeTime = now;
            }

            seat.ArrivalTime = composeTime;

            ////处理跨天
            if (seat.ArrivalTime < seat.DepartureTime)
            {
                seat.ArrivalTime = seat.ArrivalTime.AddDays(1);
            }
        }

        /// <summary>
        /// 通过prn内容解析价格
        /// </summary>
        public List<MPnrPrice> AnalysePrice(string pnrContentStr)
        {
            List<MPnrPrice> lstPrice = new List<MPnrPrice>();

            Regex reg = new Regex(@"\d{2}.(?<SEAT>(?:\S*))\W+FARE:CNY(?<FARE>(?:\d|\.){1,10})\W+TAX:(?:(?:CNY(?<TAX>(?:\d|\.){1,10}))|[A-Z]+)\W+YQ:(?:(?:CNY(?<YQ>(?:\d|\.){1,10}))|[A-Z]+)\W+TOTAL:(?<TOTAL>(?:\d|\.){1,10})");

            MatchCollection mats = reg.Matches(pnrContentStr);
            foreach (Match mat in mats)
            {
                //throw new CommandAnalysisException("分析价格出错");
                MPnrPrice price = new MPnrPrice();
                price.Fare = PriceAnalysis.ConvertDecimal(mat.Groups["FARE"].Value);
                price.FuelFee = PriceAnalysis.ConvertDecimal(mat.Groups["YQ"].Value);
                price.AirportBuildFee = PriceAnalysis.ConvertDecimal(mat.Groups["TAX"].Value);

                lstPrice.Add(price);
            }

            return lstPrice;
        }
    }
}