using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using Magicodes.Utility;
namespace Magicodes.Utility
{
    /// <summary>
    /// 压缩
    /// </summary>
    public class CompressionUtils
    {
        /// <summary>
        /// GZip压缩输出
        /// </summary>
        /// <param name="instance"></param>
        public static void GZipAndSend(object instance)
        {
            CompressionUtils.GZipAndSend((instance != null) ? instance.ToString() : string.Empty);
        }
        /// <summary>
        /// GZip压缩输出
        /// </summary>
        /// <param name="instance"></param>
        public static void GZipAndSend(string instance)
        {
            CompressionUtils.GZipAndSend(instance, "application/json");
        }
        /// <summary>
        /// GZip压缩输出
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="responseType"></param>
        public static void GZipAndSend(string instance, string responseType)
        {
            CompressionUtils.GZipAndSend(Encoding.UTF8.GetBytes(instance), responseType);
        }
        /// <summary>
        /// GZip压缩输出
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="responseType"></param>
        public static void GZipAndSend(byte[] instance, string responseType)
        {
            CompressionUtils.Send(CompressionUtils.GZip(instance), responseType);
        }
        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="responseType"></param>
        public static void Send(byte[] instance, string responseType)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.AppendHeader("Content-Encoding", "gzip");
            response.Charset = "utf-8";
            response.ContentType = responseType;
            response.BinaryWrite(instance);
        }
        /// <summary>
        /// GZip压缩
        /// </summary>
        public static byte[] GZip(string instance)
        {
            return GZip(Encoding.UTF8.GetBytes(instance));
        }
        /// <summary>
        /// GZip压缩
        /// </summary>
        public static byte[] GZip(byte[] instance)
        {
            MemoryStream stream = new MemoryStream();
            GZipStream zipstream = new GZipStream(stream, CompressionMode.Compress);
            zipstream.Write(instance, 0, instance.Length);
            zipstream.Close();

            return stream.ToArray();
        }
        /// <summary>
        /// 是否支持GZip压缩
        /// </summary>
        public static bool IsGZipSupported
        {
            get
            {
                HttpRequest request = HttpContext.Current.Request;
                bool ie6 = request.Browser.IsBrowser("IE") && request.Browser.MajorVersion <= 6;
                string encoding = (request.Headers["Accept-Encoding"] ?? string.Empty).ToLowerInvariant();
                return !ie6 && encoding.Contains("gzip", "deflate");
            }
        }
    }
}
