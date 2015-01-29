using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Magicodes.T4.Models;
using Magicodes.Web.Interfaces.T4;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :T4Extensions
//        description :
//
//        created by 雪雁 at  2015/1/7 10:21:38
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4.Extensions
{
    public static class T4Extensions
    {
        /// <summary>
        /// 获取显示名
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDisplayName(this ICustomAttributeProvider customAttributeProvider, bool inherit = false)
        {
            string displayName = null;
            var displayAttribute = customAttributeProvider.GetAttribute<DisplayAttribute>(false);
            if (displayAttribute != null) displayName = displayAttribute.Name;
            else
            {
                var displayNameAttribute = customAttributeProvider.GetAttribute<DisplayNameAttribute>(false);
                if (displayNameAttribute != null)
                    displayName = displayNameAttribute.DisplayName;
            }
            return displayName;

        }
        /// <summary>
        /// 获取T4生成特性
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static T4PropertyInfo GetT4PropertyInfo(this PropertyInfo pro)
        {
            var t4ProInfo = new T4PropertyInfo()
            {
                Name = pro.Name,
                Ignore = pro.GetAttribute<T4GenerationIgnoreAttribute>(false) != null,
                DataType = pro.GetT4DataType()
            };

            //显示名
            var displayAttribute = pro.GetAttribute<DisplayAttribute>(false);
            t4ProInfo.DisplayName = displayAttribute == null ? null : displayAttribute.Name;
            //是否必填
            var requiredAttribute = pro.GetAttribute<RequiredAttribute>(false);
            t4ProInfo.Required = requiredAttribute != null;

            //字符串长度
            var StringLengthAttribute = pro.GetAttribute<StringLengthAttribute>(false);
            t4ProInfo.MaxLength = StringLengthAttribute == null ? (int?)null : StringLengthAttribute.MaximumLength;

            //描述
            var descriptionAttribute = pro.GetAttribute<DescriptionAttribute>(false);
            t4ProInfo.Description = descriptionAttribute == null ? null : descriptionAttribute.Description;

            //只读
            var t4ReadOnlyFieldAttribute = pro.GetAttribute<T4ReadOnlyFieldAttribute>(false);
            t4ProInfo._ReadOnly = t4ReadOnlyFieldAttribute != null;
            if (t4ProInfo._ReadOnly)
                t4ProInfo._ReadOnlyType = t4ReadOnlyFieldAttribute.ReadOnlyType;
            //组特性
            var t4FormGroupAttribute = pro.GetAttribute<T4FormGroupAttribute>(false);
            if (t4FormGroupAttribute != null)
            {
                t4ProInfo.T4GroupInfo = new T4.Models.T4GroupInfo()
                {
                    Name = t4FormGroupAttribute.GroupName
                };
            }

            var t4SelectAttribute = pro.GetAttribute<T4SelectAttribute>(false);
            if (t4SelectAttribute != null)
                t4ProInfo.T4Select = t4SelectAttribute;
            return t4ProInfo;
        }
        /// <summary>
        /// 获取所有的属性特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<T4PropertyInfo> GetT4PropertyInfos(this Type type)
        {
            foreach (PropertyInfo pro in type.GetProperties())
            {
                yield return pro.GetT4PropertyInfo();
            }
        }
        /// <summary>
        /// 获取所有的HTML属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<T4PropertyInfo> GetHtmlT4PropertyInfo(this Type type)
        {
            return type.GetT4PropertyInfos().Where(p => p.DataType == T4DataType.Html);
        }
        /// <summary>
        /// 获取程序集属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider assembly, bool inherit = false)
 where T : Attribute
        {

            return assembly
                .GetCustomAttributes(typeof(T), inherit)
                .OfType<T>()
                .FirstOrDefault();
        }
        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this ICustomAttributeProvider assembly, bool inherit = false) where T : Attribute
        {
            return assembly.GetCustomAttributes(typeof(T), inherit).Any(m => (m as T) != null);
        }
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static T4DataType GetT4DataType(this PropertyInfo pro)
        {
            var proType = pro.PropertyType;
            //属性名
            var proName = pro.Name;
            //显示名
            var displayAttribute = GetAttribute<DisplayAttribute>(pro, false);
            var displayName = displayAttribute == null ? "" : displayAttribute.Name;
            //是否必填
            var requiredAttribute = GetAttribute<RequiredAttribute>(pro, false);
            var required = requiredAttribute != null;

            //字符串长度，大于50会生成TextArea，最大值默认4000
            var stringLengthAttribute = GetAttribute<StringLengthAttribute>(pro, false);
            var maxLength = stringLengthAttribute == null ? (int?)null : stringLengthAttribute.MaximumLength;
            if (maxLength == null)
            {
                var maxLengthAttribute = GetAttribute<MaxLengthAttribute>(pro, false);
                if (maxLengthAttribute != null) maxLength = maxLengthAttribute.Length;
            }
            //是否邮箱地址
            var EmailAddressAttribute = GetAttribute<EmailAddressAttribute>(pro, false);
            var isEmail = EmailAddressAttribute != null;

            T4DataType? dataType = null;
            if (GetAttribute<T4SelectAttribute>(pro, false) != null)
            {
                dataType = T4DataType.Select;
                return dataType.Value;
            }

            //数据类型
            var dt = GetAttribute<DataTypeAttribute>(pro, false);
            if (dt != null)
            {
                switch (dt.DataType)
                {
                    case DataType.CreditCard:
                        dataType = T4DataType.CreditCard;
                        break;
                    case DataType.Currency:
                        dataType = T4DataType.Currency;
                        break;
                    case DataType.Custom:
                        dataType = T4DataType.Custom;
                        break;
                    case DataType.Date:
                        dataType = T4DataType.Date;
                        break;
                    case DataType.DateTime:
                        dataType = T4DataType.DateTime;
                        break;
                    case DataType.Duration:
                        dataType = T4DataType.Duration;
                        break;
                    case DataType.EmailAddress:
                        dataType = T4DataType.EmailAddress;
                        break;
                    case DataType.Html:
                        dataType = T4DataType.Html;
                        break;
                    case DataType.ImageUrl:
                        dataType = T4DataType.ImageUrl;
                        break;
                    case DataType.MultilineText:
                        dataType = T4DataType.MultilineText;
                        break;
                    case DataType.Password:
                        dataType = T4DataType.Password;
                        break;
                    case DataType.PhoneNumber:
                        dataType = T4DataType.PhoneNumber;
                        break;
                    case DataType.PostalCode:
                        dataType = T4DataType.PostalCode;
                        break;
                    case DataType.Text:
                        dataType = T4DataType.Text;
                        break;
                    case DataType.Time:
                        dataType = T4DataType.Time;
                        break;
                    case DataType.Upload:
                        dataType = T4DataType.Upload;
                        break;
                    case DataType.Url:
                        dataType = T4DataType.Url;
                        break;
                    default:
                        break;
                }
            }
            if (isEmail) dataType = T4DataType.EmailAddress;

            if (dataType == null)
            {
                if (proType == typeof(String))
                {
                    if (maxLength == null || maxLength.Value > 50)
                        dataType = T4DataType.MultilineText;
                    else
                        dataType = T4DataType.Text;
                }
                else if (proType == typeof(Boolean))
                    dataType = T4DataType.Bit;
                else if (proType == typeof(Int32) || proType == typeof(Int64))
                    dataType = T4DataType.Integer;
            }
            if (dataType == null)
                dataType = T4DataType.Text;
            return dataType.Value;
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string T4Html(this PropertyInfo pro, Dictionary<T4DataType, string> dic, string defaultHtmlTemplate)
        {
            return pro.T4Html(dic, defaultHtmlTemplate, "default");
        }
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string T4Html(this PropertyInfo pro, Dictionary<T4DataType, string> dic, string defaultHtmlTemplate, string tag)
        {
            var t4ProInfo = pro.GetT4PropertyInfo();
            t4ProInfo.Tag = tag;
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
        public static string T4Html(this Type type, Dictionary<T4DataType, string> dic, string defaultHtmlTemplate)
        {
            return type.T4Html(dic, defaultHtmlTemplate, "default");
        }
        public static string T4Html(this Type type, Dictionary<T4DataType, string> dic, string defaultHtmlTemplate, string tag)
        {
            if (type == null || dic == null) return string.Empty;
            var str = new StringBuilder();
            foreach (PropertyInfo pro in type.GetProperties())
            {
                var html = pro.T4Html(dic, defaultHtmlTemplate, tag);
                if (!string.IsNullOrEmpty(html))
                    str.AppendLine(html);
            }
            return str.ToString();
        }
        /// <summary>
        /// 获取当前程序集中应用此特性的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesWith<TAttribute>(this Assembly assembly, bool inherit) where TAttribute : System.Attribute
        {
            var attrType = typeof(TAttribute);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attrType, true).Length > 0)
                {
                    yield return type;
                }
            }
            //return from t in assembly.GetTypes()
            //       where t.IsDefined(type, inherit)
            //       select t;
        }
    }
}
