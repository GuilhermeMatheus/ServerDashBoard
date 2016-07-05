using HostDoctor.Diagnostics.Exams;
using HostDoctor.Diagnostics.Exams.Info;
using HostDoctor.Diagnostics.Exams.Performance;
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
            IExam exam = new HardDriveExam();
            while (true)
            {
                var arr = exam.GetResult().Information as IEnumerable;
                Console.Clear();

                foreach (var item in arr)
                    Console.WriteLine(item);
                
                Thread.Sleep(exam.GetPreferredUpdateTime());
            }
        }
    }
}
