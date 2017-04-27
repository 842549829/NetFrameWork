using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// 证件号类型
    /// </summary>
    [DataContract]
    public enum CredentialsType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [EnumMember]
        身份证,

        /// <summary>
        /// 出身日期
        /// </summary>
        [EnumMember]
        出生日期,

        /// <summary>
        /// 护照
        /// </summary>
        [EnumMember]
        护照,

        /// <summary>
        /// 军官证
        /// </summary>
        [EnumMember]
        军官证,

        /// <summary>
        /// 回乡证
        /// </summary>
        [EnumMember]
        回乡证,

        /// <summary>
        /// 台胞证
        /// </summary>
        [EnumMember]
        台胞证,

        /// <summary>
        /// 港澳通行证
        /// </summary>
        [EnumMember]
        港澳通行证,

        /// <summary>
        /// 国际海员证
        /// </summary>
        [EnumMember]
        国际海员证,

        /// <summary>
        /// 外国人永久居住证
        /// </summary>
        [EnumMember]
        外国人永久居住证,

        /// <summary>
        ///  其他
        /// </summary>
        [EnumMember]
        其他
    }
}