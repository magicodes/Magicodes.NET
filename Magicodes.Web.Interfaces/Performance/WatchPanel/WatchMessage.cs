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
//        filename :WatchMessage
//        description :
//
//        created by 雪雁 at  2014/11/17 17:07:42
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Performance.WatchPanel
{
    /// <summary>
    /// 监控消息
    /// </summary>
    public class WatchMessage
    {
        public WatchTab CurrentWatchTab { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
