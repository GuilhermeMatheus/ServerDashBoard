﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Info
{
    public class HardDriveExam : IExam
    {
        public TimeSpan GetPreferredUpdateTime()
        {
            return TimeSpan.FromSeconds(60);
        }

        public ExamResult GetResult()
        {
            var drives = DriveInfo
                            .GetDrives()
                            .Where(_ => _.IsReady)
                            .Select(_ => new { 
                                _.Name, 
                                _.TotalSize,
                                _.TotalFreeSpace, 
                                _.AvailableFreeSpace
                            });

            return ExamResultFactory.NativeExamResult(drives);
        }
    }
}