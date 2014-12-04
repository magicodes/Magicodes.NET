using Magicodes.Web.Interfaces.WebHandler.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Magicodes.Utility
{
    public static class JSONUtils
    {
        /// <summary>
        /// 转换为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 转换为JSON字符串，并格式化日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonWithDateFormatyyMMddHHmmss(this object obj)
        {
            return obj.ToJson(JSONFormatHanding.DateFormatString_yyMMddHHmmss);
        }

        /// <summary>
        /// 转换为JSON字符串，并格式化日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonWithDateFormatyyyyMMddHHmmss(this object obj)
        {
            return obj.ToJson(JSONFormatHanding.DateFormatString_yyyyMMddHHmmss);
        }

        /// <summary>
        /// 转换为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerSettings jsonSetting)
        {
            return JsonConvert.SerializeObject(obj, jsonSetting);
        }

        /// <summary>
        /// 转换为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JSONFormatHanding jsonHanding)
        {
            switch (jsonHanding)
            {
                case JSONFormatHanding.DateFormatHandling_MicrosoftDateFormat:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    });
                case JSONFormatHanding.DateFormatHandling_IsoDateFormat:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat
                    });
                case JSONFormatHanding.DefaultValueHandling_Ignore:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });
                case JSONFormatHanding.NullValueHandling_Ignore:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                case JSONFormatHanding.TypeNameHandling_All:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                case JSONFormatHanding.DateFormatString_yyMMddHHmmss:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        DateFormatString = "yy-MM-dd HH:mm:ss"
                    });
                case JSONFormatHanding.DateFormatString_yyyyMMddHHmmss:
                    return obj.ToJson(new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-dd HH:mm:ss"
                    });
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 将JSON字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonStrDeserializeObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        /// 将XMLDocument对象转换为JSON字符串
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(this XmlDocument xmlDoc)
        {
            return JsonConvert.SerializeXmlNode(xmlDoc);
        }

        /// <summary>
        /// 将XMLDocument对象转换为JSON字符串
        /// </summary>
        /// <param name="xNode"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(this XNode xNode)
        {
            return JsonConvert.SerializeXNode(xNode);
        }

        /// <summary>
        /// 将XMLDocument对象转换为JSON字符串
        /// </summary>
        /// <param name="xNode"></param>
        /// <param name="omitRootObject"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(this XNode xNode, bool omitRootObject)
        {
            return JsonConvert.SerializeXNode(xNode, Newtonsoft.Json.Formatting.None, omitRootObject);
        }
        /// <summary>
        /// 将XML字符串转换为JSON字符串
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(this string xmlStr)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            return doc.SerializeXmlNode();
        }
    }
}
