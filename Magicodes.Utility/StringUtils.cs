using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace Magicodes.Utility
{
    public static class StringUtils
    {
        
        /// <summary>
        /// 替换目标字符串中的所有oldValues数组中的字符串，并使用convert函数来转换返回新字符串
        /// </summary>
        /// <param name="instance">目标字符串</param>
        /// <param name="oldValues">需要替换的字符串数组</param>
        /// <param name="convert">转换函数</param>
        /// <returns>
        /// 替换后的字符串
        /// </returns>
        [Description("替换目标字符串中的所有oldValues数组中的字符串，并使用convert函数来转换返回新字符串")]
        public static string Replace(this string instance, string[] oldValues, Func<string, string> convert)
        {
            if (oldValues == null || oldValues.Length < 1)
            {
                return instance;
            }

            oldValues.Each<string>(value => instance = instance.Replace(value, convert(value)));

            return instance;
        }

        /// <summary>
        /// 判断字符串为null或者空或者空格.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// 判断字符串不为null或者空或者空格.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string text)
        {
            return !text.IsEmpty();
        }

        /// <summary>
        /// 返回两个字符串之间的字符串。
        /// </summary>
        [Description("返回两个字符串之间的字符串")]
        public static string Between(this string text, string start, string end)
        {
            return text.IsEmpty() ? text : text.RightOf(start).LeftOfRightmostOf(end);
        }
        /// <summary>
        /// 只要字符串中是否存在其中一个，则返回True
        /// </summary>
        /// <param name="instance">目标字符串</param>
        /// <param name="args">字符串组</param>
        /// <returns>是否存在</returns>
        public static bool Contains(this string instance, params string[] args)
        {
            return args.Any(instance.Contains);
        }
        /// <summary>
        /// 如果目录不存在，则创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        public static string CreateDirectoryIfNotExist(this string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        public static string ToMD5Hash(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text.Trim());
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        /// <param name="text">The text to convert to sentence case</param>
        public static string ToTitleCase(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return text.Split(' ').ToTitleCase();
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        /// <param name="text">The text to convert to sentence case</param>
        public static string ToTitleCase(this string text, CultureInfo ci)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return text.Split(' ').ToTitleCase(ci);
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        public static string ToTitleCase(this string[] words)
        {
            return words.ToTitleCase(null);
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        public static string ToTitleCase(this string[] words, CultureInfo ci)
        {
            if (words == null || words.Length == 0)
            {
                return "";
            }

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = (ci != null ? char.ToUpper(words[i][0], ci) : char.ToUpper(words[i][0])) + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsLowerCamelCase(this string text)
        {
            if (text.IsEmpty())
            {
                return false;
            }

            return text.Substring(0, 1).ToLowerInvariant().Equals(text.Substring(0, 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToLowerCamelCase(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return text.Substring(0, 1).ToLower(CultureInfo.InvariantCulture) + text.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToLowerCamelCase(this string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return "";
            }

            return values.ToLowerCamelCase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return text.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + text.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string[] values, string separator)
        {
            string temp = "";

            foreach (string s in values)
            {
                temp += separator;
                temp += ToCamelCase(s);
            }

            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string[] values)
        {
            return values.ToCamelCase("");
        }

        /// <summary>
        /// Pad the left side of a string with characters to make the total length.
        /// </summary>
        public static string PadLeft(this string text, char c, Int32 totalLength)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            if (totalLength < text.Length)
            {
                return text;
            }

            return new String(c, totalLength - text.Length) + text;
        }

        /// <summary>
        /// Pad the right side of a string with a '0' if a single character.
        /// </summary>
        public static string PadRight(this string text)
        {
            return PadRight(text, '0', 2);
        }

        /// <summary>
        /// Pad the right side of a string with characters to make the total length.
        /// </summary>
        public static string PadRight(this string text, char c, Int32 totalLength)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            if (totalLength < text.Length)
            {
                return text;
            }

            return string.Concat(text, new String(c, totalLength - text.Length));
        }

        /// <summary>
        /// Left of the first occurance of c
        /// </summary>
        public static string LeftOf(this string text, char c)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.IndexOf(c);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(0, i);
        }

        /// <summary>
        /// Left of the first occurance of text
        /// </summary>
        public static string LeftOf(this string text, string value)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.IndexOf(value);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(0, i);
        }

        /// <summary>
        /// Left of the n'th occurance of c
        /// </summary>
        public static string LeftOf(this string text, char c, int n)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = -1;

            while (n != 0)
            {
                i = text.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return text;
                }
                --n;
            }

            return text.Substring(0, i);
        }

        /// <summary>
        /// Right of the first occurance of c
        /// </summary>
        public static string RightOf(this string text, char c)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.IndexOf(c);

            if (i == -1)
            {
                return string.Empty;
            }

            return text.Substring(i + 1);
        }

        /// <summary>
        /// Right of the first occurance of text
        /// </summary>
        public static string RightOf(this string text, string value)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.IndexOf(value);

            if (i == -1)
            {
                return "";
            }

            return text.Substring(i + value.Length);
        }

        /// <summary>
        /// Right of the n'th occurance of c
        /// </summary>
        public static string RightOf(this string text, char c, int n)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = -1;

            while (n != 0)
            {
                i = text.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return "";
                }
                --n;
            }

            return text.Substring(i + 1);
        }

        /// <summary>
        /// Right of the n'th occurance of c
        /// </summary>
        public static string RightOf(this string text, string c, int n)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = -1;

            while (n != 0)
            {
                i = text.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return "";
                }
                --n;
            }

            return text.Substring(i + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string LeftOfRightmostOf(this string text, char c)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.LastIndexOf(c);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(0, i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LeftOfRightmostOf(this string text, string value)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.LastIndexOf(value);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(0, i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RightOfRightmostOf(this string text, char c)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.LastIndexOf(c);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(i + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RightOfRightmostOf(this string text, string value)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            int i = text.LastIndexOf(value);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(i + value.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceLastInstanceOf(this string text, string oldValue, string newValue)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return string.Format("{0}{1}{2}", text.LeftOfRightmostOf(oldValue), newValue, text.RightOfRightmostOf(oldValue));
        }

        /// <summary>
        /// Accepts a string like "ArrowRotateClockwise" and returns "arrow_rotate_clockwise.png".
        /// </summary>
        public static string ToCharacterSeparatedFileName(this string name, char separator, string extension)
        {
            if (name.IsEmpty())
            {
                return name;
            }

            MatchCollection match = Regex.Matches(name, @"([A-Z]+)[a-z]*|\d{1,}[a-z]{0,}");

            string temp = "";

            for (int i = 0; i < match.Count; i++)
            {
                if (i != 0)
                {
                    temp += separator;
                }

                temp += match[i].ToString().ToLowerInvariant();
            }

            string format = (string.IsNullOrEmpty(extension)) ? "{0}{1}" : "{0}.{1}";

            return string.Format(format, temp, extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Enquote(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            int i;
            int len = text.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            for (i = 0; i < len; i += 1)
            {
                char c = text[i];
                if ((c == '\\') || (c == '"') || (c == '>'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else
                {
                    if (c < ' ')
                    {
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EnsureSemiColon(this string text)
        {
            if (text.IsEmpty())
            {
                return text;
            }

            return (string.IsNullOrEmpty(text) || text.EndsWith(";")) ? text : string.Concat(text, ";");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Test(this string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool Test(this string text, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(text, pattern, options);
        }

        /// <summary>
        /// Truncate a string and add an ellipsis ('...') to the end if it exceeds the specified length
        /// </summary>
        /// <param name="text">The string to truncate</param>
        /// <param name="length">The maximum length to allow before truncating</param>
        /// <returns>The converted text</returns>
        public static string Ellipsis(this string text, int length)
        {
            return StringUtils.Ellipsis(text, length, false);
        }

        /// <summary>
        /// Truncate a string and add an ellipsis ('...') to the end if it exceeds the specified length
        /// </summary>
        /// <param name="text">The string to truncate</param>
        /// <param name="length">The maximum length to allow before truncating</param>
        /// <param name="word">True to try to find a common work break</param>
        /// <returns>The converted text</returns>
        public static string Ellipsis(this string text, int length, bool word)
        {
            if (text != null && text.Length > length)
            {
                if (word)
                {
                    string vs = text.Substring(0, length - 2);
                    int index = Math.Max(vs.LastIndexOf(' '), Math.Max(vs.LastIndexOf('.'), Math.Max(vs.LastIndexOf('!'), vs.LastIndexOf('?'))));

                    if (index == -1 || index < (length - 15))
                    {
                        return text.Substring(0, length - 3) + "...";
                    }

                    return vs.Substring(0, index) + "...";
                }

                return text.Substring(0, length - 3) + "...";
            }
            return text;
        }

        /// <summary>
        /// Base64 string decoder
        /// </summary>
        /// <param name="text">The text string to decode</param>
        /// <returns>The decoded string</returns>
        public static string Base64Decode(this string text)
        {
            Decoder decoder = new UTF8Encoding().GetDecoder();

            byte[] bytes = Convert.FromBase64String(text);
            char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];

            decoder.GetChars(bytes, 0, bytes.Length, chars, 0);

            return new String(chars);
        }

        /// <summary>
        /// Base64 string encoder
        /// </summary>
        /// <param name="text">The text string to encode</param>
        /// <returns>The encoded string</returns>
        public static string Base64Encode(this string text)
        {
            byte[] bytes = new byte[text.Length];
            bytes = Encoding.UTF8.GetBytes(text);

            return Convert.ToBase64String(bytes);
        }


        private static readonly Random random = new Random();

        /// <summary>
        /// Generate a random string of character at a certain length
        /// </summary>
        /// <param name="chars">The Characters to use in the random string</param>
        /// <param name="length">The length of the random string</param>
        /// <returns>A string of random characters</returns>
        public static string Randomize(this string chars, int length)
        {
            char[] buf = new char[length];

            for (int i = 0; i < length; i++)
            {
                buf[i] = chars[StringUtils.random.Next(chars.Length)];
            }

            return new string(buf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string Randomize(this string chars)
        {
            return chars.Randomize(chars.Length);
        }

        /// <summary>
        /// 判断Int数组是否在字符串中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="iArr"></param>
        /// <returns></returns>
        public static bool IsIntArraInStr(this string str, IEnumerable<int> iArr)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            var ids = iArr.ConvertIntArrToStr();
            return str.Contains(ids);
        }
        /// <summary>
        /// 获取最后一个分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="spitChar"></param>
        /// <returns></returns>
        public static string GetTheLastStrFromArraStr(this string str, char spitChar)
        {
            var arra = str.Split(spitChar);
            return arra[arra.Length - 1];
        }
        /// <summary>
        /// 将可以字符串转换为int数组
        /// </summary>
        /// <param name="str">，1，2，3，  必须是前逗号后逗号格式</param>
        /// <returns>返回int数组，不排序</returns>
        public static IEnumerable<int> ConvertStrToIntArr(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return !str.Contains(",") ? null : str.Trim(',').Split(',').Select(p => Convert.ToInt32(p));
        }
        ///// <summary>
        ///// 将int数组转换为字符串格式
        ///// </summary>
        ///// <param name="intArr"></param>
        ///// <returns>返回前逗号后逗号格式的字符串，字符串已排序</returns>
        //public static string ConvertIntArrToStr(this int[] intArr)
        //{
        //    return intArr.ConvertIntArrToStr();
        //}
        /// <summary>
        /// 将int数组转换为字符串格式
        /// </summary>
        /// <param name="intArr"></param>
        /// <returns>返回前逗号后逗号格式的字符串，字符串已排序</returns>
        public static string ConvertIntArrToStr(this IEnumerable<int> intArr)
        {
            return string.Format(",{0},", string.Join(",", intArr.OrderBy(p => p)));
        }
    }
}
