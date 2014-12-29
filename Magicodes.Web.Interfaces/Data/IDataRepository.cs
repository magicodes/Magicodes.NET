using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :IDataRepository
//        description :
//
//        created by 雪雁 at  2014/12/23 23:01:53
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryable();
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        IPagedList<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "", int pageSize = 10, int pageIndex = 1);
        TEntity Get(params dynamic[] keyValues);
        TEntity GetByID(dynamic id);

        void Add(TEntity entity);
        void Remove(dynamic id);
        void Remove(TEntity entityToDelete);
        void RemoveRange(IEnumerable<TEntity> entitys);
        void Update(TEntity entityToUpdate);
        void SaveChanges();
    }
}
