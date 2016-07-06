using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.Web.Hubs
{
    public class ClientExamHub : Hub
    {
        public void BroadcastExam(object exam)
        {
            Clients.All.handleNewExam(exam);
        }
    }
}