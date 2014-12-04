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
//        filename :MailMessage
//        description :
//
//        created by 雪雁 at  2014/10/22 14:03:52
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Strategy.Email
{
    public class MailInfo
    {
        // 摘要: 
        //     Message contents
        public virtual string Body { get; set; }
        //
        // 摘要: 
        //     Destination, email 地址
        public virtual string Destination { get; set; }
        //
        // 摘要: 
        //     Subject
        public virtual string Subject { get; set; }
    }
}
