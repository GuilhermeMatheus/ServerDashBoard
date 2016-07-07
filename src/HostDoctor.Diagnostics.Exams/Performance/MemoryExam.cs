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

            return this.NativeExamResult(new { available, totalMemory });
        }

        public Guid GetGuid()
        {
            return new Guid("6117dfa5-4fa9-47d9-af4e-b39f3b27b34c");
        }
    }
}
