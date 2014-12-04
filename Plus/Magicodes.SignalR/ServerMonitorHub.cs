using Microsoft.AspNet.SignalR;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :ServerMonitorHub
//        description :
//
//        created by 雪雁 at  2014/10/26 12:12:53
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.SignalR
{
    /// <summary>
    /// 服务器性能监控Hub
    /// </summary>
    public class ServerMonitorHub : Hub
    {
        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

    }
    public class ServerMonitorHelper
    {
        /// <summary>
        /// CPU性能计数器
        /// </summary>
        public static readonly Lazy<PerformanceCounter> CPUPerformanceCounter = new Lazy<PerformanceCounter>(() => new PerformanceCounter("Processor", "% Processor Time", "_Total"));

        /// <summary>
        /// 计算机信息（提供用于获取与计算机的内存、已加载程序集、名称和操作系统有关的信息的属性）
        /// </summary>
        public static readonly Lazy<ComputerInfo> ComputerInfo = new Lazy<ComputerInfo>(() => new ComputerInfo());

        /// <summary>
        /// 获取CPU占用百分比
        /// </summary>
        /// <returns></returns>
        public static double GetCpuPercent()
        {
            var percentage = CPUPerformanceCounter.Value.NextValue();
            return Math.Round(percentage, 2, MidpointRounding.AwayFromZero);
        }
        /// <summary>
        /// 获取内存占用百分比
        /// </summary>
        /// <returns></returns>
        public double GetMemoryPercent()
        {
            var usedMem = ComputerInfo.Value.TotalPhysicalMemory - ComputerInfo.Value.AvailablePhysicalMemory;//总内存减去可用内存
            return Math.Round(
                     (double)(usedMem / Convert.ToDecimal(ComputerInfo.Value.TotalPhysicalMemory) * 100),
                     2,
                     MidpointRounding.AwayFromZero);
        }
    }
}
