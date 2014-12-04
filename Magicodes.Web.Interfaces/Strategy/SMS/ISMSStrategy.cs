using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.SMS
{
    /// <summary>
    /// 短信策略接口
    /// </summary>
    public interface ISMSStrategy : IStrategyBase
    {
        /// <summary>
        /// 短信服务器地址
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// 短信账号
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 短信密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="to">接收人号码</param>
        /// <param name="body">短信内容</param>
        /// <returns>是否发送成功</returns>
        bool Send(string to, string body);
    }
}
