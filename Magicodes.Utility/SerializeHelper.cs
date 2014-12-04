using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Magicodes.Utility
{
    /// <summary>
    /// XML序列化辅助类
    /// </summary>
    public class SerializeHelper
    {
        #region XML序列化
        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="filename">文件路径</param>
        public static T Load<T>(string filename)
        {
            return (T)Load(typeof(T), filename);
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filename">文件路径</param>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="strXml">xml内容</param>
        public static T LoadFromStr<T>(string strXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(strXml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public static string ToXml<T>(T item)
        {
            var serializer = new XmlSerializer(item.GetType());
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }
        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public static string ToXmlAndFormat<T>(T item)
        {
            var serializer = new XmlSerializer(item.GetType());
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = System.Xml.Formatting.Indented;
                writer.Indentation = 1;
                writer.IndentChar = '\t';
                //xd.WriteTo(xtw);
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }
        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        /// <param name="path">路径</param>
        public static void ToXmlAndFormat<T>(T item, string path)
        {
            File.WriteAllText(path, ToXmlAndFormat(item));
        }
        #endregion

        #region SoapFormatter序列化
        /// <summary>
        /// SoapFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToSoap<T>(T item)
        {
            var formatter = new SoapFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// SoapFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromSoap<T>(string str)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);
            var formatter = new SoapFormatter();
            using (var ms = new MemoryStream())
            {
                xmlDoc.Save(ms);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        #region BinaryFormatter序列化
        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToBinary<T>(T item)
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                byte[] bytes = ms.ToArray();
                var sb = new StringBuilder();
                foreach (var bt in bytes)
                {
                    sb.Append(string.Format("{0:X2}", bt));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromBinary<T>(string str)
        {
            var intLen = str.Length / 2;
            var bytes = new byte[intLen];
            for (var i = 0; i < intLen; i++)
            {
                var ibyte = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                bytes[i] = (byte)ibyte;
            }
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion
    }
}
