using Magicodes.Web.Interfaces.Data;
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
namespace Magicodes.Web.Interfaces.Data.API.Messages
{
    public abstract class MessageDataRepositoryBase : IDataRepository<IMessage>
    {

        public abstract IQueryable<IMessage> GetQueryable();

        public abstract IEnumerable<IMessage> Get(System.Linq.Expressions.Expression<Func<IMessage, bool>> filter = null, Func<IQueryable<IMessage>, IOrderedQueryable<IMessage>> orderBy = null, string includeProperties = "");

        public abstract IEnumerable<IMessage> Get(System.Linq.Expressions.Expression<Func<IMessage, bool>> filter = null);

        public abstract IPagedList<IMessage> Get(System.Linq.Expressions.Expression<Func<IMessage, bool>> filter = null, Func<IQueryable<IMessage>, IOrderedQueryable<IMessage>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 1);

        public abstract IMessage Get(params dynamic[] keyValues);

        public abstract IMessage GetByID(dynamic id);

        public abstract void Add(IMessage entity);

        public abstract void Remove(dynamic id);

        public abstract void Remove(IMessage entityToDelete);

        public abstract void RemoveRange(IEnumerable<IMessage> entitys);

        public abstract void Update(IMessage entityToUpdate);

        public abstract void SaveChanges();
    }
}
