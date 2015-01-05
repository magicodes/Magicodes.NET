using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :WebAPIBase
//        description :
//
//        created by 雪雁 at  2014/10/12 19:39:49
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.API
{
    public class WebAPIControllerBase : ApiController
    {
        /// <summary>
        /// 当前应用程序上下文对象
        /// </summary>
        public ApplicationContextBase ApplicationContext { get { return GlobalApplicationObject.Current.ApplicationContext; } }
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <returns></returns>
        public IUser<TKey> GetUser<TKey>()
        {
            var strategy = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy<TKey>>();
            if (strategy == null) return null;
            return strategy.GetUser();
        }
    }
}
