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
        /// 转换为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerSettings jsonSetting)
        {
            return JsonConvert.SerializeObject(obj, jsonSetting);
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
