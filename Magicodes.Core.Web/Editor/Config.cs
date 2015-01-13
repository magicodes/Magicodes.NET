using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

//*************************************************************************************
// <copyright file="Config" company="Magicode Team">
//     copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Eyes</author>
// <summary></summary>
//*************************************************************************************
namespace Magicodes.Core.Web.Editor
{
    public static class Config
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Scripts/ueditor/config.json"));
            return JObject.Parse(json);
        }
        private static JObject _items;
        public static JObject Items
        {
            get
            {
                if (noCache || _items == null)
                {
                    _items = BuildItems();
                }
                return _items;
            }
        }
        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static string[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<string>()).ToArray();
        }

        public static string GetString(string key)
        {
            return GetValue<string>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
