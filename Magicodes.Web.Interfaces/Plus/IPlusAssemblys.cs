using Magicodes.Web.Interfaces.Plus.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus
{
    public interface IPlusAssemblyInfo
    {
        /// <summary>
        /// 程序集Guid
        /// </summary>
        System.Guid Id { get; set; }
        /// <summary>
        /// 程序集全名
        /// </summary>
        string FullName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        System.DateTime UpdateTime { get; set; }
        /// <summary>
        /// 程序集版本
        /// </summary>
        string Version { get; set; }
        /// <summary>
        /// 短命
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 程序集说明标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 程序集描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 程序集生成配置
        /// </summary>
        string Configuration { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        string Company { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        string Product { get; set; }
        /// <summary>
        /// 版权信息
        /// </summary>
        string Copyright { get; set; }
        /// <summary>
        /// 商标
        /// </summary>
        string Trademark { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        string Culture { get; set; }
        /// <summary>
        /// 插件配置信息
        /// </summary>
        PlusConfigInfo PlusConfigInfo { get; set; }
        /// <summary>
        /// 是否已经安装
        /// </summary>
        bool IsInstalled { get; set; }

    }
}
