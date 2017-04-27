using System;
using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// PNR舱位对象
    /// </summary>
    [DataContract]
    public class MPnrSeat
    {
        /// <summary>
        /// 是否共享航班
        /// </summary>
        [DataMember]
        public bool IsShareFlight { get; set; }

        /// <summary>
        /// 共享航班
        /// </summary>
        [DataMember]
        public string ShareFlightNO { get; set; }

        /// <summary>
        /// 座位状态
        /// </summary>
        [DataMember]
        public SeatStatus Status { get; set; }

        /// <summary>
        /// 出发航站楼
        /// </summary>
        [DataMember]
        public string DepartureTerm { get; set; }

        /// <summary>
        /// 到达航站楼
        /// </summary>
        [DataMember]
        public string ArrivalTerm { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        [DataMember]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 到达时间
        /// </summary>
        [DataMember]
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// 航班号
        /// </summary>
        [DataMember]
        public string FlightNO { get; set; }

        /// <summary>
        /// 出发城市
        /// </summary>
        [DataMember]
        public string FromCity { get; set; }

        /// <summary>
        /// 到达城市
        /// </summary>
        [DataMember]
        public string ToCity { get; set; }

        /// <summary>
        /// 舱位
        /// </summary>
        [DataMember]
        public string SeatCode { get; set; }

        /// <summary>
        /// 出发城市名称
        /// </summary>
        [DataMember]
        public string FromCityName { get; set; }

        /// <summary>
        /// 到达城市名称
        /// </summary>
        [DataMember]
        public string ToCityName { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        [DataMember]
        public int Discount { get; set; }
    }
}