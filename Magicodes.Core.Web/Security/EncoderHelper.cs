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
//        filename :EncoderHelper
//        description :
//
//        created by 雪雁 at  2014/11/17 10:26:42
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Security
{
    public class EncoderHelper
    {
        public static string CssEncode(string str)
        {
            return Microsoft.Security.Application.Encoder.CssEncode(str);
        }
        public static string HtmlAttributeEncode(string str)
        {
            return Microsoft.Security.Application.Encoder.HtmlAttributeEncode(str);
        }
        public static string HtmlEncode(string str, bool useNamedEntities)
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(str, useNamedEntities);
        }
        public static string HtmlEncode(string str)
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(str);
        }
        public static string HtmlFormUrlEncode(string str, System.Text.Encoding inputEncoding)
        {
            return Microsoft.Security.Application.Encoder.HtmlFormUrlEncode(str, inputEncoding);
        }
        public static string HtmlFormUrlEncode(string str, int codePage)
        {
            return Microsoft.Security.Application.Encoder.HtmlFormUrlEncode(str, codePage);
        }
        public static string HtmlFormUrlEncode(string str)
        {
            return Microsoft.Security.Application.Encoder.HtmlFormUrlEncode(str);
        }
        public static string JavascriptEncode(string str, bool emitQuotes = false)
        {
            return Microsoft.Security.Application.Encoder.JavaScriptEncode(str, emitQuotes);
        }

        public static string JavascriptEncode(string str)
        {
            return Microsoft.Security.Application.Encoder.JavaScriptEncode(str);
        }
        public static string LdapDistinguishedNameEncode(string input, bool useInitialCharacterRules, bool useFinalCharacterRule)
        {
            return Microsoft.Security.Application.Encoder.LdapDistinguishedNameEncode(input, useInitialCharacterRules, useFinalCharacterRule);
        }
        public static string LdapDistinguishedNameEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.LdapDistinguishedNameEncode(input);
        }
        public static string LdapEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.LdapEncode(input);
        }
        public static string LdapFilterEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.LdapFilterEncode(input);
        }
        public static string UrlEncode(string input, System.Text.Encoding inputEncoding)
        {
            return Microsoft.Security.Application.Encoder.UrlEncode(input, inputEncoding);
        }
        public static string UrlEncode(string input, int codePage)
        {
            return Microsoft.Security.Application.Encoder.UrlEncode(input, codePage);
        }
        public static string UrlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.UrlEncode(input);
        }
        public static string UrlPathEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.UrlPathEncode(input);
        }
        public static string VisualBasicScriptEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.VisualBasicScriptEncode(input);
        }
        public static string XmlAttributeEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.XmlAttributeEncode(input);
        }
        public static string XmlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.XmlEncode(input);
        }
    }
}
