using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.Logger
{
    /// <summary>
    /// 消息格式化处理函数
    /// </summary>
    /// <param name="format">格式化字符串</param>
    /// <param name="args">参数</param>
    /// <returns></returns>
    public delegate string FormatMessageHandler(string format, params object[] args);
}
