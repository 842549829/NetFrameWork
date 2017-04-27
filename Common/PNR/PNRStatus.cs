using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// 编码状态
    /// </summary>
    [DataContract]
    public enum PNRStatus
    {
        /// <summary>
        /// NO PNR
        /// </summary>
        [EnumMember]
        NoPNR,

        /// <summary>
        /// PNR已被取消
        /// </summary>
        [EnumMember]
        Cancelled,

        /// <summary>
        /// 正常PNR
        /// </summary>
        [EnumMember]
        Normal,

        /// <summary>
        /// 已出票
        /// </summary>
        [EnumMember]
        Outed,

        /// <summary>
        /// 已失效
        /// </summary>
        [EnumMember]
        Invalid,
    }
}