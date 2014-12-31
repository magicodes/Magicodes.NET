using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_VideoRepository:DataRepositoryBase<CMS_Video,CMSDbContext>
    {
        public CMS_VideoRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}