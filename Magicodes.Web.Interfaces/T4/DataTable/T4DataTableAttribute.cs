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
//        filename :T4DataTableAttribute
//        description :
//
//        created by 雪雁 at  2015/1/21 14:30:16
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4.DataTable
{
    /// <summary>
    /// 数据列表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class T4DataTableAttribute : Attribute
    {
        /// <summary>
        /// 表格标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 表格描述
        /// </summary>
        public string Description { get; set; }
    }
}
