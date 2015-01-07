using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Repositories;
using Magicodes.Core.Data;

namespace Magicodes.CMS.UnitOfWork
{
    public partial class CMS_DbSession:DbSessionBase<CMSDbContext>
    {
        
        private CMS_ChannelRepository cms_ChannelRepository;
        public CMS_ChannelRepository CMS_ChannelRepository
        {
            get
            {
                if (cms_ChannelRepository == null)
                {
                    cms_ChannelRepository = new CMS_ChannelRepository(context);
                }
                return cms_ChannelRepository;
            }
        }

        private CMS_ClassTypeRepository cms_ClassTypeRepository;
        public CMS_ClassTypeRepository CMS_ClassTypeRepository
        {
            get
            {
                if (cms_ClassTypeRepository == null)
                {
                    cms_ClassTypeRepository = new CMS_ClassTypeRepository(context);
                }
                return cms_ClassTypeRepository;
            }
        }

        private CMS_CommentRepository cms_CommentRepository;
        public CMS_CommentRepository CMS_CommentRepository
        {
            get
            {
                if (cms_CommentRepository == null)
                {
                    cms_CommentRepository = new CMS_CommentRepository(context);
                }
                return cms_CommentRepository;
            }
        }

        private CMS_ContentRepository cms_ContentRepository;
        public CMS_ContentRepository CMS_ContentRepository
        {
            get
            {
                if (cms_ContentRepository == null)
                {
                    cms_ContentRepository = new CMS_ContentRepository(context);
                }
                return cms_ContentRepository;
            }
        }

        private CMS_ContentTagRepository cms_ContentTagRepository;
        public CMS_ContentTagRepository CMS_ContentTagRepository
        {
            get
            {
                if (cms_ContentTagRepository == null)
                {
                    cms_ContentTagRepository = new CMS_ContentTagRepository(context);
                }
                return cms_ContentTagRepository;
            }
        }

        private CMS_PhotoRepository cms_PhotoRepository;
        public CMS_PhotoRepository CMS_PhotoRepository
        {
            get
            {
                if (cms_PhotoRepository == null)
                {
                    cms_PhotoRepository = new CMS_PhotoRepository(context);
                }
                return cms_PhotoRepository;
            }
        }

        private CMS_TagRepository cms_TagRepository;
        public CMS_TagRepository CMS_TagRepository
        {
            get
            {
                if (cms_TagRepository == null)
                {
                    cms_TagRepository = new CMS_TagRepository(context);
                }
                return cms_TagRepository;
            }
        }

        private CMS_VideoRepository cms_VideoRepository;
        public CMS_VideoRepository CMS_VideoRepository
        {
            get
            {
                if (cms_VideoRepository == null)
                {
                    cms_VideoRepository = new CMS_VideoRepository(context);
                }
                return cms_VideoRepository;
            }
        }
    }
}