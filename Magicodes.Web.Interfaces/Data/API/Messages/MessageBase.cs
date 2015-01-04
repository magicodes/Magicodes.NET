using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MessageBase
//        description :
//
//        created by 雪雁 at  2014/12/30 15:51:25
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data.API.Messages
{
    /// <summary>
    /// 消息基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserKeyType"></typeparam>
    public class MessageBase<TKey, TUserKeyType> : CommonBusinessModelBase<TKey, TUserKeyType>, IMessage
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(100)]
        public virtual string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>

        public virtual string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual int MessageType { get; set; }
    }
}
