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
//        filename :T4OnlyVerification
//        description :
//
//        created by 雪雁 at  2014/10/28 21:51:59
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4
{
    /// <summary>
    /// T4生成唯一验证代码【属性】
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class T4OnlyVerificationAttribute : Attribute
    {
        /// <summary>
        /// 使用默认的构造函数表示此属性生成唯一验证代码
        /// </summary>
        public T4OnlyVerificationAttribute()
        {

        }
        /// <summary>
        /// 忽略已删除数据
        /// </summary>
        public bool IgnoreDeletedData { get; set; }
        /// <summary>
        /// 使用默认的构造函数表示此属性生成唯一验证代码
        /// </summary>
        public T4OnlyVerificationAttribute(bool ignoreDeletedData)
        {
            IgnoreDeletedData = ignoreDeletedData;
        }

    }
}
