using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Process
{
    public class Toolhelp
    { 
        [DllImport("Toolhelp.dll")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("Coredll.dll")]
        public static extern int CloseHandle(IntPtr handle);
        [DllImport("Toolhelp.dll")]
        public static extern bool Process32First(IntPtr handle, ref PROCESSENTRY32 pe);
        [DllImport("Toolhelp.dll")]
        public static extern bool Process32Next(IntPtr handle, ref PROCESSENTRY32 pe);
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]//注意，此处为宽字符   
            public string szExeFile;
            public uint th32MemoryBase;
            public uint th32AccessKey;
        }

        public enum SnapShotFlags : uint
        {
            TH32CS_SNAPHEAPLIST = 0x00000001,
            TH32CS_SNAPPROCESS = 0x00000002,
            TH32CS_SNAPTHREAD = 0x00000004,
            TH32CS_SNAPMODULE = 0x00000008,
            TH32CS_SNAPALL = (TH32CS_SNAPHEAPLIST | TH32CS_SNAPPROCESS | TH32CS_SNAPTHREAD | TH32CS_SNAPMODULE),
            TH32CS_GETALLMODS = 0x80000000
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="ProcessName"></param>
        /// <returns></returns>
        public static bool KillProcess(string ProcessName)
        {
            try
            {
                IntPtr handle = Toolhelp.CreateToolhelp32Snapshot((uint)Toolhelp.SnapShotFlags.TH32CS_SNAPPROCESS, 0);
                if ((int)handle != -1)
                {
                    Toolhelp.PROCESSENTRY32 pe32 = new Toolhelp.PROCESSENTRY32();
                    pe32.dwSize = (uint)Marshal.SizeOf(typeof(Toolhelp.PROCESSENTRY32));
                    if (Toolhelp.Process32First(handle, ref pe32))
                    {
                        do
                        {
                            if (pe32.szExeFile == ProcessName)
                            {
                                //Console.WriteLine("\n-----------------------------------------------------");

                                //Console.WriteLine("\n  PROCESS NAME:     = {0}", pe32.szExeFile);

                                //Console.WriteLine("\n  parent process ID = {0}", pe32.th32ParentProcessID);

                                //Console.WriteLine("\n  process ID        = {0}", pe32.th32ProcessID);

                                //Console.WriteLine("\n  thread count      ={0}", pe32.cntThreads);

                                //Console.WriteLine("\n  Priority Base     = {0}", pe32.pcPriClassBase);

                                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById((int)pe32.th32ProcessID);
                                p.Kill();
                                return true;
                            }
                            //遍历获取下一个进程 
                        } while (Toolhelp.Process32Next(handle, ref pe32));

                        Toolhelp.CloseHandle(handle);
                    }

                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
