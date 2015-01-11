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
    }
}
