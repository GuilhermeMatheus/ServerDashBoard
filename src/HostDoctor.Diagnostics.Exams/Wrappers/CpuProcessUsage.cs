using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CT = System.Runtime.InteropServices.ComTypes;

namespace HostDoctor.Diagnostics.Exams.Wrappers
{
    public class CpuProcessUsage
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemTimes(out CT.FILETIME lpIdleTime, out CT.FILETIME lpKernelTime, out CT.FILETIME lpUserTime);

        private readonly Process _process;

        private CT.FILETIME _prevSysKernel;
        private CT.FILETIME _prevSysUser;

        private TimeSpan _prevProcTotal = TimeSpan.MinValue;
        private Int16 _cpuUsage = -1;
        private DateTime _lastRun = DateTime.MinValue;
        private long _runCount = 0;

        public CpuProcessUsage(Process process)
        {
            _process = process;
            
            _prevSysUser.dwHighDateTime = 0;
            _prevSysUser.dwLowDateTime = 0;
            
            _prevSysKernel.dwHighDateTime = 0;
            _prevSysKernel.dwLowDateTime = 0;
        }

        public short GetUsage()
        {
            if (_process.HasExited)
                return -1;

            short cpuCopy = _cpuUsage;
            if (Interlocked.Increment(ref _runCount) == 1)
            {
                if (!EnoughTimePassed)
                {
                    Interlocked.Decrement(ref _runCount);
                    return cpuCopy;
                }

                CT.FILETIME sysIdle, sysKernel, sysUser;
                TimeSpan procTime;

                procTime = _process.TotalProcessorTime;

                if (!GetSystemTimes(out sysIdle, out sysKernel, out sysUser))
                {
                    Interlocked.Decrement(ref _runCount);
                    return cpuCopy;
                }

                if (!IsFirstRun)
                {
                    var sysKernelDiff = SubtractTimes(sysKernel, _prevSysKernel);
                    var sysUserDiff = SubtractTimes(sysUser, _prevSysUser);

                    var sysTotal = sysKernelDiff + sysUserDiff;

                    var procTotal = procTime.Ticks - _prevProcTotal.Ticks;

                    if (sysTotal > 0)
                        _cpuUsage = (short)((100.0 * procTotal) / sysTotal);
                }

                _prevProcTotal = procTime;
                _prevSysKernel = sysKernel;
                _prevSysUser = sysUser;

                _lastRun = DateTime.Now;

                cpuCopy = _cpuUsage;
            }

            Interlocked.Decrement(ref _runCount);
            return cpuCopy;
        }

        private UInt64 SubtractTimes(CT.FILETIME a, CT.FILETIME b)
        {
            var aInt = ((UInt64)(a.dwHighDateTime << 32)) | (UInt64)a.dwLowDateTime;
            var bInt = ((UInt64)(b.dwHighDateTime << 32)) | (UInt64)b.dwLowDateTime;

            return aInt - bInt;
        }

        private bool EnoughTimePassed
        {
            get
            {
                const int minimumElapsedMS = 250;
                TimeSpan sinceLast = DateTime.Now - _lastRun;
                return sinceLast.TotalMilliseconds > minimumElapsedMS;
            }
        }

        private bool IsFirstRun
        {
            get
            {
                return (_lastRun == DateTime.MinValue);
            }
        }
    }
}
