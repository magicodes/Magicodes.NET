using Magicodes.Models.Default;
using Magicodes.Models.Default.Entitys.Account;
using Magicodes.Utility;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Magicodes.Strategy.FormAuth
{
    public class FormAuthStrategy : IUserAuthenticationStrategy
    {
        public AuthStatus IsCorrectLoginName(string userName)
        {
            var status = new AuthStatus() { IsSuccess = true };
            if (string.IsNullOrWhiteSpace(userName))
            {
                status.IsSuccess = false;
                status.Message = "登录名不能为空！";
            }
            return status;
        }

        public AuthStatus IsCorrectPassword(string password)
        {
            var status = new AuthStatus() { IsSuccess = true };
            if (string.IsNullOrWhiteSpace(password))
            {
                status.IsSuccess = false;
                status.Message = "密码不能为空！";
            }
            return status;
        }

        public string GetPasword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + "_Magicodes", "md5");
            //MD5加密
            //return EncryptUtils.MD5Encoding(password, "Magicodes");
        }

        public void LoginOut()
        {
            FormsAuthentication.SignOut();
        }

        public AuthStatus Login(string loginName, string password, bool isRememberPassword)
        {
            var loginStates = IsCorrectLoginName(loginName);
            if (!loginStates.IsSuccess) return loginStates;
            loginStates = IsCorrectPassword(loginName);
            if (!loginStates.IsSuccess) return loginStates;
            using (var db = new MagicodesDefaultDbContext())
            {
                var pwd = GetPasword(password);
                var member = db.Members.FirstOrDefault(p => p.LoginName == loginName && p.Password == pwd);
                if (member != null)
                {
                    loginStates.IsSuccess = true;
                    loginStates.Message = "登陆成功！";
                    var tkt = new FormsAuthenticationTicket(1, "loginName", DateTime.Now,
DateTime.Now.AddMinutes(30), isRememberPassword, member.ToJsonWithDateFormatyyyyMMddHHmmss());
                    var cookiestr = FormsAuthentication.Encrypt(tkt);
                    var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                    if (isRememberPassword)
                        ck.Expires = tkt.Expiration;
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    if (HttpContext.Current != null)
                        HttpContext.Current.Response.Cookies.Add(ck);
                    else
                    {
                        loginStates.IsSuccess = false;
                        loginStates.Message = "登陆失败，不支持此方式登录！";
                    }
                    //FormsAuthentication.SetAuthCookie(member.Id.ToString(), isRememberPassword);
                }
                else
                {
                    loginStates.IsSuccess = false;
                    loginStates.Message = "登陆失败，用户名或密码不正确！";
                }
            }
            return loginStates;
        }
        /// <summary>
        /// 获取当前登录用户数据
        /// </summary>
        /// <returns></returns>
        public IUser GetCurrentLoginUser()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                // 获取存储的数据
                return ticket.UserData.JsonStrDeserializeObject<Member>();
            }
            return null;
        }

        public void Initialize()
        {
        }

        

        public bool IsAuthenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }


        public void RedirectToLoginPage()
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}
