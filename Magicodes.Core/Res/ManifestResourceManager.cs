using Magicodes.Core.Performance.Watch;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces.Plus.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Strategy.Logger;
namespace Magicodes.Core.Res
{
    /// <summary>
    /// 程序集资源管理
    /// </summary>
    public class ManifestResourceManager : IManifestResourceManager
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public List<IPlusInfo> PlusAssemblysList
        {
            get
            {
                return GlobalApplicationObject.Current.ApplicationContext.PlusAssemblysList;
            }
        }
        static readonly LoggerStrategyBase Log = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        public System.Collections.Concurrent.ConcurrentDictionary<string, IResourceUrl> ResourcesDic { get; set; }

        public void AddResourcesDic(string key, IResourceUrl resourceUrl)
        {
            var resource = ResourcesDic;
            resource.AddOrUpdate(key, resourceUrl, (tKey, existingVal) => resourceUrl);
        }

        public void AddResourcesDic(string key, IResourceUrl resourceUrl, bool isAlias)
        {
            var resource = ResourcesDic;
            resourceUrl.IsAlias = isAlias;
            resource.AddOrUpdate(key, resourceUrl, (tKey, existingVal) => resourceUrl);
        }

        public System.IO.Stream GetManifestResourceStream(System.Reflection.Assembly assembly, string resourceName)
        {
            var name = resourceName;
            using (new CodeWatch("GetManifestResourceStream", 1000, new Action<string, LoggerStrategyBase, int?, long>((tag, currentLog, wcount, execms) => currentLog.LogFormat(LoggerLevels.Warn, "\t{0}:资源({3})请求时间为({1})ms.已超过阀值（{2}）ms.", tag, execms, wcount, name))))
            {
                return assembly.GetManifestResourceStream(resourceName);
            }
        }

        public string GetWebResourceAsString(System.Reflection.Assembly assembly, string resourceName)
        {
            string content;
            if (assembly == null) return null;
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return null;
            using (var reader = new System.IO.StreamReader(stream))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }

        public System.IO.Stream GetManifestResourceStream(string resourceName)
        {
            var resourceUrl = GetResourceUrlByResourceName(resourceName);
            return resourceUrl == null ? null : GetManifestResourceStream(resourceUrl);
        }

        public System.IO.Stream GetManifestResourceStream(IResourceUrl resourceUrl)
        {
            using (var wacth = new CodeWatch("GetManifestResourceStream", 1000, new Action<string, LoggerStrategyBase, int?, long>((tag, currentLog, wcount, execms) => currentLog.LogFormat(LoggerLevels.Warn, "\t{0}:资源({3})请求时间为({1})ms.已超过阀值（{2}）ms.", tag, execms, wcount, resourceUrl.ManifestResourceName))))
            {
                if (!PlusAssemblysList.Any(p => p.Assembly.FullName == resourceUrl.AssemblyFullName))
                    throw new Exception("此插件已移除或者不存在。");
                var ass = PlusAssemblysList.First(p => p.Assembly.FullName == resourceUrl.AssemblyFullName);
                return ass.Assembly.GetManifestResourceStream(resourceUrl.ManifestResourceName);
            }
        }

        public System.Reflection.Assembly GetAssemblyByResourceUrl(IResourceUrl resourceUrl)
        {
            return PlusAssemblysList.First(p => p.Assembly.FullName == resourceUrl.AssemblyFullName).Assembly;
        }

        public IResourceUrl GetResourceUrlByResourceName(string resourceName)
        {
            var resourcesDic = ResourcesDic;
            resourceName = resourceName.ToLower();
            if (resourceName.IsEmpty() || !resourcesDic.ContainsKey(resourceName))
                return null;
            var resourceUrl = resourcesDic[resourceName];
            return resourceUrl;
        }

        public string GetWebResourceAsString(string resourceName)
        {
            var resourceUrl = GetResourceUrlByResourceName(resourceName);
            if (resourceUrl == null)
            {
                throw new Exception("获取资源失败：" + resourceName);
            }
            var assembly = GetAssemblyByResourceUrl(resourceUrl);

            return GetWebResourceAsString(assembly, resourceUrl.ManifestResourceName);
        }




    }
}
