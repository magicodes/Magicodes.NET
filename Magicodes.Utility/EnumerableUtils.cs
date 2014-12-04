using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Utility
{
    public static class EnumerableUtils
    {
        /// <summary>
        /// 遍历当前对象，并且调用方法进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="action">方法</param>
        /// <returns>当前集合</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T item in instance)
            {
                action(item);
            }

            return instance;
        }
        /// <summary>
        /// 遍历N次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static int Each<T>(this int instance, Action<int> action)
        {
            for (int i = 0; i < instance; i++)
            {
                action(i);
            }
            return instance;
        }
        /// <summary>
        /// 以“,”拼接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable items)
        {
            return items.Join(",", "{0}");
        }

        /// <summary>
        /// 使用分隔符拼接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable items, string separator)
        {
            return items.Join(separator, "{0}");
        }

        /// <summary>
        /// 使用分隔符、以及模板字符串拼接字符串
        /// </summary>
        /// <param name="items">待拼接集合</param>
        /// <param name="separator">分隔符</param>
        /// <param name="template">字符串格式化模板</param>
        /// <returns></returns>
        public static string Join(this IEnumerable items, string separator, string template)
        {
            var sb = new StringBuilder();
            foreach (object item in items)
            {
                if (item == null) continue;
                sb.Append(separator);
                sb.Append(string.Format(template, item.ToString()));
            }

            return sb.ToString().RightOf(separator);
        }
    }
}
