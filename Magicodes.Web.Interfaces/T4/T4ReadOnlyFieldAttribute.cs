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
//        filename :T4ReadOnlyFieldAttribute
//        description :
//
//        created by 雪雁 at  2015/1/12 16:11:07
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4
{
    /// <summary>
    /// T4字段生成只读特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class T4ReadOnlyFieldAttribute : Attribute
    {
        public T4ReadOnlyFieldAttribute()
        {
            this.ReadOnlyType = ReadOnlyTypes.Add;
        }
        public T4ReadOnlyFieldAttribute(ReadOnlyTypes readOnlyType)
        {
            this.ReadOnlyType = readOnlyType;
        }
        public ReadOnlyTypes ReadOnlyType { get; set; }
    }
}
