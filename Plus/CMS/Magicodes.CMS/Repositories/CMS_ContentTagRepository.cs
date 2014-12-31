using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ContentTagRepository:DataRepositoryBase<CMS_ContentTag,CMSDbContext>
    {
        public CMS_ContentTagRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}