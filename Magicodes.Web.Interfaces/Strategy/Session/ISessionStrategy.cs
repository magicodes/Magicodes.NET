using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.Session
{
    /// <summary>
    /// 会话状态策略接口
    /// </summary>
    public interface ISessionStrategy : IStrategyBase
    {
        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        object Get(string key);
        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        T Get<T>(string key);

        /// <summary>
        /// 移除用户会话状态数据
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 设置用户会话状态数据的数据项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Set(string key, object value);

    }
}
