using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.Models
{
    public class CMS_ContentTag : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 标签Id
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        /// 内容Id
        /// </summary>
        public Guid ContentId { get; set; }
        /// <summary>
        /// 总推荐数
        /// </summary>
        public int TotalRecommended { get; set; }
    }
}