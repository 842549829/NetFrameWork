using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// 乘客类型
    /// </summary>
    [DataContract]
    public enum PassengerType
    {
        /// <summary>
        /// 成人
        /// </summary>
        [EnumMember]
        成人 = 0,

        /// <summary>
        /// 儿童
        /// </summary>
        [EnumMember]
        儿童 = 1,

        /// <summary>
        /// 婴儿
        /// </summary>
        [EnumMember]
        婴儿 = 3,
    }
}