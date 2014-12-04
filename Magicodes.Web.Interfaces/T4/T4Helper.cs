using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :T4Helper
//        description :
//
//        created by 雪雁 at  2014/10/27 21:40:53
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.T4
{
    public class T4Helper
    {
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
        /// 
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static T4DataType GetT4DataType(PropertyInfo pro)
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
            var StringLengthAttribute = GetAttribute<StringLengthAttribute>(pro, false);
            var maxLength = StringLengthAttribute == null ? 4000 : StringLengthAttribute.MaximumLength;

            //是否邮箱地址
            var EmailAddressAttribute = GetAttribute<EmailAddressAttribute>(pro, false);
            var isEmail = EmailAddressAttribute != null;

            T4DataType? dataType = null;
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
                    if (maxLength <= 300)
                    {
                        dataType = T4DataType.Text;
                    }
                    else
                    {
                        dataType = T4DataType.MultilineText;
                    }
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
    }
}
