using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_PhotoRepository:DataRepositoryBase<CMS_Photo,CMSDbContext>
    {
        public CMS_PhotoRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}