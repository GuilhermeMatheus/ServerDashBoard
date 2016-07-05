using DashBoard.API.Hubs;
using HostDoctor.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoard.API.Controllers
{
    public class ExamController : ApiControllerWithHub<ExamHub>
    {
        // PUT Exam/PutResult/5
        public void Put(ExamResult examResult)
        {
            Hub.Clients.All.newExamResult(examResult);
        }
    }
}
