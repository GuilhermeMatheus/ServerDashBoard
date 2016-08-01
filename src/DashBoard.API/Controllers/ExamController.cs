using DashBoard.API.Filters;
using DashBoard.API.Hubs;
using DashBoard.API.Models;
using HostDoctor.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoard.API.Controllers
{
    public class ExamController : ApiControllerWithHub<ExamHub>
    {
        [HttpPost, WellKnowMachinesFilter]
        public void Post(ExamResultModel examResult)
        {
            Hub.Clients.All.newExamResult(examResult);
        }
    }
}
