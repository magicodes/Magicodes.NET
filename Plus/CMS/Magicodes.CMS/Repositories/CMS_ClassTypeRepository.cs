using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ClassTypeRepository:DataRepositoryBase<CMS_ClassType,CMSDbContext>
    {
        public CMS_ClassTypeRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}