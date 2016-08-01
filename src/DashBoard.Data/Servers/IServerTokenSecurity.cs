using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Data.Servers
{
    public interface IServerTokenSecurity
    {
        bool IsWellKnownServer(string serverName);
        Guid GenerateTokenFor(string serverName);
        bool CheckIdentity(string serverName, Guid token);
    }
}
