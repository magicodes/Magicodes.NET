using Magicodes.Core.Data;
using Magicodes.Models.Blog.Models.Account;
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
//        filename :BlogUserRepository
//        description :
//
//        created by 雪雁 at  2014/12/24 11:08:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Repositorys
{
    public class BlogUserRepository : DataRepositoryBase<BlogUser, BlogDbContext>
    {

        public BlogUserRepository(BlogDbContext context)
            : base(context)
        {
        }

    }
}
