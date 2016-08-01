using DashBoard.Data.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Services.Servers
{
    public class ServerTokenSecurity : IServerTokenSecurity
    {
        public ServerTokenSecurity()
        {

        }

        public bool CheckIdentity(string serverName, Guid token)
        {
            throw new NotImplementedException();
        }

        public Guid GenerateTokenFor(string serverName)
        {
            throw new NotImplementedException();
        }

        public bool IsWellKnownServer(string serverName)
        {
            throw new NotImplementedException();
        }
    }
}
