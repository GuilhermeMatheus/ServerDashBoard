using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using HostDoctor.Diagnostics.Service.Helpers;
using HostDoctor.Diagnostics.Service.TPL;
using System.Threading.Tasks.Dataflow;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HostDoctor.Diagnostics.Service
{
    public class DiagnosticsService : ServiceBase
    {
        private const string ADDR = @"C:\Users\svc.sharepointcptm\Documents\visual studio 2013\Projects\HostDoctor\HostDoctor.Diagnostics.Exams\bin\Debug\HostDoctor.Diagnostics.Exams.dll";
        private DoctorNeverEndTask[] doctors;

        protected override void OnStart(string[] args)
        {
            Start(args);
        }

        protected override void OnStop()
        {
            Stop();
        }

        public void Start(string[] args)
        {
            args = new string[] { ADDR };

            doctors = AssemblyLoader
                .GetTypesInAssemblies<IExam>(args)
                .Select(Activator.CreateInstance)
                .Select(_ => new DoctorNeverEndTask((IExam)_))
                .ToArray();

            foreach (var doctor in doctors)
            {
                doctor.AddActionBlock(GetActionsBlock());
                doctor.StartWork();
            }
        }

        private IEnumerable<ActionBlock<ExamResult>> GetActionsBlock()
        {
            //yield return new ActionBlock<ExamResult>(_ => Console.WriteLine(_));
            yield return new ActionBlock<ExamResult>(async _ => await NotifyDashBoardAPI(_));
        }

        public void Stop()
        {
            foreach (var doctor in doctors)
                doctor.StopWork();
        }

        private static async Task NotifyDashBoardAPI(ExamResult exam)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52766/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                await client.PostAsJsonAsync("api/exam", exam);
                //.RunSynchronously();
            }
        }
    }
}