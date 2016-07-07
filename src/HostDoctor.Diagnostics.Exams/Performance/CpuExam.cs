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
            return this.NativeExamResult(new { cpuUsage });
        }

        public Guid GetGuid()
        {
            return new Guid("7a48ec01-1c28-402f-986f-c72dd7706c60");
        }
    }
}
