using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Xml.Linq;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Plus.Info;
using Magicodes.Web.Interfaces;

namespace Magicodes.Core.Plus
{
    /// <summary>
    /// 程序集管理
    /// </summary>
    public class AssemblyManager
    {
        /// <summary>
        /// 获取程序集最后写入时间
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static long GetAssemblyTicks(Assembly assembly)
        {
            return GetAssemblyTime(assembly).Ticks;
        }
        /// <summary>
        /// 获取程序集最后写入时间
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        public static DateTime GetAssemblyTime(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            return File.GetLastWriteTime(new Uri(assemblyName.CodeBase).LocalPath);
        }
        /// <summary>
        /// 获取文件写入毫秒数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        public static long GetFileWriteTicks(string path)
        {
            return File.GetLastWriteTime(path).Ticks;
        }
        /// <summary>
        /// 获取程序集属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(ICustomAttributeProvider assembly, bool inherit = false)
 where T : Attribute
        {
            return assembly
                .GetCustomAttributes(typeof(T), inherit)
                .OfType<T>()
                .FirstOrDefault();
        }
        /// <summary>
        /// 获取当前程序集信息
        /// </summary>
        /// <param name="pluAssembly"></param>
        /// <returns></returns>
        public static IPlusAssemblyInfo GetPlusAssemblysInfo(Assembly pluAssembly, FileInfo dllFile)
        {
            var plus = pluAssembly.GetName();
            var descriptionAttr = GetAttribute<AssemblyDescriptionAttribute>(pluAssembly, false);
            var titleAttr = GetAttribute<AssemblyTitleAttribute>(pluAssembly, false);
            var guidAttr = GetAttribute<GuidAttribute>(pluAssembly, false);
            var companyAttr = GetAttribute<AssemblyCompanyAttribute>(pluAssembly, false);
            var configurationAttr = GetAttribute<AssemblyConfigurationAttribute>(pluAssembly, false);
            var copyrightAttr = GetAttribute<AssemblyCopyrightAttribute>(pluAssembly, false);
            var cultureAttr = GetAttribute<AssemblyCultureAttribute>(pluAssembly, false);
            var productAttr = GetAttribute<AssemblyProductAttribute>(pluAssembly, false);
            var trademarkAttr = GetAttribute<AssemblyTrademarkAttribute>(pluAssembly, false);
            {
                var plusAss = new PlusAssemblyInfo()
                {
                    Id = guidAttr == null ? Guid.NewGuid() : Guid.Parse(guidAttr.Value),
                    Description = descriptionAttr == null ? string.Empty : descriptionAttr.Description,
                    FullName = pluAssembly.FullName,
                    Name = plus.Name,
                    Title = titleAttr == null ? string.Empty : titleAttr.Title,
                    UpdateTime = GetAssemblyTime(pluAssembly),
                    Version = string.Format("{0}.{1}.{2}.{3}", plus.Version.Major, plus.Version.Minor, plus.Version.Build, plus.Version.Revision),
                    Company = companyAttr == null ? string.Empty : companyAttr.Company,
                    Configuration = configurationAttr == null ? string.Empty : configurationAttr.Configuration,
                    Copyright = copyrightAttr == null ? string.Empty : copyrightAttr.Copyright,
                    Culture = cultureAttr == null ? string.Empty : cultureAttr.Culture,
                    Product = productAttr == null ? string.Empty : productAttr.Product,
                    Trademark = trademarkAttr == null ? string.Empty : trademarkAttr.Trademark
                };
                string configContent = null;
                //获取插件目录的配置文件
                var configPath = Path.Combine(dllFile.Directory.Parent.FullName, "Plus.config");
                if (File.Exists(configPath))
                    configContent = File.ReadAllText(configPath);
                else
                    configContent = GlobalApplicationObject.Current.ApplicationContext.ManifestResourceManager.GetWebResourceAsString(pluAssembly, string.Format("{0}.Plus.config", plus.Name));

                if (configContent.IsEmpty())
                {
                    throw new MagicodesException("程序集" + pluAssembly.FullName + "并不包含Plus.Config（注意区分大小写）文件，系统无法解析。请检查该插件是否存在Plus.Config（允许以嵌入式的资源存在）。");
                }
                else
                {
                    plusAss.PlusConfigInfo = SerializeHelper.LoadFromStr<PlusConfigInfo>(configContent);
                }
                return plusAss;
            }
        }
    }
}
