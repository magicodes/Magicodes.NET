
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Magicodes.CMS.Models;

namespace Magicodes.CMS.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CMSDbContext>
    {
        public Configuration()
        {
            //关闭自动生成迁移（让程序只打我们自己生成的迁移）
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CMSDbContext context)
        {
            
        }
    }
}
