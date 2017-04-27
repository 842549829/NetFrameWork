using System;
using System.Runtime.Serialization;

namespace Common.Code
{
    /// <summary>
    /// 分页类
    /// </summary>
    [Serializable]
    [DataContract]
    public class Paging
    {
        /// <summary>
        /// 页码(默认1)
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小(默认10)
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 总条数
        /// </summary>
        [DataMember]
        public int RowsCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        private int pageCount;

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int PageCount
        {
            get
            {
                this.pageCount = (this.RowsCount % this.PageSize) == 0
                                     ? this.RowsCount / this.PageSize
                                     : (this.RowsCount / this.PageSize) + 1;
                return this.pageCount;
            }

            set
            {
                this.pageCount = value;
            }
        }

        /// <summary>
        /// 是否获取总条数
        /// </summary>
        [DataMember]
        public bool GetRowsCount { get; set; } = true;

        /// <summary>
        /// 开始索引
        /// </summary>
        public int StratRows
        {
            get
            {
                if (this.PageIndex <= 0)
                {
                    return 0;
                }

                return this.PageSize * (this.PageIndex - 1);
            }
        }
    }
}