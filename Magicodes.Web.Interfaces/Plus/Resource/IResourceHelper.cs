using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus.Resource
{
    /// <summary>
    /// 程序集资源辅助类
    /// </summary>
    public interface IResourceHelper
    {
        /// <summary>
        /// 加载程序集并且处理程序集资源
        /// </summary>
        /// <param name="pluAssembly"></param>
        IPlusAssemblyInfo LoadPlusAndPlusResource(IPlusInfo plusInfo);
         /// <summary>
        /// 程序集初始化（处理Workflow、资源、代码、主题）
        /// </summary>
        /// <param name="pluAssembly"></param>
        /// <param name="fwAss"></param>
        void AssemblyInitialize(Assembly pluAssembly, IPlusAssemblyInfo fwAss);
         /// <summary>
        /// 加载程序集资源
        /// </summary>
        /// <param name="pluAssembly"></param>
        /// <param name="isWriteResource"></param>
        /// <param name="fwAss"></param>
        void LoadAssemblyResources(Assembly pluAssembly, bool isWriteResource, IPlusAssemblyInfo fwAss);
        /// <summary>
        /// 检查是否已将资源写入站点目录
        /// </summary>
        /// <param name="assDirPath"></param>
        /// <param name="fwAss"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        bool CheckHasWrittenToSiteDir(string assDirPath, IPlusAssemblyInfo fwAss, ref IResourceUrl resourceUrl);
         /// <summary>
        /// 将文件输出到站点目录
        /// </summary>
        /// <param name="assDirPath"></param>
        /// <param name="pluAssembly"></param>
        /// <param name="resourceUrl"></param>
        /// <param name="fwAss"></param>
        void WriteResourceToSiteDir(string assDirPath, Assembly pluAssembly, ref IResourceUrl resourceUrl, IPlusAssemblyInfo fwAss);
    }
}
