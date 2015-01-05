using Magicodes.Models.Mvc.Models;
using Magicodes.Web.Interfaces.Data;
using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :SiteAdminNavigationRepository
//        description :
//
//        created by 雪雁 at  2014/12/31 15:46:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.DAL
{
    public class SiteAdminNavigationRepository : SiteAdminNavigationRepositoryBase<string>
    {
        internal AppDbContext context = new AppDbContext();
        internal DbSet<SiteAdminNavigation> dbSet;
        public SiteAdminNavigationRepository()
        {
            this.dbSet = context.SiteAdminNavigations;
        }
        public override IQueryable<SiteAdminNavigationBase<string>> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public override IEnumerable<SiteAdminNavigationBase<string>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<string>, bool>> filter = null, Func<IQueryable<SiteAdminNavigationBase<string>>, IOrderedQueryable<SiteAdminNavigationBase<string>>> orderBy = null, string includeProperties = "")
        {
            IQueryable<SiteAdminNavigationBase<string>> query = dbSet;

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

        public override IEnumerable<SiteAdminNavigationBase<string>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<string>, bool>> filter = null)
        {
            IQueryable<SiteAdminNavigationBase<string>> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public override Web.Interfaces.Data.IPagedList<SiteAdminNavigationBase<string>> Get(System.Linq.Expressions.Expression<Func<SiteAdminNavigationBase<string>, bool>> filter = null, Func<IQueryable<SiteAdminNavigationBase<string>>, IOrderedQueryable<SiteAdminNavigationBase<string>>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 1)
        {
            IQueryable<SiteAdminNavigationBase<string>> query = dbSet;

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
            return new PagedList<SiteAdminNavigationBase<string>>(results, pageIndex, pageSize, totalCount);
        }

        public override SiteAdminNavigationBase<string> Get(params dynamic[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public override SiteAdminNavigationBase<string> GetByID(dynamic id)
        {
            return dbSet.Find(id);
        }

        public override void Add(SiteAdminNavigationBase<string> entity)
        {
            var nav = ConvertToChildT(entity);
            dbSet.Add(nav);
        }

        private static SiteAdminNavigation ConvertToChildT(SiteAdminNavigationBase<string> entity)
        {
            var jsonStr = JsonConvert.SerializeObject(entity);
            return JsonConvert.DeserializeObject<SiteAdminNavigation>(jsonStr);
        }
        private static IEnumerable<SiteAdminNavigation> ConvertToChildT(IEnumerable<SiteAdminNavigationBase<string>> entitys)
        {
            var jsonStr = JsonConvert.SerializeObject(entitys);
            return JsonConvert.DeserializeObject<IEnumerable<SiteAdminNavigation>>(jsonStr);
        }

        public override void Remove(dynamic id)
        {
            var entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }

        public override void Remove(SiteAdminNavigationBase<string> entityToDelete)
        {
            Remove(entityToDelete.Id);
        }

        public override void RemoveRange(IEnumerable<SiteAdminNavigationBase<string>> entitys)
        {
            foreach (var entityToDelete in entitys)
            {
                Remove(entityToDelete.Id);
            }
        }

        public override void Update(SiteAdminNavigationBase<string> entityToUpdate)
        {
            dbSet.Attach(entityToUpdate as SiteAdminNavigation);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public override void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
