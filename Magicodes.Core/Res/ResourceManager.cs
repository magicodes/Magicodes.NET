using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus.Resource;
using Magicodes.Utility;
using Magicodes.Core.Plus;
using Magicodes.Web.Interfaces.Strategy.Logger;
namespace Magicodes.Core.Res
{
    public class ResourceManager
    {
        #region 属性
        Stream _stream;
        StringBuilder _sb;
        string _webResource;
        byte[] _output;
        int _length;
        static readonly LoggerStrategyBase Log = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        /// <summary>
        /// 是否压缩流--GZip
        /// </summary>
        public bool IsCompress { get; set; }
        /// <summary>
        /// 是否压缩内容
        /// </summary>
        public bool IsMinify { get; set; }

        IManifestResourceManager ManifestResourceManager = GlobalApplicationObject.Current.ApplicationContext.ManifestResourceManager;

        /// <summary>
        /// 是否为调试模式（Get参数IsDebug，或者ASPX  Debug模式）
        /// 调试模式将不启用脚本与样式压缩，并输出详细异常信息
        /// ASPX Debug模式在WebConfig中compilation元素debug属性配置
        /// </summary>
        public bool IsDebug
        {
            get
            {
                return GlobalApplicationObject.Current.ApplicationContext.ApplicationMode == ApplicationModes.Debug;
            }
        }
        public HttpContext HttpCurrentContext { get; set; }
        #endregion
        /// <summary>
        /// 判断是否为图片
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsImage(string ext)
        {
            return ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg";
        }
        /// <summary>
        /// 判断资源是否有修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsSourceModified(HttpRequest request)
        {
            bool dateModified = false;
            //获取上次修改时间
            string requestIfModifiedSinceHeader = request.Headers["If-Modified-Since"] ?? string.Empty;
            DateTime requestIfModifiedSince;
            DateTime.TryParse(requestIfModifiedSinceHeader, out requestIfModifiedSince);
            DateTime responseLastModified = new DateTime(AssemblyManager.GetAssemblyTicks(typeof(ResourceManager).Assembly)).ToUniversalTime();
            if (requestIfModifiedSince != DateTime.MinValue && responseLastModified != DateTime.MinValue)
            {
                requestIfModifiedSince = requestIfModifiedSince.ToUniversalTime();
                if (responseLastModified > requestIfModifiedSince)
                {
                    TimeSpan diff = responseLastModified - requestIfModifiedSince;
                    if (diff > TimeSpan.FromSeconds(1))
                    {
                        dateModified = true;
                    }
                }
            }
            else
            {
                dateModified = true;
            }
            return dateModified;
        }
        /// <summary>
        /// 向页面输出内容
        /// </summary>
        /// <param name="data"></param>
        /// <param name="responseType"></param>
        public void Send(byte[] data, string responseType)
        {
            if (this.IsCompress)
            {
                CompressionUtils.Send(data, responseType);
            }
            else
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Charset = "utf-8";
                response.ContentType = responseType;
                response.BinaryWrite(data);
            }
        }
        public void SetCache(byte[] content)
        {
            this.HttpCurrentContext.Cache.Insert(this._webResource + (this.IsCompress ? "_gzip" : "_nonegzip"), content, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromDays(30));
        }

