using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics
{
    public class ExamResult
    {
        public string CounterName { get; private set; }
        public string MachineName { get; private set; }
        public object Information { get; private set; }

        public ExamResult(object information, string counterName) :
            this(information, counterName, Environment.MachineName) { }

        public ExamResult(object information, string counterName, string machineName)
        {
            CounterName = counterName;
            Information = information;
            MachineName = machineName;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1} @{2}", CounterName, Information, MachineName);
        }
    }
}
