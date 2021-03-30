using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMan
{
    public partial class Form1 : Form
    {
        Timer loop = new Timer();
        Timer Timer99 = new Timer();
        public Form1()
        {
            InitializeComponent();
        }
        private void loopTick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            foreach(var t in processes)
            {
                
                if (!Processes.Items.Contains(t.ProcessName))
                {
                    Processes.Items.Add(t.ProcessName);
                }
               
            }
            progressBar1.Maximum = (int)PerformanceInfo.GetTotalMemoryInMiB();
            progressBar1.Value = (int)PerformanceInfo.GetTotalMemoryInMiB() - (int)PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            int kkm = (int)PerformanceInfo.GetTotalMemoryInMiB() - (int)PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            label1.Text = "RAM: " + kkm +"MB" + "/" + (int)PerformanceInfo.GetTotalMemoryInMiB() + "MB";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loop.Interval = 500;
            loop.Tick += new EventHandler(loopTick);
            loop.Start();
            Timer99.Tick += Timer99_Tick; // don't freeze the ui
            Timer99.Interval = 1024;
            Timer99.Start();
        }
        public PerformanceCounter myCounter =
            new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        public Int32 j = 0;
        public void Timer99_Tick(System.Object sender, System.EventArgs e)

        {
            //Console.Clear();
            j = Convert.ToInt32(myCounter.NextValue());
            //Console.WriteLine(j);
            label2.Text = "Disk: "+j+"%";
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            loop.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           DialogResult resl = MessageBox.Show(Form1.ActiveForm, "Are you sure you want to shut down the process: " + Processes.SelectedItem.ToString(), "Are you sure?", MessageBoxButtons.YesNo);
           if(resl == DialogResult.Yes)
            {
                Process[] proc = Process.GetProcessesByName(Processes.SelectedItem.ToString());
                foreach(var p in proc)
                {
                    if (p.ProcessName == "svchost" || p.ProcessName == "smss" || p.ProcessName == "csrss" || p.ProcessName == "wininit" || p.ProcessName == "logonui" || p.ProcessName == "lsass" || p.ProcessName == "services" || p.ProcessName == "winlogon" || p.ProcessName == "System")
                    {
                        MessageBox.Show("Cannot kill OS-critical processes");
                    }
                    else
                    {
                        //p.Kill();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DllInjectionResult resl = new DllInjector().Inject(Processes.SelectedItem.ToString(), textBox1.Text);
            if (resl != DllInjectionResult.Success)
            {
                MessageBox.Show("Error Injecting DLL: "+resl.ToString());
            }
        }
    }
    public static class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

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

        public static Int64 GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }

        public static Int64 GetTotalMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }
    }
    public enum DllInjectionResult
    {
        DllNotFound,
        GameProcessNotFound,
        InjectionFailed,
        Success
    }

    public sealed class DllInjector
    {
        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress,
            IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        static DllInjector _instance;

        public static DllInjector GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DllInjector();
                }
                return _instance;
            }
        }

        public DllInjector() { }

        public DllInjectionResult Inject(string sProcName, string sDllPath)
        {
            if (!File.Exists(sDllPath))
            {
                return DllInjectionResult.DllNotFound;
            }

            uint _procId = 0;

            Process[] _procs = Process.GetProcesses();
            for (int i = 0; i < _procs.Length; i++)
            {
                if (_procs[i].ProcessName == sProcName)
                {
                    _procId = (uint)_procs[i].Id;
                    break;
                }
            }

            if (_procId == 0)
            {
                return DllInjectionResult.GameProcessNotFound;
            }

            if (!bInject(_procId, sDllPath))
            {
                return DllInjectionResult.InjectionFailed;
            }

            return DllInjectionResult.Success;
        }

        bool bInject(uint pToBeInjected, string sDllPath)
        {
            IntPtr hndProc = OpenProcess((0x2 | 0x8 | 0x10 | 0x20 | 0x400), 1, pToBeInjected);

            if (hndProc == INTPTR_ZERO)
            {
                return false;
            }

            IntPtr lpLLAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (lpLLAddress == INTPTR_ZERO)
            {
                return false;
            }

            IntPtr lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)sDllPath.Length, (0x1000 | 0x2000), 0X40);

            if (lpAddress == INTPTR_ZERO)
            {
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0)
            {
                return false;
            }

            if (CreateRemoteThread(hndProc, (IntPtr)null, INTPTR_ZERO, lpLLAddress, lpAddress, 0, (IntPtr)null) == INTPTR_ZERO)
            {
                return false;
            }

            CloseHandle(hndProc);

            return true;
        }
    }
    }
