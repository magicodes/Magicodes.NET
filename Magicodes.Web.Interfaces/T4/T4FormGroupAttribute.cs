using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :T4FormGroupAttribute
//        description :
//
//        created by 雪雁 at  2015/1/5 12:13:06
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4
{
    /// <summary>
    /// T4表单控件组【类】
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class T4FormGroupAttribute : Attribute
    {
        public T4FormGroupAttribute(string groupName)
        {
            this.GroupName = groupName;
        }
        public T4FormGroupAttribute(string groupName, bool allowCollapse)
        {
            this.GroupName = groupName;
            this.AllowCollapse = allowCollapse;
        }
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 允许折叠
        /// </summary>
        public bool AllowCollapse { get; set; }
    }
}
