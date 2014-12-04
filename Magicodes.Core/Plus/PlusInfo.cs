using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Plus
{
    public class PlusInfo : IPlusInfo
    {

        public System.Reflection.Assembly Assembly { get; set; }


        public IPlusAssemblyInfo PlusAssemblys { get; set; }
    }
}
