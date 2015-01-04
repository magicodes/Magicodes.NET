using Magicodes.Web.Interfaces.Data;
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
//        filename :MessageDataRepositoryBase
//        description :
//
//        created by 雪雁 at  2014/12/30 16:39:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data.API.SiteNavs
{
    public abstract class SiteAdminNavigationRepositoryBase<TUserKeyType> : IDataRepository<SiteAdminNavigationBase<TUserKeyType>>
    {

        public abstract IQueryable<SiteAdminNavigationBase<TUserKeyType>> GetQueryable();

        public abstract IEnumerable<SiteAdminNavigationBase<TUserKeyType>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<TUserKeyType>, bool>> filter = null, Func<IQueryable<SiteAdminNavigationBase<TUserKeyType>>, IOrderedQueryable<SiteAdminNavigationBase<TUserKeyType>>> orderBy = null, string includeProperties = "");

        public abstract IEnumerable<SiteAdminNavigationBase<TUserKeyType>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<TUserKeyType>, bool>> filter = null);

        public abstract IPagedList<SiteAdminNavigationBase<TUserKeyType>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<TUserKeyType>, bool>> filter = null, Func<IQueryable<SiteAdminNavigationBase<TUserKeyType>>, IOrderedQueryable<SiteAdminNavigationBase<TUserKeyType>>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 1);

        public abstract SiteAdminNavigationBase<TUserKeyType> Get(params dynamic[] keyValues);

        public abstract SiteAdminNavigationBase<TUserKeyType> GetByID(dynamic id);

        public abstract void Add(SiteAdminNavigationBase<TUserKeyType> entity);

        public abstract void Remove(dynamic id);

        public abstract void Remove(SiteAdminNavigationBase<TUserKeyType> entityToDelete);

        public abstract void RemoveRange(IEnumerable<SiteAdminNavigationBase<TUserKeyType>> entitys);

        public abstract void Update(SiteAdminNavigationBase<TUserKeyType> entityToUpdate);

        public abstract void SaveChanges();
    }
}
