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
//        filename :WatchPanel
//        description :监控面板
//
//        created by 雪雁 at  2014/11/17 16:57:29
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Performance.WatchPanel
{
    /// <summary>
    /// 监控面板
    /// </summary>
    public class WatchPanel
    {
        public WatchPanel()
        {
            WatchTabs = new List<WatchTab>();
        }
        #region 属性
        /// <summary>
        /// 监控tab
        /// </summary>
        public List<WatchTab> WatchTabs { get; set; }
        #endregion
        /// <summary>
        /// 选项卡添加事件
        /// </summary>
        public event EventHandler OnTabAdd = (o, e) => { };
        /// <summary>
        /// 消息事件
        /// </summary>
        public event EventHandler OnMessageAdd = (o, e) => { };
        /// <summary>
        /// 执行选项卡添加事件
        /// </summary>
        /// <param name="sender"></param>
        internal void ExecuteTabAddEvent(WatchTab sender)
        {
            OnTabAdd(sender, new EventArgs());
        }
        /// <summary>
        /// 执行消息添加事件
        /// </summary>
        /// <param name="sender"></param>
        internal void ExecuteMessageAddEvent(WatchMessage sender)
        {
            OnMessageAdd(sender, new EventArgs());
        }

        #region 方法
        /// <summary>
        /// 添加监控面板
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public async virtual Task<WatchTab> AddTabAsync(string tabName)
        {
            if (WatchTabs.Any(p => p.TabName == tabName))
                return null;
            var watchTab = new WatchTab(tabName);
            WatchTabs.Add(watchTab);
            this.ExecuteTabAddEvent(watchTab);
            return watchTab;
        }
        /// <summary>
        /// 获取当前控制面板
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public virtual WatchTab Find(string tabName)
        {
            return WatchTabs.Find(p => p.TabName == tabName);
        }
        #endregion


    }
}
