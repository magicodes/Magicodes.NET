using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.T4.Extensions;
using Magicodes.T4.Models;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :FormHelper
//        description :
//
//        created by 雪雁 at  2015/1/14 15:23:49
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4
{
    /// <summary>
    /// 表单辅助类
    /// </summary>
    public class FormHelper
    {
        /// <summary>
        /// 是否启用表单组
        /// </summary>
        public bool IsEnableFormGroup { get; set; }
        /// <summary>
        /// 组集合
        /// </summary>
        public List<String> Groups { get; set; }
        /// <summary>
        /// 字段类型模板
        /// </summary>
        public Dictionary<T4DataType, string> FieldTypeTemplates { get; set; }
        /// <summary>
        /// 默认模板
        /// </summary>
        public string DefaultHtmlTemplate { get; set; }
        public string DefaultGroupName { get; set; }

        /// <summary>
        /// 标识，表示表单类型。比如add、edit
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 表单生成模型类型
        /// </summary>
        public Type FormModelType { get; set; }
        public IEnumerable<T4.Models.T4PropertyInfo> T4PropertyInfos { get; set; }

        public FormHelper(Type formModelType, Dictionary<T4DataType, string> fieldTypeTemplates, string defaultHtmlTemplate, string tag)
        {
            this.FormModelType = formModelType;
            this.FieldTypeTemplates = fieldTypeTemplates;
            this.DefaultHtmlTemplate = defaultHtmlTemplate;
            this.Tag = tag;
            //组特性
            var t4FormGroupAttribute = formModelType.GetAttribute<T4FormGroupAttribute>(false);
            if (t4FormGroupAttribute != null)
                this.IsEnableFormGroup = true;

            this.T4PropertyInfos = formModelType.GetT4PropertyInfos();
            if (this.IsEnableFormGroup)
            {
                this.DefaultGroupName = t4FormGroupAttribute.GroupName;
                this.Groups = this.T4PropertyInfos.Where(p => p.T4GroupInfo != null).Select(p => p.T4GroupInfo.Name).Distinct().ToList();
                if (!this.Groups.Contains(t4FormGroupAttribute.GroupName)) this.Groups.Add(t4FormGroupAttribute.GroupName);
            }
        }
        public string T4Html(string groupName)
        {
            var fields = this.T4PropertyInfos.Where(p => p.T4GroupInfo != null && p.T4GroupInfo.Name == groupName);
            var sb = new StringBuilder();
            foreach (var item in fields)
            {
                sb.Append(T4FieldHtml(item));
            }
            return sb.ToString();
        }
        public string T4Html()
        {
            var fields = this.T4PropertyInfos;
            var sb = new StringBuilder();
            foreach (var item in fields)
            {
                sb.Append(T4FieldHtml(item));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public string T4FieldHtml(T4PropertyInfo t4ProInfo)
        {
            t4ProInfo.Tag = this.Tag;
            var dic = this.FieldTypeTemplates;
            var defaultHtmlTemplate = this.DefaultHtmlTemplate;

            //判断是否只读
            t4ProInfo.ReadOnly =
                (t4ProInfo._ReadOnly && t4ProInfo._ReadOnlyType == ReadOnlyTypes.All)
                ||
                (t4ProInfo.Tag.Equals("add", StringComparison.CurrentCultureIgnoreCase) && t4ProInfo._ReadOnly && (t4ProInfo._ReadOnlyType == ReadOnlyTypes.Add))
                ||
                (t4ProInfo.Tag.Equals("edit", StringComparison.CurrentCultureIgnoreCase) && t4ProInfo._ReadOnly && (t4ProInfo._ReadOnlyType == ReadOnlyTypes.Edit));

            if (t4ProInfo.Ignore || t4ProInfo.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)) return null;
            var temp = dic.ContainsKey(t4ProInfo.DataType) ? dic[t4ProInfo.DataType] : defaultHtmlTemplate;
            if (!string.IsNullOrEmpty(temp))
            {
                var str = temp;
                foreach (var proInfo in t4ProInfo.GetType().GetProperties())
                {
                    var name = "{" + proInfo.Name + "}";
                    var value = (proInfo.GetValue(t4ProInfo) ?? string.Empty).ToString();
                    //bool类型小写
                    if (proInfo.PropertyType == typeof(bool))
                        value = value.ToLower();
                    str = str.Replace(name, value);
                }
                return str;
            }
            return string.Empty;
        }
    }
}
