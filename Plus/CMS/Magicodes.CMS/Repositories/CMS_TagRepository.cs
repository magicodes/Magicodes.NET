using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_TagRepository:DataRepositoryBase<CMS_Tag,CMSDbContext>
    {
        public CMS_TagRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}