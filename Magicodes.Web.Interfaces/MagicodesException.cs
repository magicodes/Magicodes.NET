using System;
using System.Runtime.Serialization;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MagicodesException
//        description :
//
//        created by 雪雁 at  2014/10/19 14:57:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces
{
    /// <summary>
    /// Magicodes异常类
    /// </summary>
    [Serializable]
    public class MagicodesException : ApplicationException
    {
        /// <summary>
        /// 异常关键字，便于查找问题
        /// </summary>
        public string KeyWord { get; set; }
        public MagicodesException() { }
        /// <summary>
        /// Magicodes.NET框架异常
        /// </summary>
        /// <param name="message">异常消息以及关键字（建议不要添加特殊字符）</param>
        public MagicodesException(string message) : base(message) {
            KeyWord = message;
        }
        public MagicodesException(string message, Exception inner) : base(message, inner) { }

        protected MagicodesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        private string helpLink = "https://github.com/magicodes/Magicodes.NET/issues?q=is%3Aissue";
        /// <summary>
        /// 帮助链接
        /// </summary>
        public override string HelpLink
        {
            get
            {
                return string.IsNullOrEmpty(KeyWord) ? helpLink : helpLink + " " + KeyWord;
            }
            set
            {
                helpLink = value;
            }
        }
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + "帮助链接：" + HelpLink;
        }
    }
}
