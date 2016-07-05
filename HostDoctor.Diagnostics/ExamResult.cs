using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics
{
    public class ExamResult
    {
        public string MachineName { get; private set; }
        public object Information { get; private set; }

        public ExamResult(object information) :
            this(information, Environment.MachineName) { }

        public ExamResult(object information, string machineName)
        {
            Information = information;
            MachineName = machineName;
        }

        public override string ToString()
        {
            return String.Format("{0} @{1}", Information, MachineName).Trim();
        }
    }
}
