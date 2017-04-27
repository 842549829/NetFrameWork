using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// 座位的状态
    /// </summary>
    [DataContract]
    public enum SeatStatus
    {
        /// <summary>
        /// 预订状态，可以生成订单
        /// </summary>
        [EnumMember]
        HK,
        /// <summary>
        /// 不能生成订单
        /// </summary>
        [EnumMember]
        HL,
        /// <summary>
        /// 不能生成订单
        /// </summary>
        [EnumMember]
        HN,
        /// <summary>
        /// 不能生成订单
        /// </summary>
        [EnumMember]
        NN,
        /// <summary>
        /// 确定订位，可以生成订单
        /// </summary>
        [EnumMember]
        RR,
        /// <summary>
        /// 不能生成订单
        /// </summary>
        [EnumMember]
        NO,
        /// <summary>
        /// 已被取消，不能生成订单
        /// </summary>
        [EnumMember]
        XX,
        /// <summary>
        /// 不能生成订单,航空公司X编码
        /// </summary>
        [EnumMember]
        HX,
        /// <summary>
        /// 航班已变更，不能生成订单
        /// </summary>
        [EnumMember]
        UN,
        /// <summary>
        /// 已K到位置，可以生成订单
        /// </summary>
        [EnumMember]
        KK,
        /// <summary>
        /// 预订状态，可以生成订单
        /// </summary>
        [EnumMember]
        DK,
        /// <summary>
        /// 申请状态，不能生成订单
        /// </summary>
        [EnumMember]
        DL,
        /// <summary>
        /// 可以生成订单
        /// </summary>
        [EnumMember]
        KL,
        /// <summary>
        /// 其它状态，可以生成订单
        /// </summary>
        [EnumMember]
        OTHER,
        /// <summary>
        /// 航班变动
        /// </summary>
        [EnumMember]
        TK,
        /// <summary>
        /// UU
        /// </summary>
        [EnumMember]
        UU,
        /// <summary>
        ///  UC
        /// </summary>
        [EnumMember]
        UC
    }
}