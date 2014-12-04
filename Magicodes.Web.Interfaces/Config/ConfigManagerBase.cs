using Magicodes.Web.Interfaces.Config.Info;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Magicodes.Web.Interfaces.Config
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public abstract class ConfigManagerBase
    {
        protected ConcurrentDictionary<Type, ConfigBase> Configs = new ConcurrentDictionary<Type, ConfigBase>();

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns></returns>
        public abstract T LoadConfig<T>() where T : ConfigBase;
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public abstract void SaveConfig<T>(T configType) where T : ConfigBase;

        /// <summary>
        /// 获取策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T GetConfig<T>() where T : ConfigBase;
        /// <summary>
        /// 获取策略，如果获取成功，则执行成功函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T GetConfigWhenSuccess<T>(Action<T> actionWhenSuccess) where T : ConfigBase;
        /// <summary>
        /// 获取策略，获取完成后执行指定函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T GetConfigWhenComplete<T>(Action<T> actionWhenComplete) where T : ConfigBase;
    }
}
