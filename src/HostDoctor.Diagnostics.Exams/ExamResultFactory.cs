using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams
{
    internal class ExamResultFactory
    {
        public static ExamResult NativeExamResult(object information, [CallerFilePath] string counterFilePath = null)
        {
            if (counterFilePath == null)
                throw new ArgumentNullException("counterFilePath");

            var counterName = Path.GetFileNameWithoutExtension(counterFilePath);
            return new ExamResult(information, counterName);
        }
    }
}
