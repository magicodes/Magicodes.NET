using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Web.Interfaces.Strategy.Email
{
    public interface IMailStrategy : IStrategyBase
    {
        Task SendAsync(MailInfo message);
    }
}
