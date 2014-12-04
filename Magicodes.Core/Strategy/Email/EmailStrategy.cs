using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config.Info;
using Magicodes.Web.Interfaces.Strategy.Email;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :EmailStrategy
//        description :
//
//        created by 雪雁 at  2014/10/22 13:57:45
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Strategy.Email
{
    /// <summary>
    /// 框架默认的邮件策略
    /// </summary>
    class EmailStrategy : IMailStrategy
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件信息</param>
        /// <returns></returns>
        public Task SendAsync(MailInfo message)
        {
            var mailConfig = GlobalApplicationObject.Current.ApplicationContext.ConfigManager.GetConfig<MailConfigInfo>();
            if (mailConfig == null)
            {
                GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.Log(LoggerLevels.Warn, string.Format("没有找到邮箱配置，邮件【{0}】无法发送！", message.Subject));
                return Task.FromResult(false);
            }
            try
            {
                var mail = new MailMessage()
                    {
                        From = new MailAddress(mailConfig.MailFrom, mailConfig.FromNickName),
                        To =
                        {
                            new MailAddress(message.Destination)
                        },
                        Subject = message.Subject,
                        Body = message.Body,
                        BodyEncoding = System.Text.Encoding.UTF8,
                        SubjectEncoding = System.Text.Encoding.UTF8,
                        IsBodyHtml = true,
                        Priority = MailPriority.High,
                    };
                var mailClient = new SmtpClient()
                {
                    Host = mailConfig.SmtpServer,
                    Port = mailConfig.SmtpPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(userName: mailConfig.UserName, password: mailConfig.Password),
                    EnableSsl = mailConfig.EnableSsl,
                };
                mailClient.Send(mail);
            }
            catch (Exception ex)
            {
                GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.Log(LoggerLevels.Error, string.Format("发送邮件失败【{0}】！", message.Destination), ex);
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public void Initialize()
        {
            
        }
    }
}
