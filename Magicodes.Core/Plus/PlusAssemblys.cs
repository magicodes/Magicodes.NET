using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces.Plus.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Plus
{
    public class PlusAssemblyInfo : IPlusAssemblyInfo
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Version { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Configuration { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Copyright { get; set; }

        public string Trademark { get; set; }

        public string Culture { get; set; }

        public PlusConfigInfo PlusConfigInfo { get; set; }

        public bool IsInstalled { get; set; }
    }
}
