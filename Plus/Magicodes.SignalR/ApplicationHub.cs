using Magicodes.Models.Mvc.Models.Account;
using Microsoft.AspNet.SignalR;
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
//        filename :GlobalHub
//        description :
//
//        created by 雪雁 at  2014/10/25 17:18:43
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.SignalR
{
    public class ApplicationHub : Hub
    {
        static List<string> OnLineUser = new List<string>();

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
}
