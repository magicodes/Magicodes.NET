using Magicodes.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//        description :Anton
//        created by Anton at 2014年11月7日 19:05:34
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.BlogData
{
    public partial class BaseRepository<T> where T : class, new()
    {
        //EF上下文的实例保证，线程内唯一
        //实例化EF框架
        //DataModelContainer db = new DataModelContainer();

        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        private DbContext db = EFContextFactory.GetCurrentDbContext();

        //添加
        public T AddEntity(T entity,bool isSave=false)
        {
            EntityState state = db.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                db.Entry(entity).State = EntityState.Added;
            }
            if (isSave)
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return entity; 
        }

        //修改 1
        public bool UpdateEntity(T entity, bool isSave = false)
        {
            var entry = db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                db.Set<T>().Attach(entity);
                entry.State = EntityState.Modified;
            }
            if (isSave) return db.SaveChanges() > 0;
            return false;
        }
        //修改2 按需修改
        public bool UpdateEntity(Expression<Func<T, T>> propertyExpression, bool isSave = false) 
        {
            try
            {
                var memberInitExpression = propertyExpression.Body as MemberInitExpression;
                var entity = PropertyUtils.CreateEntity(propertyExpression);
                DbEntityEntry<T> entry = db.Entry(entity);
                entry.State = EntityState.Unchanged;
                if (memberInitExpression != null)
                    foreach (var memberInfo in memberInitExpression.Bindings)
                    {
                        string propertyName = memberInfo.Member.Name;
                        entry.Property(propertyName).IsModified = true;
                    }
                db.Configuration.ValidateOnSaveEnabled = false;
                if (isSave) return db.SaveChanges() > 0;
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Configuration.AutoDetectChangesEnabled = true;
            }

        }
        //删除 可根据主键值 直接删除
        public bool DeleteEntity(T entity,bool isSave=false)
        {
            try
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                DbEntityEntry<T> entry = db.Entry(entity);
                var aaa = db.Entry(entity).State;
               
                    db.Entry(entity).State = EntityState.Deleted;
                
                db.Configuration.ValidateOnSaveEnabled = false;
                //entry.State = EntityState.Unchanged;
                //entry.State = EntityState.Deleted;
                if (isSave) return db.SaveChanges() > 0;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        //查询
        public IQueryable<T> LoadEntity(Func<T, bool> wherelambda)
        {
            return db.Set<T>().Where<T>(wherelambda).AsQueryable();
        }

        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,
            Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = db.Set<T>().Where<T>(whereLambda);

            total = tempData.Count();

            //排序获取当前页的数据
            if (isAsc)
            {
                tempData = tempData.OrderBy<T, S>(orderByLambda).
                      Skip<T>(pageSize * (pageIndex - 1)).
                      Take<T>(pageSize).AsQueryable();
            }
            else
            {
                tempData = tempData.OrderByDescending<T, S>(orderByLambda).
                     Skip<T>(pageSize * (pageIndex - 1)).
                     Take<T>(pageSize).AsQueryable();
            }
            return tempData.AsQueryable();
        }

        /// <summary>
        /// 执行SQL语句查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryCmdStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public List<T> SqlQuery<T>(string queryCmdStr, object[] paras) 
        {
            try
            {
                return paras == null ? db.Database.SqlQuery<T>(queryCmdStr).ToList() : db.Database.SqlQuery<T>(queryCmdStr, paras).ToList();
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }

        }


    }
}
