using DashBoard.API.Hubs;
using DashBoard.API.Model;
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
        [HttpPost]
        public void Post(ExamResultModel examResult)
        {
            Debug.WriteLine(examResult);
            Hub.Clients.All.newExamResult(examResult);
        }
    }
}
