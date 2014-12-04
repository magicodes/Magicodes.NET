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
//        filename :MC
//        description :
//
//        created by 雪雁 at  2014/10/19 16:46:38
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces
{
    public class MC
    {
        private static readonly Lazy<MC> LazyContext = new Lazy<MC>(() => new MC());
        /// <summary>
        /// 当前全局上下文对象
        /// </summary>
        public static MC Current { get { return LazyContext.Value; } }
        private GlobalApplicationObject GlobalApplicationObject
        {
            get
            {
                return GlobalApplicationObject.Current;
            }
        }
        private ApplicationContextBase ApplicationContext
        {
            get
            {
                return GlobalApplicationObject.ApplicationContext;
            }
        }
        public MC()
        {
            
        }
    }
}
