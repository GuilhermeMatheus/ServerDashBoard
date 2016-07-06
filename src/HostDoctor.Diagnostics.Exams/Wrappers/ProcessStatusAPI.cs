using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Exams.Wrappers
{
    public static class ProcessStatusAPI
    {
        private const int ONE_MB_IN_BYTES = 1048576;
        
        public static Int64 GetPhysicalAvailableMemoryInMiB()
        {
            var pi = new PerformanceInformation();
            return GetPerformanceInfo(out pi, Marshal.SizeOf(pi))
                ? ConvertPhysicalPtrToMegaBytes(pi.PhysicalAvailable, pi.PageSize)
                : -1;
        }
        public static Int64 GetTotalMemoryInMiB()
        {
            var pi = new PerformanceInformation();
            return GetPerformanceInfo(out pi, Marshal.SizeOf(pi))
                ? ConvertPhysicalPtrToMegaBytes(pi.PhysicalTotal, pi.PageSize)
                : -1;
        }

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

        private static Int64 ConvertPhysicalPtrToMegaBytes(IntPtr physical, IntPtr pageSize)
        {
            return Convert.ToInt64(physical.ToInt64() * pageSize.ToInt64() / ONE_MB_IN_BYTES);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PerformanceInformation
    {
        public int Size;
        public IntPtr CommitTotal;
        public IntPtr CommitLimit;
        public IntPtr CommitPeak;
        public IntPtr PhysicalTotal;
        public IntPtr PhysicalAvailable;
        public IntPtr SystemCache;
        public IntPtr KernelTotal;
        public IntPtr KernelPaged;
        public IntPtr KernelNonPaged;
        public IntPtr PageSize;
        public int HandlesCount;
        public int ProcessCount;
        public int ThreadCount;
    }
}
