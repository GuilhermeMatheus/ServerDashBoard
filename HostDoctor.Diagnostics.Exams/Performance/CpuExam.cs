using HostDoctor.Diagnostics.Exams.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Performance
{
    public class CpuExam : PerformanceCounterWrapper, IExam 
    {
        public CpuExam() : base("Processor", "% Processor Time", "_Total") { }

        public TimeSpan GetPreferredUpdateTime()
        {
            return TimeSpan.FromSeconds(1);
        }

        public ExamResult GetResult()
        {
            var cpuUsage = GetLongValue();
            return ExamResultFactory.NativeExamResult(new { cpuUsage });
        }
    }
}
