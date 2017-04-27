using System;
using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// 乘客对象
    /// </summary>
    [DataContract]
    public class MPnrMPassenger
    {
        /// <summary>
        /// 结算代码
        /// </summary>
        private string settleCode;

        /// <summary>
        /// 票号
        /// </summary>
        private string ticketCode;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MPnrMPassenger()
        {
            this.MobileNo = string.Empty;
        }

        /// <summary>
        /// 乘机人手机号
        /// </summary>
        [DataMember]
        public string MobileNo
        {
            get;
            set;
        }

        /// <summary>
        /// 结算代码
        /// </summary>
        [DataMember]
        public string SettleCode
        {
            get { return this.settleCode ?? string.Empty; }
            set { this.settleCode = value; }
        }

        /// <summary>
        /// 票号
        /// </summary>
        [DataMember]
        public string TicketCode
        {
            get { return this.ticketCode ?? string.Empty; }
            set { this.ticketCode = value; }
        }

        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        public int SequenceNO { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [DataMember]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        [DataMember]
        public string CredID { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public CredentialsType CredType { get; set; }

        /// <summary>
        /// 乘客类型
        /// </summary>
        [DataMember]
        public PassengerType Type { get; set; }

        /// <summary>
        /// 发卡银行 公务员票新增字段
        /// </summary>
        [DataMember]
        public string BankName
        {
            get;
            set;
        }

        /// <summary>
        /// 发卡银行Code 公务员票新增字段
        /// </summary>
        [DataMember]
        public string BankCode
        {
            get;
            set;
        }
    }
}