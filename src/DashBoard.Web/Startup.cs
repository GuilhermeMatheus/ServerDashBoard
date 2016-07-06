using DashBoard.Web.Listeners;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(DashBoard.Web.Startup))]
namespace DashBoard.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            (new ApiListener()).StartHubConnectionAsync();
        }
    }
}