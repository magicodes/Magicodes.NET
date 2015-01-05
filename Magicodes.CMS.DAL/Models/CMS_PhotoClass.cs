using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// 图片类型
    /// </summary>
    public partial class CMS_PhotoClass : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 图片类型名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public Nullable<int> ParentId { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<int> Sequence { get; set; }
    }
}
