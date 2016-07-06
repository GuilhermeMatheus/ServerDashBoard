using HostDoctor.Diagnostics.Exams.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Performance
{
    public class ProcessStatusExam : IExam
    {
        ProcessWrap[] _processes;

        class ProcessWrap
        {
            public string ProcessName { get; set; }
            public long WorkingSet64 { get; set; }
            public CpuProcessUsage CpuUsage { get; set; }
        }

        public ProcessStatusExam()
        {
            UpdateProcesses();
        }

        private void UpdateProcesses()
        {
            _processes = Process
                .GetProcesses()
                .Where(_ => _.Id > 4)
                .Select(_ => new ProcessWrap
                {
                    ProcessName = _.ProcessName,
                    WorkingSet64 = _.WorkingSet64,
                    CpuUsage = new CpuProcessUsage(_)
                })
                .ToArray();
        }

        public TimeSpan GetPreferredUpdateTime()
        {
            return TimeSpan.FromSeconds(1);
        }

        public ExamResult GetResult()
        {
            var processes = _processes
                                .Select(_ => new {
                                    _.ProcessName,
                                    _.WorkingSet64,
                                    Usage = _.CpuUsage.GetUsage()
                                })
                                .Where(_ => _.Usage > 0)
                                .OrderByDescending(_ => _.Usage)
                                .ToArray();
            
            return ExamResultFactory.NativeExamResult(processes);
        }
    }
}