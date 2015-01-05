using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    ///  ”∆µ¿‡–Õ
    /// </summary>
    public partial class CMS_VideoClass : CommonBusinessModelBase<int, string>
    {
        public string VideoClassName { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int Sequence { get; set; }
    }
}
