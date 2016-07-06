using DashBoard.Web.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DashBoard.Web.Listeners
{
    public class ApiListener
    {
        public async Task StartHubConnectionAsync()
        {
            var hubConnection = new HubConnection("http://localhost:52766/");
            
            var examHubProxy = hubConnection.CreateHubProxy("ExamHub");
            examHubProxy.On("newExamResult", _ => HandleNewExam(_));
            
            await hubConnection.Start();
        }

        private static void HandleNewExam(object exam)
        {
            Debug.WriteLine("WebApp received a new Exam {0}.", exam);
            var hub = GlobalHost.ConnectionManager.GetHubContext<ClientExamHub>();
            hub.Clients.All.handleNewExam(exam);
        }
    }
}