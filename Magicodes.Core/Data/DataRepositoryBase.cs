using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.Web.Interfaces.Data;
using System.Data.Entity;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :DataRepositoryBase
//        description :
//
//        created by 雪雁 at  2014/12/23 23:10:07
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Data
{
    public abstract class DataRepositoryBase<TEntity, TDbContext> : IDataRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected TDbContext context;
        internal DbSet<TEntity> dbSet;
        public DataRepositoryBase(TDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public virtual IPagedList<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 1)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);
            //计算总记录数
            var totalCount = query.Count();
            //获取分页结果
            var results = query.Skip((pageIndex - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();
            //返回分页列表
            return new PagedList<TEntity>(results, pageIndex, pageSize, totalCount);
        }

        public virtual TEntity Get(params dynamic[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public virtual TEntity GetByID(dynamic id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Remove(dynamic id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            //if (context.Entry(entityToDelete).State == EntityState.Detached)
            //    dbSet.Attach(entityToDelete);
            
            dbSet.Remove(entityToDelete);
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entitys)
        {
            dbSet.RemoveRange(entitys);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
        public IQueryable<TEntity> GetQueryable()
        {
            return dbSet.AsQueryable();
        }
    }
}
