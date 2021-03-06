﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics
{
    public interface IExam
    {
        Guid GetGuid();
        TimeSpan GetPreferredUpdateTime();
        ExamResult GetResult();
    }
}
