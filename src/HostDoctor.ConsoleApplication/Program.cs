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
        private static readonly string ADDR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\visual studio 2013\Projects\HostDoctor\HostDoctor.Diagnostics.Exams\bin\Debug\HostDoctor.Diagnostics.Exams.dll";
        
        static void Main(string[] args)
        {
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var diagnosticsService = new DiagnosticsService();

            Console.WriteLine("Iniciando trabalho...");
            diagnosticsService.Start(new string[] { ADDR });

            Console.ReadLine();
            Console.WriteLine("Parando trabalho...");
            diagnosticsService.Stop();

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadLine();
        }
    }
}
