using Magicodes.Utility;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Magicodes.Core.Config
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public class ConfigManager : ConfigManagerBase
    {
        LoggerStrategyBase logger = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        private static object _locker = new object();//锁对象
        public override T LoadConfig<T>()
        {
            lock (_locker)
            {
                var path = Path.Combine(GlobalApplicationObject.Current.ApplicationContext.SitePaths.SiteConfigDirPath, typeof(T).Name + ".config");
                if (!File.Exists(path))
                {
                    if (logger != null) logger.LogFormat(LoggerLevels.Warn, "配置文件【{0}】不存在，无法加载配置【{1}】！", path, typeof(T).FullName);
                    SerializeHelper.Save(typeof(T).Assembly.CreateInstance(typeof(T).FullName), path + ".demo.xml");
                    return default(T);
                }
                SetMonitor<T>(path);

                return SerializeHelper.Load<T>(path);
            }
        }
        //设置监视器
        public void SetMonitor<T>(string path) where T : ConfigBase
        {
            if (!File.Exists(path))
                return;
            try
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.0);
                //监视文件需要传入一个IList对象，所以即便只有一个文件也需要新建List对象
                var filePaths = new List<string>() { path };
                //新建一个文件监视器对象，添加对资源文件的监视
                var monitor = new HostFileChangeMonitor(filePaths);
                //调用监视器的NotifyOnChanged方法传入发生改变时的回调方法
                monitor.NotifyOnChanged(
                    new OnChangedCallback(state =>
                    {
                        var config = this.LoadConfig<T>();
                        if (config != null)
                        {
                            Configs.AddOrUpdate(typeof(T), config, (tKey, existingVal) => { return config; });
                            logger.Log(LoggerLevels.Info, "配置已更新，路径：" + path);
                        }
                    })
                );
                policy.ChangeMonitors.Add(monitor);
            }
            catch (Exception ex)
            {
                logger.Log(LoggerLevels.Error, "设置监视器出错，路径：" + path, ex);
            }
        }

        public override void SaveConfig<T>(T configType)
        {
            lock (_locker)
            {
                var path = Path.Combine(GlobalApplicationObject.Current.ApplicationContext.SitePaths.SiteConfigDirPath, typeof(T).Name + ".config");
                configType.UpdateTime = DateTime.Now;
                SerializeHelper.Save(configType, path);
                //Configs.AddOrUpdate(typeof(T), configType, (tKey, existingVal) => { return configType; });
            }
        }

        public override T GetConfig<T>()
        {
            lock (_locker)
            {
                var type = typeof(T);
                if (Configs.ContainsKey(type))
                {
                    return Configs[type] as T;
                }
                else
                {
                    var config = LoadConfig<T>();
                    if (config != null)
                    {
                        Configs.AddOrUpdate(type, config, (tKey, existingVal) => { return config; });
                        return config;
                    }
                }
            }
            return default(T);
        }

        public override T GetConfigWhenSuccess<T>(Action<T> actionWhenSuccess)
        {
            var configInfo = GetConfig<T>();
            if (configInfo != null)
            {
                actionWhenSuccess.Invoke(configInfo);
            }
            return configInfo;
        }

        public override T GetConfigWhenComplete<T>(Action<T> actionWhenComplete)
        {
            var configInfo = GetConfig<T>();
            actionWhenComplete.Invoke(configInfo);
            return configInfo;
        }
    }
}
