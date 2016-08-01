using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBoard.API.Models
{
    public class ExamResultModel
    {
        public string CounterName { get; set; }
        public string MachineName { get; set; }
        public object Information { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1} @{2}", CounterName, Information, MachineName);
        }
    }
}