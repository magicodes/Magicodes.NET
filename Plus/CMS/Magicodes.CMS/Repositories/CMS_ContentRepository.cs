using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.CMS.ViewModels;
using Magicodes.Core.Data;

namespace Magicodes.CMS.Repositories
{
    public class CMS_ContentRepository:DataRepositoryBase<CMS_Content,CMSDbContext>
    {
        public CMS_ContentRepository(CMSDbContext context):base(context)
        {
            
        }

        public IEnumerable<CMS_ContentInfoViewModel> GetContentDetailInfo()
        {
            var contentInfoList = 
                                   (from type in context.CMS_ClassTypes
                                    join content in context.CMS_Contents
                                    on type.Id equals content.ClassTypeId
                                    where !content.Deleted
                                    select new CMS_ContentInfoViewModel()
                                    {
                                        Id = content.Id,
                                        ClassTypeName = type.ClassTypeName,
                                        Deleted = content.Deleted,
                                        Sequence = content.Sequence,
                                        CreateTime = content.CreateTime,
                                        Title = content.Title,
                                        Keywords = content.Keywords
                                    }).ToList();

            return contentInfoList;
        } 
    }
}