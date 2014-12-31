using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_CommentRepository:DataRepositoryBase<CMS_Comment,CMSDbContext>
    {
        public CMS_CommentRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}