        /// <summary>
        /// 设置浏览器缓存
        /// </summary>
        public void SetResponseCache()
        {
            HttpCachePolicy cache = HttpCurrentContext.Response.Cache;
            DateTime modifiedDate = new DateTime(AssemblyManager.GetAssemblyTime(typeof(ResourceManager).Assembly).Ticks).ToUniversalTime();
            DateTime nowDate = DateTime.Now.ToUniversalTime().AddSeconds(-1);
            if (modifiedDate > nowDate)
            {
                modifiedDate = nowDate;
            }
            cache.SetLastModified(modifiedDate);
            cache.SetOmitVaryStar(true);
            cache.SetVaryByCustom("v");
            cache.SetExpires(DateTime.UtcNow.AddDays(365));
            cache.SetMaxAge(TimeSpan.FromDays(365));
            cache.SetValidUntilExpires(true);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetCacheability(HttpCacheability.Public);
            cache.SetLastModifiedFromFileDependencies();
        }
        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="responseType"></param>
        private void WriteImage(string responseType)
        {
            this._output = this.GetCache();

            if (this._output == null)
            {
                this._length = Convert.ToInt32(this._stream.Length);
                this._output = new Byte[this._length];
                this._stream.Read(this._output, 0, this._length);

                this.SetCache(this._output);
            }

            this.Send(this._output, responseType);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        private byte[] GetCache()
        {
            return this.HttpCurrentContext.Cache[this._webResource + (this.IsCompress ? "_gzip" : "_nonegzip")] as byte[];
        }
        public virtual string GetWebResourceUrl(string resourceName)
        {
            return this.GetWebResourceUrl(this.GetType(), resourceName);
        }
        public virtual string GetWebResourceUrl(Type type, string resourceName)
        {
            return string.Empty;
        }
        public virtual string GetWebResourceAsString(Type type, string resourceName)
        {
            string script;
            using (var reader = new System.IO.StreamReader(type.Assembly.GetManifestResourceStream(null, resourceName)))
            {
                script = reader.ReadToEnd();
            }

            return script;
        }
        public virtual string GetWebResourceAsString(string resourceName)
        {
            return this.GetWebResourceAsString(this.GetType(), resourceName);
        }
        public ResourceManager(HttpContext context)
        {
            HttpCurrentContext = context;
            IsCompress = CompressionUtils.IsGZipSupported;
        }
        public ResourceManager(HttpContext context, bool isCompress)
        {
            HttpCurrentContext = context;
            IsCompress = IsCompress;
        }
        /// <summary>
        /// 输出资源内容（会自动缓存、JS（CSS）压缩）
        /// </summary>
        public void SendResourceContent()
        {
            SendResourceContent(null);
        }
        /// <summary>
        /// 设置资源名称
        /// </summary>
        /// <param name="filePath"></param>
        private void SetWebResourceName(string filePath)
        {
            this._webResource = filePath.ToLower().Trim('/')
                .RightOf("/")
                .Replace("/", ".")
                .Replace("-", "_");
        }
        public const string ErrorTemplate = "<div id='error' class='error'>\n<h1>{0}</h1>\n<p>\n{1}\n</p></div>";
        /// <summary>
        /// 输出资源内容
        /// </summary>
        /// <param name="resName"></param>
        public void SendResourceContent(string resName)
        {
            if (resName.IsEmpty())
            {
                string file = this.HttpCurrentContext.Request.Url.AbsolutePath;
                SetWebResourceName(file);
            }
            else
            {
                if (!this._webResource.Contains("."))
                    throw new System.Web.HttpException("无法支持该类请求！Path：" + this._webResource);
            }
            string ext = this._webResource.RightOfRightmostOf('.');
            try
            {
                var resourceUrl = ManifestResourceManager.GetResourceUrlByResourceName(this._webResource);
                if (resourceUrl == null) throw new System.Web.HttpException("找不到此资源！Path：" + this._webResource);
                //如果为别名，则获取实际链接
                if (resourceUrl.IsAlias) resourceUrl = ManifestResourceManager.GetResourceUrlByResourceName(resourceUrl.ManifestResourceName.ToLower());
                if (resourceUrl.HasWrittenToSiteDir)
                {
                    HttpCurrentContext.Server.TransferRequest(resourceUrl.SiteRelativeUrl, true);
                    return;
                }
                //从浏览器缓存中获取
                if (!ResourceManager.IsSourceModified(HttpCurrentContext.Request))
                {
                    HttpCurrentContext.Response.SuppressContent = true;
                    HttpCurrentContext.Response.StatusCode = 304;
                    HttpCurrentContext.Response.StatusDescription = "Not Modified";
                    HttpCurrentContext.Response.AddHeader("Content-Length", "0");
                    return;
                }
                //设置输出缓存
                SetResponseCache();
                //从dll嵌入资源中根据资源名称获取资源流
                this._stream = ManifestResourceManager.GetManifestResourceStream(resourceUrl);
                //设置是否启用JS压缩
                if (!this._webResource.ToLower().Contains(".min.") && (ext == "js" || ext == "css"))
                    IsMinify = true;

                this.IsCompress = this.IsCompress && !ResourceManager.IsImage(ext);
                switch (ext)
                {
                    case "js":
                        this.WriteFile("text/javascript");
                        break;
                    case "css":
                        this.WriteFile("text/css");
                        break;
                    case "htm":
                    case "html":
                    //case "aspx":
                    //    this.WriteFile("text/html");
                    //    break;
                    case "txt":
                        this.WriteFile("text/plain");
                        break;
                    case "xls":
                        this.WriteFile("application/x-excel");
                        break;
                    case "pdf":
                        this.WriteFile("application/pdf");
                        break;
                    case "gif":
                        this.WriteImage("image/gif");
                        break;
                    case "png":
                        this.WriteImage("image/png");
                        break;
                    case "jpg":
                    case "jpeg":
                        this.WriteImage("image/jpg");
                        break;
                    #region 字体文件
                    case "eot":
                    case "ttf":
                    case "otf":
                    case "woff":
                        this.WriteFile("font/" + ext);
                        break;
                    case "svg":
                        this.WriteFile("image/svg+xml");
                        break;
                    default:
                        throw new Exception("不支持的请求类型！");
                        break;
                    #endregion
                }
            }
            catch (Exception e)
            {
                string s = this.IsDebug ? e.ToString() : e.Message;
                HttpCurrentContext.Response.StatusCode = 500;
                HttpCurrentContext.Response.StatusDescription = s.Substring(0, Math.Min(s.Length, 512));
                Log.Log(LoggerLevels.Error, "获取资源文件失败！Path:" + this._webResource, e);
                this.HttpCurrentContext.Response.Write(IsDebug
                    ? string.Format(ErrorTemplate, e.Message, e.ToString())
                    : string.Format(ErrorTemplate, "出现意外错误", "请联系管理员！"));
            }
            finally
            {
                if (this._stream != null)
                    this._stream.Close();
            }
        }
        /// <summary>
        /// 输出文件
        /// </summary>
        /// <param name="responseType"></param>
        private void WriteFile(string responseType)
        {
            this._output = this.GetCache();
            if (this._output != null)
            {
                this.Send(this._output, responseType);
                return;
            }
            if (this._stream == null)
            {
                Response404(responseType);
                return;
            }
            this._sb = new StringBuilder(4096);
            using (this._stream)
            {
                var reader = new StreamReader(this._stream);
                this._sb.Append(reader.ReadToEnd());
            }
            var data = _sb.ToString();
            //压缩脚本与样式
            if (IsMinify && !IsDebug) data = ResourceHelper.MinHelper.MinJs(data);

            var content = this.IsCompress ? CompressionUtils.GZip(data) : Encoding.UTF8.GetBytes(data);
            this.SetCache(content);
            this.Send(content, responseType);

        }
        /// <summary>
        /// 输出404
        /// </summary>
        /// <param name="responseType"></param>
        private void Response404(string responseType)
        {
            this.Send(Encoding.UTF8.GetBytes(string.Format("资源“{0}”不存在。", this._webResource)), responseType);
        }
    }
}
