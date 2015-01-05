using Magicodes.Web.Interfaces.Data.API.SiteNavs;
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
//        filename :APIContext
//        description :
//
//        created by 雪雁 at  2015/1/5 14:11:52
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data.API
{
    public class APIContext<TUserKeyType> : IUnitOfWork
    {
        private static readonly Lazy<APIContext<TUserKeyType>> LazyContext = new Lazy<APIContext<TUserKeyType>>(() => new APIContext<TUserKeyType>());
        /// <summary>
        /// 当前全局上下文对象
        /// </summary>
        public static APIContext<TUserKeyType> Current { get { return LazyContext.Value; } }
        public SiteAdminNavigationRepositoryBase<TUserKeyType> SiteAdminNavigationRepository { get; set; }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
