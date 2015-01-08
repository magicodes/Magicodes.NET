using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.CMS.ViewModels;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ClassTypeRepository:DataRepositoryBase<CMS_ClassType,CMSDbContext>
    {
        public CMS_ClassTypeRepository(CMSDbContext context):base(context)
        {
           
        }

        public IEnumerable<CMS_ClassTypeInfoViewModel> GetClassTypeDetailInfo()
        {
            var classTypes=(from c in context.CMS_Channels
                                     join t in context.CMS_ClassTypes
                                    on c.Id equals t.ChannelId
                                    where !t.Deleted
                                    select new CMS_ClassTypeInfoViewModel
                                    {
                                        ClassTypeName = t.ClassTypeName,
                                        Id = t.Id,
                                        Deleted=t.Deleted,
                                        Sequence = t.Sequence,
                                        CreateTime = t.CreateTime,
                                        ChannelName = c.ChannelName
                                    }).ToList();

            return classTypes;
        } 
    }
}