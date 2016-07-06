using HostDoctor.Diagnostics.Exams.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Performance
{
    public class MemoryExam : IExam
    {
        public TimeSpan GetPreferredUpdateTime()
        {
            return TimeSpan.FromSeconds(1);
        }

        public ExamResult GetResult()
        {
            var available = ProcessStatusAPI.GetPhysicalAvailableMemoryInMiB();
            var totalMemory = ProcessStatusAPI.GetTotalMemoryInMiB();

            return ExamResultFactory.NativeExamResult(new { available, totalMemory });
        }
    }
}
