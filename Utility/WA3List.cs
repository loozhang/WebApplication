using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// 为ADC提供专有的集合列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable()]
    public class WA3List<T> : List<T>
    {
        /// <summary>
        /// ADC列表只显示一页的列表，条目数为PageSize指定的数值
        /// </summary>
        /// <param name="pageSize">一页的大小</param>
        public WA3List(int pageSize)
            : base(pageSize)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public WA3List()
        {
        }

        private int totalRecords;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        private int totalPage;

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return totalPage; }
            set { totalPage = value; }
        }
    }
}