using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.ViewModels
{
    public class CMS_ContentInfoViewModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }

     
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(50)]
        public string ClassTypeName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [MaxLength(200)]
        public string Keywords { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<int> Sequence { get; set; }

        public bool Deleted { get; set; }
    }
}