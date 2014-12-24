using Magicodes.Web.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :UnitOfWorkBase
//        description :数据单元操作基类
//
//        created by 雪雁 at  2014/12/23 23:30:59
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Data
{
    /// <summary>
    /// 数据单元操作基类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork
        where TDbContext : DbContext, new()
    {
        protected TDbContext context = new TDbContext();
        public readonly IDbTransaction transaction;

        public UnitOfWorkBase(TDbContext dbContext)
        {
            context = dbContext;
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbTransaction">为NULL则会启动一个事务</param>
        public UnitOfWorkBase(TDbContext dbContext, IDbTransaction dbTransaction = null)
        {
            context = dbContext;
            if (dbTransaction == null)
            {
                if (context.Database.Connection.State != ConnectionState.Open)
                {
                    context.Database.Connection.Open();
                    transaction = context.Database.Connection.BeginTransaction();
                }
            }
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
        /// <summary>
        /// 提交
        /// </summary>
        public virtual void Commit()
        {
            SaveChanges();
            if (transaction != null)
                transaction.Commit();
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public virtual void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
            //foreach (var entry in context.ChangeTracker.Entries())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Modified:
            //            entry.State = EntityState.Unchanged;
            //            break;
            //        case EntityState.Added:
            //            entry.State = EntityState.Detached;
            //            break;
            //        case EntityState.Deleted:
            //            // Note - problem with deleted entities:
            //            // When an entity is deleted its relationships to other entities are severed. 
            //            // This includes setting FKs to null for nullable FKs or marking the FKs as conceptually null (don’t ask!) 
            //            // if the FK property is not nullable. You’ll need to reset the FK property values to 
            //            // the values that they had previously in order to re-form the relationships. 
            //            // This may include FK properties in other entities for relationships where the 
            //            // deleted entity is the principal of the relationship–e.g. has the PK 
            //            // rather than the FK. I know this is a pain–it would be great if it could be made easier in the future, but for now it is what it is.
            //            entry.State = EntityState.Unchanged;
            //            break;
            //    }
            //}
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //if (context.Database.Connection.State == ConnectionState.Open)
                    //{
                    //    context.Database.Connection.Close();
                    //}
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
