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
    //TODO:需要修改
    public class BlogUnitOfWork : UnitOfWorkBase<BlogDbContext>
    {

        private BlogUserRepository blogUserRepository;

        public BlogUserRepository BlogUserRepository
        {
            get
            {
                if (this.blogUserRepository == null)
                {
                    //this.blogUserRepository = new BlogUserRepository(context);
                }
                return blogUserRepository;
            }
        }
    }
}
