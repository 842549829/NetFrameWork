using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// PNR对象
    /// </summary>
    [DataContract]
    public class MPnrContent
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MPnrContent()
        {
            this.Passengers = new List<MPnrMPassenger>();
            this.Seats = new List<MPnrSeat>();
            this.Pice = new List<MPnrPrice>();
        }

        /// <summary>
        /// 是否换编码
        /// </summary>
        [DataMember]
        public bool IsChangeCode { get; set; }

        /// <summary>
        /// PNR内容
        /// </summary>
        [DataMember]
        public string PNRContentStr { get; set; }

        /// <summary>
        /// 编码状态
        /// </summary>
        [DataMember]
        public PNRStatus Status { get; set; }

        /// <summary>
        /// 是否团队票
        /// </summary>
        [DataMember]
        public bool IsTeam { get; set; }

        /// <summary>
        /// SSR FOID数量
        /// </summary>
        [DataMember]
        public int SSRFOIDCount { get; set; }

        /// <summary>
        /// pnr价格对象
        /// </summary>
        [DataMember]
        public List<MPnrPrice> Pice { get; set; }

        /// <summary>
        /// office号
        /// </summary>
        [DataMember]
        public string OfficeCode { get; set; }

        /// <summary>
        /// 编码舱位集合
        /// </summary>
        [DataMember]
        public List<MPnrSeat> Seats { get; set; }

        /// <summary>
        /// 小编码
        /// </summary>
        [DataMember]
        public string Pnr { get; set; }

        /// <summary>
        /// 大编码
        /// </summary>
        [DataMember]
        public string BigPnr { get; set; }

        /// <summary>
        /// 父编码
        /// </summary>
        [DataMember]
        public string ParentPnr { get; set; }

        /// <summary>
        /// 乘机人集合
        /// </summary>
        [DataMember]
        public List<MPnrMPassenger> Passengers { get; set; }
    }
}