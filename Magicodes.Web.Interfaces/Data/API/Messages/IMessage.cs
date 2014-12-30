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
//        filename :IMessage
//        description :
//
//        created by 雪雁 at  2014/12/30 15:47:10
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data.API.Messages
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public interface IMessage
    {
        string Title { get; set; }
        string Content { get; set; }
        int MessageType { get; set; }
    }
}
