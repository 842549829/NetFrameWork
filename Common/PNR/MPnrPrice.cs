using System.Runtime.Serialization;

namespace Common.PNR
{
    /// <summary>
    /// PNR价格项
    /// </summary>
    [DataContract]
    public class MPnrPrice
    {
        /// <summary>
        /// 燃油费 
        /// </summary>
        [DataMember]
        public decimal FuelFee { get; set; }

        /// <summary>
        /// 机场建设费
        /// </summary>
        [DataMember]
        public decimal AirportBuildFee { get; set; }

        /// <summary>
        /// 票面价
        /// </summary>
        [DataMember]
        public decimal Fare { get; set; }

        /// <summary>
        /// 销售价(不含基建、燃油)
        /// </summary>
        [DataMember]
        public decimal SalePrice { get; set; }
    }
}