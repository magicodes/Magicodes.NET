using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Web.Interfaces.Models;

namespace Magicodes.CMS.ViewModels
{
    [Serializable]
    public class CMS_ClassTypeInfoViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 栏目名
        /// </summary>
        [MaxLength(50)]
        public string ChannelName { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(50)]
        public string ClassTypeName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<int> Sequence { get; set; }

        public bool Deleted { get; set; }

    }
}