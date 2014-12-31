using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ContentRepository:DataRepositoryBase<CMS_Content,CMSDbContext>
    {
        public CMS_ContentRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}