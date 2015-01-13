using System;
using System.Collections.Generic;
using System.Linq;
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
//        created by 雪雁 at  2015/1/9 11:54:10
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4
{
    public class T4Helper
    {
        //public string ProcessTemplate(string templateFilePath, string key, object obj)
        //{
        //    TextTemplatingSession session = new TextTemplatingSession();
        //    session[key] = obj;
        //    var sessionHost = (ITextTemplatingSessionHost)this.Host;
        //    sessionHost.Session = session;

        //    var sb = new StringBuilder();
        //    sb.AppendLine("<" + "#" + "@ template debug=\"true\" hostspecific=\"true\" language=\"C#\" #" + ">");
        //    foreach (var assembly in __assemblys)
        //    {
        //        sb.Append("<").Append("#").Append("@ assembly name=\"").Append(assembly).Append("\" #").Append(">").AppendLine();
        //    }
        //    sb.AppendLine(File.ReadAllText(templateFilePath, Encoding.UTF8));
        //    var content = sb.ToString();
        //    Log(content);
        //    return new Engine().ProcessTemplate(content, this.Host);
        //}
    }
}
