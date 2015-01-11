using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Models.Mvc.Models.Menu;
using Magicodes.Models.Mvc.Models.Site;
using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Configuration
//        description :
//
//        created by 雪雁 at  2014/10/22 14:10:35
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Magicodes.Models.Mvc.AppDbContext>
    {
        public Configuration()
        {
            //关闭自动生成迁移（让程序只打我们自己生成的迁移）
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Magicodes.Models.Mvc.AppDbContext context)
        {
            #region 添加用户和角色
            var store = new AppUserStore(context);
            var userManager = new UserManager<AppUser, string>(store);
            var roleManager = new RoleManager<AppRole>(new AppRoleStore(context));
            var adminRole = new AppRole() { Id = "{74ABBD8D-ED32-4C3A-9B2A-EB134BFF5D91}", Name = "Admin", CreateTime = DateTime.Now, Deleted = false };
            if (!roleManager.RoleExists(adminRole.Name))
                roleManager.Create(adminRole);
            var user = new AppUser
            {
                Id = "{B0FBB2AC-3174-4E5A-B772-98CF776BD4B9}",
                UserName = "admin",
                Email = "liwq@magicodes.net",
                EmailConfirmed = true,
                Deleted = false,
                DisplayName = "管理员",
                IsActive = true,
                CreateTime = DateTimeOffset.Now
            };
            if (!userManager.Users.Any(p => p.Id == user.Id))
            {
                var result = userManager.Create(user, "123456abcD");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, adminRole.Name);
                }
            }
            var magicodes = new AppUser
            {
                Id = "{84389DCE-AE1F-47A3-8C42-15058B6B4CCB}",
                UserName = "magicodes",
                Email = "magicodes@magicodes.net",
                EmailConfirmed = true,
                Deleted = false,
                DisplayName = "Magicodes.NET",
                IsActive = true,
                CreateTime = DateTimeOffset.Now
            };
            if (!userManager.Users.Any(p => p.Id == magicodes.Id))
            {
                var result = userManager.Create(magicodes, "123456abcD");
            }
            #endregion

            #region 添加角色菜单
            var admin = userManager.FindById(user.Id);
            var role = roleManager.FindById(adminRole.Id);

            context.MenuLinks.ToList().ForEach(item =>
            {
                if (!item.Roles.Any(p => p.Id == role.Id))
                    item.Roles.Add(role);
            });
            context.SaveChanges();
            #endregion

            #region 添加留言测试消息
            for (int i = 0; i < 100; i++)
            {
                var siteLeaveMessage = new SiteLeaveMessage()
                {
                    Id = i,
                    Content = "网站留言测试内容" + i,
                    CreateTime = DateTimeOffset.Now,
                    CreateBy = magicodes.Id,
                    Deleted = false,
                };
                context.SiteLeaveMessages.AddOrUpdate(siteLeaveMessage);
            }
            #endregion

            #region 添加版本历史信息
            var all = from c in context.PublishVersions select c;
            context.PublishVersions.RemoveRange(all);


            var publishVersion_1 = new Magicodes.Models.Mvc.Models.PublishVersion()
            {

                Title = "Magicodes 1.0.0.0 Beta 版",
                Content = "<p>测试版本，由于精力有限，尚有很多不足之处。此版目前只针对团队成员开放。</p><ul>                 <li>后台框架已经基本完成，包括插件式架构、路由系统、WebAPI和OData、事件管理（待完善）、性能监控（待完善）、配置管理。</li>                 <li>前端UI基本规范，但是存在不少细节问题。</li>                 <li>下一版本计划支持声明式认证以及MVC等功能。</li>             </ul>",
                CreateTime = DateTimeOffset.Now,
                CreateBy = magicodes.Id,
                Deleted = false,
            };
            context.PublishVersions.AddOrUpdate(publishVersion_1);

            var publishVersion_2 = new Magicodes.Models.Mvc.Models.PublishVersion()
            {

                Title = "Magicodes 1.0.0.1 Beta 版",
                Content = "<p>测试版本。主要修复了前端细节问题。</p><ul>                 <li>主要修复了前端UI的若干问题。具体内容敬请期待后面的博文介绍。</li>             </ul>",
                CreateTime = DateTimeOffset.Now,
                CreateBy = magicodes.Id,
                Deleted = false,
            };
            context.PublishVersions.AddOrUpdate(publishVersion_2);

            var publishVersion_3 = new Magicodes.Models.Mvc.Models.PublishVersion()
            {

                Title = "Magicodes 1.0.0.2 Beta 版（New）",
                Content = "<p>测试版本。具体请查看http://www.cnblogs.com/codelove/p/4058433.html。</p><ul>                 <li>                     1. 架构多次重构，甚至核心模块多次推倒重来。                 </li>                 <li>                     2. 架构已支持MVC，不过却暂时放弃了WebForm，当然也有可能永久放弃WebForm，毕竟我目前只是一个人在战斗，兼容两套时间精力都极为有限。                 </li>                 <li>                     3. 引入了T4，已支持基于T4模板的代码生成。                 </li>                 <li>                     4. 已支持SignalR。                 </li>                 <li>                     5. 已支持ASP.NET Identity以及集成OAuth（Microsoft、QQ、Google…），暂时移除了对Form验证的支持。                 </li>                 <li>                     6. 支持WebAPI和Odata。                 </li>                 <li>                     7. 前端框架初成，支持响应式布局以及MVVM模式和模块化加载。                 </li>                 <li>                     8. 其他                 </li>                 具体请查看http://www.cnblogs.com/codelove/p/4058433.html。             </ul>",
                CreateTime = DateTimeOffset.Now,
                CreateBy = magicodes.Id,
                Deleted = false,
            };
            context.PublishVersions.AddOrUpdate(publishVersion_3);

            #endregion
        }
    }
}
