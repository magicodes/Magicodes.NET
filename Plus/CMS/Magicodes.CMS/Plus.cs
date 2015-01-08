using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.Core.Web;
using Magicodes.Web.Interfaces.Plus;

namespace Magicodes.CMS
{
    public class Plus:IPlus
    {
        public void Initialize()
        {
            GlobalConfigurationManager.ODataBuilder.EntitySet<Magicodes.CMS.Models.CMS_Channel>("CMSChannel");
        }

        public void Install()
        {
            
        }

        public void Uninstall()
        {
           
        }
    }
}