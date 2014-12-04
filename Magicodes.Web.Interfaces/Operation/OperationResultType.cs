using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Operation
{
    /// <summary>
    ///     表示业务操作结果的枚举
    /// </summary>
    public enum OperationResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [Description("操作成功。")]
        Success = 0,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged = 1,

        /// <summary>
        ///     参数错误
        /// </summary>
        [Description("参数错误。")]
        ParamError = 2,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull = 3,

        /// <summary>
        ///     权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")]
        PurviewLack = 4,

        /// <summary>
        ///     非法操作
        /// </summary>
        [Description("非法操作。")]
        IllegalOperation = 5,

        /// <summary>
        ///     警告
        /// </summary>
        [Description("警告")]
        Warning = 6,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error = 7,
        /// <summary>
        ///     操作尚未支持
        /// </summary>
        [Description("操作尚未实现。")]
        NoImplemented = 8,
    }
}
