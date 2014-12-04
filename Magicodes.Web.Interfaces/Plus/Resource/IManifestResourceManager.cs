using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus.Resource
{
    /// <summary>
    /// 程序集资源管理
    /// </summary>
    public interface IManifestResourceManager
    {
        /// <summary>
        /// 程序集资源字典
        /// </summary>
        ConcurrentDictionary<string, IResourceUrl> ResourcesDic { get; set; }
        /// <summary>
        /// 设置资源字典
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resourceUrl"></param>
        void AddResourcesDic(string key, IResourceUrl resourceUrl);

        /// <summary>
        /// 设置资源字典
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resourceUrl"></param>
        /// <param name="isAlias">是否为别名</param>
        void AddResourcesDic(string key, IResourceUrl resourceUrl, bool isAlias);
        /// <summary>
        /// 获取嵌入式资源流
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        Stream GetManifestResourceStream(Assembly assembly, string resourceName);
        /// <summary>
        /// 获取嵌入资源内容
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetWebResourceAsString(Assembly assembly, string resourceName);
        /// <summary>
        /// 获取资源流（资源名不分大小写）
        /// </summary>
        /// <param name="resourceName">资源名，比如：Magicodes.XXX.Scripts.jquery-1.10.1.min.js</param>
        /// <returns></returns>
        Stream GetManifestResourceStream(string resourceName);
        /// <summary>
        /// 获取资源流（资源名不分大小写）
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        Stream GetManifestResourceStream(IResourceUrl resourceUrl);
        /// <summary>
        /// 根据资源路径获取程序集
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        Assembly GetAssemblyByResourceUrl(IResourceUrl resourceUrl);
        /// <summary>
        /// 获取资源路径
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        IResourceUrl GetResourceUrlByResourceName(string resourceName);
        /// <summary>
        /// 获取嵌入资源内容
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetWebResourceAsString(string resourceName);
    }
}
