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
        public MagicodesException() { }

        public MagicodesException(string message) : base(message) { }

        public MagicodesException(string message, Exception inner) : base(message, inner) { }

        protected MagicodesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
