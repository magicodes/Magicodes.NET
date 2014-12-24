using Magicodes.Core.Data;
using Magicodes.Models.Blog.Models.Account;
using Magicodes.Models.Blog.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :BlogUnitOfWork
//        description :
//
//        created by 雪雁 at  2014/12/24 11:04:25
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog
{
    public class BlogUnitOfWork : UnitOfWorkBase<BlogDbContext>
    {
        public BlogUnitOfWork(BlogDbContext dbContext)
            : base(dbContext)
        {

        }
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbTransaction">为NULL则会启动一个事务</param>
        public BlogUnitOfWork(BlogDbContext dbContext, IDbTransaction dbTransaction = null)
            : base(dbContext, dbTransaction)
        {

        }
        private BlogUserRepository blogUserRepository;

        public BlogUserRepository BlogUserRepository
        {
            get
            {
                if (this.blogUserRepository == null)
                {
                    this.blogUserRepository = new BlogUserRepository(context);
                }
                return blogUserRepository;
            }
        }
    }
}
