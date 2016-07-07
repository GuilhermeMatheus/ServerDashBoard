using HostDoctor.Diagnostics.Exams.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Info
{
    public class UpTimeExam : PerformanceCounterWrapper, IExam
    {
        public UpTimeExam() : base("System", "System Up Time", null) { }

        public TimeSpan GetPreferredUpdateTime()
        {
            return TimeSpan.FromSeconds(240);
        }

        public ExamResult GetResult()
        {
            var upTime = GetLongValue();
            return this.NativeExamResult(new { upTime });
        }

        public Guid GetGuid()
        {
            return new Guid("10348b2d-ad8d-4df5-9698-d6ca168f1e9e");
        }
    }
}
