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
//        filename :WatchTab
//        description :
//
//        created by 雪雁 at  2014/11/17 16:59:33
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Performance.WatchPanel
{
    /// <summary>
    /// 监控选项卡
    /// </summary>
    public class WatchTab
    {
        public WatchTab(string tabName)
        {
            Messages = new List<WatchMessage>();
            this.TabName = tabName;
        }
        /// <summary>
        /// Tab名称
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// 顶部内容
        /// </summary>
        public string Head { get; set; }
        /// <summary>
        /// 底部内容
        /// </summary>
        public string Foot { get; set; }
        /// <summary>
        /// Tab显示内容
        /// </summary>
        public List<WatchMessage> Messages { get; set; }
        /// <summary>
        /// 限制数。0为不限制
        /// </summary>
        private int limitCount = 0;
        /// <summary>
        /// 消息限制数
        /// </summary>
        public int LimitCount
        {
            get { return limitCount; }
            set { limitCount = value; }
        }
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public virtual void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            if (LimitCount > 0 && Messages.Count > LimitCount)
            {
                Messages.RemoveRange(0, Messages.Count - LimitCount);
            }
            var watchMessage = new WatchMessage()
            {
                CurrentWatchTab = this,
                Message = message,
                UpdateTime = DateTime.Now
            };
            Messages.Add(watchMessage);

            Magicodes.Web.Interfaces.GlobalApplicationObject.Current.WatchPanel.ExecuteMessageAddEvent(watchMessage);
        }
    }
}
