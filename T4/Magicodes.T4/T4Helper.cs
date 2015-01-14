using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.T4.Extensions;
using Magicodes.T4.Models;
using Magicodes.Web.Interfaces.T4;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :T4Helper
//        description :
//
//        created by 雪雁 at  2015/1/9 11:54:10
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4
{
    public class T4Helper
    {
        public static string T4Html(Type type, Dictionary<T4DataType, string> dic, string defaultHtmlTemplate, string tag)
        {
            if (type == null || dic == null) return string.Empty;
            var str = new StringBuilder();
            var proInfos = type.GetT4PropertyInfos();

            //组特性
            var t4FormGroupAttribute = type.GetAttribute<T4FormGroupAttribute>(false);
            var groups = new List<string>();

            foreach (var item in proInfos)
            {

            }
            //foreach (PropertyInfo pro in type.GetProperties())
            //{
            //    var html = pro.T4Html(dic, defaultHtmlTemplate, tag);
            //    if (!string.IsNullOrEmpty(html))
            //        str.AppendLine(html);
            //}
            return str.ToString();
        }
    }
}
