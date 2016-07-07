using HostDoctor.Diagnostics.Exams.Performance;
using HostDoctor.Diagnostics.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostDoctor.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var addr = typeof(CpuExam).Assembly.Location;

            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var diagnosticsService = new DiagnosticsService();

            Console.WriteLine("Iniciando trabalho... " + addr);
            var workers = diagnosticsService.Start(new string[] { addr });
            Console.WriteLine(workers + " workers");

            Console.ReadLine();
            Console.WriteLine("Parando trabalho...");
            diagnosticsService.Stop();

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadLine();
        }
    }
}
