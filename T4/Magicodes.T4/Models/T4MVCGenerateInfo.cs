using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.T4;
using Magicodes.T4.Extensions;
namespace Magicodes.T4.Models
{
    //======================================================================
    //
    //        Copyright (C) 2014-2016 Magicodes.NET团队    
    //        All rights reserved
    //
    //        filename :T4MVCGenerateInfo
    //        description :
    //
    //        created by 雪雁 at  2015/1/25 15:12:55
    //        http://www.magicodes.net
    //
    //======================================================================
    public class T4MVCGenerateInfo<TModel, TDbContext>
        where TModel : class
        where TDbContext : DbContext
    {
        public T4MVCGenerateInfo()
        {
            //设置必须的命名空间
            RequiredNamespaces = new List<string>() { typeof(TDbContext).Namespace };
            ControllerRootName = string.Empty;
            IsOverpostingProtectionRequired = true;
            ContextTypeName = typeof(TDbContext).Name;
            var tmodel=typeof(TModel);
            ModelTypeName = tmodel.Name;
            ModelTypeFullName = tmodel.FullName;
            var modelNamespace = typeof(TModel).Namespace;
            //设置模型的命名空间
            if (!RequiredNamespaces.Contains(modelNamespace))
                RequiredNamespaces.Add(modelNamespace);
            var db = (DbContext)Activator.CreateInstance(typeof(TDbContext),
                "Data Source=.;Initial Catalog=Magicodes.Models.Mvc;Integrated Security=True");
            //this.EntitySetName=EntityHelper.GetEntitySetName<TModel>(db);

            this.EntitySetName = db.GetEntitySetName<TModel>();
            var keyTypes = db.GetKeysTypes<TModel>();
            this.PrimaryKeyName = db.GetKeyNames<TModel>().First();
            this.PrimaryKeyType = keyTypes.First().FullName;
            this.PrimaryKeyShortType = keyTypes.First().Name;
            this.Title = typeof(TModel).GetDisplayName();
            this.Propertys = typeof(TModel).GetT4PropertyInfos();
        }

        public TModel Model { get; set; }
        public TDbContext DbContext { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 控制器根名称
        /// </summary>
        public string ControllerRootName { get; set; }
        /// <summary>
        /// 是否异步
        /// </summary>
        public bool UseAsync { get; set; }
        /// <summary>
        /// 必须的命名空间
        /// </summary>
        public List<string> RequiredNamespaces { get; set; }
        /// <summary>
        /// 当前命名空间
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 当前DbContext
        /// </summary>
        public string ContextTypeName { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntitySetName { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PrimaryKeyName { get; set; }
        /// <summary>
        /// 主键类型简称
        /// </summary>
        public string PrimaryKeyShortType { get; set; }
        /// <summary>
        /// 主键类型
        /// </summary>
        public string PrimaryKeyType { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public List<Object> Params { get; set; }
        /// <summary>
        /// 模型类型名称
        /// </summary>
        public string ModelTypeName { get; set; }
        public string ModelTypeFullName { get; set; }
        public bool IsOverpostingProtectionRequired { get; set; }
        public bool IsAdminController { get; set; }
        public bool IsPartialView { get; set; }
        public string LayoutPageFile { get; set; }
        public string Title { get; set; }
        public bool UseLayoutPage { get; set; }
        public IEnumerable<T4PropertyInfo> Propertys { get; set; }
    }
}
