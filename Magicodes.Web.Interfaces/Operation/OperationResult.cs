using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Operation
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        ///     获取或设置 操作结果类型
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        ///     获取或设置 操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     获取或设置 操作返回的日志消息，用于记录日志
        /// </summary>
        public string LogMessage { get; set; }
        /// <summary>
        ///     获取或设置  操作返回的结果数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///     获取或设置 操作结果附加信息
        /// </summary>
        public dynamic AppendData { get; set; }
    }
}
