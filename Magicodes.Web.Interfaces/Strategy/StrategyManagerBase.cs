using Magicodes.Web.Interfaces.Strategy.Cache;
using Magicodes.Web.Interfaces.Strategy.Email;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.Strategy.Session;
using Magicodes.Web.Interfaces.Strategy.SMS;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Magicodes.Web.Interfaces.Strategy
{
    /// <summary>
    /// 策略管理
    /// </summary>
    public abstract class StrategyManagerBase
    {
        /// <summary>
        /// 策略字典
        /// </summary>
        static private Lazy<ConcurrentDictionary<string, List<IStrategyBase>>> strategyDictionary = new Lazy<ConcurrentDictionary<string, List<IStrategyBase>>>(() =>
        {
            return new ConcurrentDictionary<string, List<IStrategyBase>>();
        }, LazyThreadSafetyMode.ExecutionAndPublication);
        /// <summary>
        /// 策略字典
        /// </summary>
        public ConcurrentDictionary<string, List<IStrategyBase>> StrategyDictionary { get { return strategyDictionary.Value; } }
        /// <summary>
        /// 添加策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract T AddStrategy<T>(T t) where T : IStrategyBase;
        /// <summary>
        /// 添加策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract T AddStrategy<T>(string key, T t) where T : IStrategyBase;
        /// <summary>
        /// 添加策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract T AddDefaultStrategy<T>(string key, T t) where T : IStrategyBase;
        /// <summary>
        /// 获取策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T GetDefaultStrategy<T>() where T : IStrategyBase;
        /// <summary>
        /// 获取策略
        /// </summary>
        /// <typeparam name="T">策略类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public abstract T GetDefaultStrategy<T>(string key) where T : IStrategyBase;
        /// <summary>
        /// 获取策略集合
        /// </summary>
        /// <typeparam name="T">策略类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public abstract List<T> GetStrategys<T>(string key) where T : IStrategyBase;
        /// <summary>
        /// 获取策略集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract List<T> GetStrategys<T>() where T : IStrategyBase;
    }
}
