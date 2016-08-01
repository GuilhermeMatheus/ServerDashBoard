using DashBoard.Data.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DashBoard.API.Filters
{
    public enum WellKnowMachinesFilterStatus
    {
        TokenNotProvided = 1,
        UnknownServer = 2,
        WrongOrExpiredToken = 3
    }

    public class WellKnowMachinesFilterAttribute : ActionFilterAttribute
    {
        public IServerTokenSecurity ServerTokenSecurity { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var requestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            var headers = request.Headers;

            var token = headers["AuthToken"];
            var serverName = headers["ServerName"];
            
            if (serverName == null) //TODO: Definitivamente, fazer isso melhor
                throw new HttpRequestException("ServerName argument not provided");
            
            if (token == null)
            {
                var isWellKnown = !ServerTokenSecurity.IsWellKnownServer(serverName);
                if (isWellKnown)
                    filterContext.Result = new JsonResult {
                        Data = new {
                            successful = false,
                            status = WellKnowMachinesFilterStatus.TokenNotProvided,
                            message = "Token not provided"
                        }
                    };
                else
                    filterContext.Result = new JsonResult {
                        Data = new {
                            successful = false,
                            status = WellKnowMachinesFilterStatus.UnknownServer,
                            message = "Unknown server"
                        }
                    };

                return;
            }

            if(!ServerTokenSecurity.CheckIdentity(serverName, new Guid(token)))
            {
                filterContext.Result = new JsonResult {
                    Data = new {
                        successful = false,
                        status = WellKnowMachinesFilterStatus.WrongOrExpiredToken,
                        message = "Wrong or expired token"
                    }
                };

                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}