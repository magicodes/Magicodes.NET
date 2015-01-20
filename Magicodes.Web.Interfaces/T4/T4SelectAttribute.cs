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
//        filename :T4SelectAttribute
//        description :
//
//        created by 雪雁 at  2015/1/15 14:34:59
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4
{
    /// <summary>
    /// 下拉列表生成特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class T4SelectAttribute : Attribute
    {
        /// <summary>
        /// JSONUrl
        /// </summary>
        public string DataUrl { get; set; }
        /// <summary>
        /// 显示字段名
        /// </summary>
        public string DisplayField { get; set; }
        /// <summary>
        /// 值字段名
        /// </summary>
        public string ValueField { get; set; }
        /// <summary>
        /// 根属性
        /// </summary>
        public string Root { get; set; }
        public T4SelectAttribute()
        {
        }
        public T4SelectAttribute(string dataUrl, string displayField, string valueField)
        {
            this.DataUrl = dataUrl;
            this.DisplayField = displayField;
            this.ValueField = valueField;
        }
        public T4SelectAttribute(string dataUrl, string displayField, string valueField, string root)
        {
            this.DataUrl = dataUrl;
            this.DisplayField = displayField;
            this.ValueField = valueField;
            this.Root = root;
        }
    }
}
