using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Magicodes.Core.Web.Editor
{
    public class ConfigHandler:Handler
    {
        public ConfigHandler(HttpContext context):base(context)
        {
            
        }
        public override void Process()
        {
            WriteJson(Config.Items);
        }
    }
}
