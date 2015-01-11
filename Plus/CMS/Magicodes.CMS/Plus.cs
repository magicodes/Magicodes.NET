using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.CMS.Models;
using Magicodes.CMS.ViewModels;
using Magicodes.Core.Web;
using Magicodes.Web.Interfaces.Plus;

namespace Magicodes.CMS
{
    public class Plus:IPlus
    {
        public void Initialize()
        {
            GlobalConfigurationManager.ODataBuilder.EntitySet<CMS_Channel>("CMSChannel");
            GlobalConfigurationManager.ODataBuilder.EntitySet<CMS_ClassType>("CMSClassType");
            GlobalConfigurationManager.ODataBuilder.EntitySet<CMS_ClassTypeInfoViewModel>("CMSClassTypeInfo");
        }

        public void Install()
        {
            
        }

        public void Uninstall()
        {
           
        }
    }
}