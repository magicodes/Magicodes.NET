using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.T4.Models
{
    public class T4PropertyInfo
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
        public int? MaxLength { get; set; }
        public string Description { get; set; }
        public T4DataType DataType { get; set; }
        public bool ReadOnly { get; set; }
        public bool _ReadOnly { get; set; }
        public ReadOnlyTypes _ReadOnlyType { get; set; }
        public string Tag { get; set; }
        /// <summary>
        /// 组信息
        /// </summary>
        public T4GroupInfo T4GroupInfo { get; set; }
        public T4SelectAttribute T4Select { get; set; }
    }
}
