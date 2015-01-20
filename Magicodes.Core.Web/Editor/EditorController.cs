using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Magicodes.Core.Web.Controllers;

namespace Magicodes.Core.Web.Editor
{
    /// <summary>
    /// 配置默认的文档加载器
    /// </summary>
    [RoutePrefix("Editor")]
    public class EditorController:Controller
    {
        [Route]
        [Route("CentreHandler")]
        public void CentreHandler()
        {
            Handler action = null;
            var context = System.Web.HttpContext.Current;
            switch (context.Request["action"])
            {
                case "config":
                    action = new ConfigHandler(context);
                    break;
                case "listimage":
                    action = new ListFileManager(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new ListFileManager(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    action = new CrawlerHandler(context);
                    break;
                case "uploadimage":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = Config.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = Config.GetString("scrawlPathFormat"),
                        SizeLimit = Config.GetInt("scrawlMaxSize"),
                        UploadFieldName = Config.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("videoAllowFiles"),
                        PathFormat = Config.GetString("videoPathFormat"),
                        SizeLimit = Config.GetInt("videoMaxSize"),
                        UploadFieldName = Config.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("fileAllowFiles"),
                        PathFormat = Config.GetString("filePathFormat"),
                        SizeLimit = Config.GetInt("fileMaxSize"),
                        UploadFieldName = Config.GetString("fileFieldName")
                    });
                    break;
                default:
                    action = new NotSupportedHandler(context);
                    break;
            }
            action.Process();
        }
    }
}
