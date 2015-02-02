using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :T4MvcHelper
//        description :
//
//        created by 雪雁 at  2015/2/1 21:34:46
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4.Templates.MVC
{
    public class T4MvcHelper
    {
        public static string CreateMvcViewsDirectoryIfNotExist(string templatePath, string controllerName)
        {
            var controllerDirPath = Path.GetDirectoryName(templatePath);
            if (!controllerDirPath.EndsWith("Controllers"))
                throw new NotSupportedException("Magicodes.NET MVC基架参数模板只允许放在控制器目录。");
            
            if (Directory.Exists(controllerDirPath))
            {
                var controllerDir = new DirectoryInfo(controllerDirPath);
                var viewsDirPath = Path.Combine(controllerDir.Parent.FullName, "Views");
                if (!Directory.Exists(viewsDirPath))
                    Directory.CreateDirectory(viewsDirPath);

                var controllerViewDirPath = Path.Combine(viewsDirPath, controllerName);
                if (!Directory.Exists(controllerViewDirPath))
                    Directory.CreateDirectory(controllerViewDirPath);
                return controllerViewDirPath;
            }
            throw new System.IO.DirectoryNotFoundException("控制器目录不存在。");
        }
    }
}
