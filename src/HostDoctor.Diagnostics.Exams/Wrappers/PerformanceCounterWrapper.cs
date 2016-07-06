using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Wrappers
{
    public class PerformanceCounterWrapper
    {
        private readonly PerformanceCounter _performanceCounter;

        public PerformanceCounterWrapper(string categoryName, string counterName, string instanceName)
        {
            _performanceCounter = new PerformanceCounter {
                CategoryName = categoryName,
                CounterName = counterName,
                InstanceName = instanceName
            };

            _performanceCounter.NextValue();
        }
    
        public float GetValue()
        {
            return _performanceCounter.NextValue();
        }

        public long GetLongValue()
        {
            return Convert.ToInt64(GetValue());
        }
    }
}
