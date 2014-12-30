using Magicodes.CMS.Migrations;
using Magicodes.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.Models
{
    public class CMSDbContext : DbContext
    {
        static CMSDbContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CMSDbContext, Configuration>());
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        public CMSDbContext()
            : base(GlobalApplicationObject.Current.ConnectionStringName)
        {

        }
        public static CMSDbContext Create()
        {
            return new CMSDbContext();
        }

    }
}