using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Web.Interfaces.Data
{
    public partial interface IDbSession:IDisposable
    {
        IUnitOfWork StartupUnitOfWork();
    }
}
