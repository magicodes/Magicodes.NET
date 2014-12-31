using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ChannelRepository:DataRepositoryBase<CMS_Channel,CMSDbContext>
    {
        public CMS_ChannelRepository(CMSDbContext context):base(context)
        {
            
        }
    }
}