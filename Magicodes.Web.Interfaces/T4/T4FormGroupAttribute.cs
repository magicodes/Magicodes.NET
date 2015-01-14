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
    /// T4表单控件组
    /// 注意：如果需要生成组，则首先需要在相关类添加此特效以表示启用组生成，然后在相关属性上添加此特效以标注此属性的分组
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
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
