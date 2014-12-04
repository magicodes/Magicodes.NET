using Magicodes.Models.Mvc;
using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config.Info;
using Magicodes.Web.Interfaces.Strategy.Email;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AppUserManager
//        description :
//
//        created by 雪雁 at  2014/10/22 16:29:40
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Strategy.Identity
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            var strategy = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<IMailStrategy>();
            if (strategy == null)
            {
                GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.Log(LoggerLevels.Warn, string.Format("没有实现邮件策略，邮件【{0}】无法发送！", message.Subject));
                return Task.FromResult(false);
            }
            else
            {
                var mailMessage = new MailInfo()
                {
                    Body = message.Body,
                    Destination = message.Destination,
                    Subject = message.Subject
                };
                //if (GlobalApplicationObject.Current.ApplicationContext.IsDebug)
                GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.Log(LoggerLevels.Debug, Newtonsoft.Json.JsonConvert.SerializeObject(mailMessage));
                return strategy.SendAsync(mailMessage);
            }
        }
    }
    /// <summary>
    /// 短信服务
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IAppRoleStore store)
            : base(store)
        {
        }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var manager = new AppRoleManager(new AppRoleStore());
            return manager;
        }
    }
    /// <summary>
    /// 用户管理与配置
    /// </summary>
    public class AppUserManager : UserManager<AppUser, string>
    {
        public AppUserManager(IAppUserStore store)
            : base(store)
        {
        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
            IOwinContext context)
        {
            var manager = new AppUserManager(new AppUserStore());
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "您的安全码为：{0}。请妥善保管，打死也不能泄露哦！"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<AppUser>
            {
                Subject = "安全码",
                BodyFormat = "您的安全码为：{0}。请妥善保管，打死也不能泄露哦！"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// 将用户添加到多个角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roles">角色名列表</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> AddUserToRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<AppUser, string>)Store;
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("无效的用户Id！");
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            //挨个添加
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                await userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
            }
            //加完后，更新
            return await UpdateAsync(user).ConfigureAwait(false);
        }

        /// <summary>
        /// 从角色列表中移除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roles">角色列表</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<AppUser, string>)Store;
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("无效的用户Id");
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            foreach (var role in roles.Where(userRoles.Contains))
            {
                await userRoleStore.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
            }
            return await UpdateAsync(user).ConfigureAwait(false);
        }
    }
    /// <summary>
    /// 配置要在此应用程序中使用的应用程序登录管理器
    /// </summary>
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public AppOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
                else if (context.ClientId == AppAuthConfig.PublicClientId)
                {
                    var expectedUri = new Uri(context.Request.Uri, "/");
                    context.Validated(expectedUri.AbsoluteUri);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
    public class AppAuthConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
        static AppAuthConfig()
        {
            PublicClientId = "Magicodes.Web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new AppOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public static void ConfigureAuth(IAppBuilder app)
        {
            // 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            app.CreatePerOwinContext(AppDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            // 使应用程序可使用 Cookie 存储登录用户的信息
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, AppUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // 使用 Cookie 临时存储有关某个用户使用第三方登录提供程序进行登录的信息
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 使应用程序可以在双重身份验证过程中验证第二因素时暂时存储用户信息。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 使应用程序可以记住第二登录验证因素，例如电话或电子邮件。
            // 选中此选项后，登录过程中执行的第二个验证步骤将保存到你登录时所在的设备上。
            // 此选项类似于在登录时提供的“记住我”选项。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 使应用程序可以使用不记名令牌来验证用户
            app.UseOAuthBearerTokens(OAuthOptions);

            var oauthConfig = Magicodes.Web.Interfaces.GlobalApplicationObject.Current.ApplicationContext.ConfigManager.GetConfig<AccountAuthenticationInfo>();
            if (oauthConfig != null && oauthConfig.AccountConfigInfoList != null)
            {
                #region 启用微软账号登陆
                var microsoftAccount = oauthConfig.AccountConfigInfoList.FirstOrDefault(p => p.Enable && p.AccountType == AccountTypes.Microsoft);
                if (microsoftAccount != null)
                {
                    app.UseMicrosoftAccountAuthentication(
                        clientId: microsoftAccount.Id,
                        clientSecret: microsoftAccount.Secret);
                }
                #endregion
                #region 启用微软账号登陆
                var qqAccount = oauthConfig.AccountConfigInfoList.FirstOrDefault(p => p.Enable && p.AccountType == AccountTypes.QQ);
                if (qqAccount != null)
                {
                    app.UseQQConnectAuthentication(
                        appId: qqAccount.Id,
                        appSecret: qqAccount.Secret);
                }
                #endregion
            }

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
