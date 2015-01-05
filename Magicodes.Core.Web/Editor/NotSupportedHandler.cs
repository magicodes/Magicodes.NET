using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.Core.Web.Editor;

namespace Magicodes.Core.Web.Editor
{
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            WriteJson(new
            {
                state = "action 参数为空或者 action 不被支持。"
            });
        }
    }
}

