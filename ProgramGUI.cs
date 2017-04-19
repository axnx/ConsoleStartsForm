using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleStartsForm
{

    //http://stackoverflow.com/questions/1305257/using-attachconsole-user-must-hit-enter-to-get-regular-command-line
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        internal static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        internal const int VK_RETURN = 0x0D;
        internal const int WM_KEYDOWN = 0x100;
    }

    class ProgramGUI
    {

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow")]
        private static extern IntPtr _GetConsoleWindow();

        //[DllImport("kernel32.dll")]
        //static extern bool FreeConsole();

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool AllocConsole();


        //// Attaches the calling process to the console of the specified process.
        //// http://msdn.microsoft.com/en-us/library/ms681952%28v=vs.85%29.aspx
        //[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        //private static extern bool AttachConsole(int processId);

        [STAThread]
        static void Main(string[] args)
        {
            bool attached = false;

            // Get uppermost window process
            IntPtr ptr = NativeMethods.GetForegroundWindow();
            int u;
            NativeMethods.GetWindowThreadProcessId(ptr, out u);
            //

            Process process = Process.GetProcessById(u);


            //var childPidToParentPid = GetAllProcessParentPids();
            //int currentProcessId = Process.GetCurrentProcess().Id;

            //Console.WriteLine("Current Process ID: " + currentProcessId);
            //Console.WriteLine("Parent Process ID: " + childPidToParentPid[currentProcessId]);
            //MessageBox.Show(childPidToParentPid[currentProcessId].ToString());


            if (args.Length == 0)
            {
                //FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                var consoleHandle = _GetConsoleWindow();
                // run GUI
                //if (consoleHandle == IntPtr.Zero || AppDomain.CurrentDomain.FriendlyName.Contains(".vshost")) {
                if (AppDomain.CurrentDomain.FriendlyName.Contains(".vshost"))
                {
                MessageBox.Show("Visual Studio Call or consoleHandle? ");
                    // we either have no console window or we're started from within visual studio
                    // This is the form I usually run. Change it to match your code.
                    Application.Run(new Main());
                }
                else
                {

                    // command line given, display console
                    //if (!NativeMethods.AttachConsole(-1))
                    //{
                    //    // attach to a parent process console
                    //    NativeMethods.AllocConsole(); // alloc a new console if none available
                    //    ConsoleMain(args);
                    //    attached = true;
                    //}

                    if (string.Compare(process.ProcessName, "cmd", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        // attach to the current active console
                        //MessageBox.Show("attach" + process.Id.ToString());
                        NativeMethods.AttachConsole(process.Id);
                        attached = true;
                        //Console.Write("your output");
                    }
                    else
                    {
                        // create new console
                        NativeMethods.AllocConsole();
                    }

                    //Console.Write("your output");
                    ConsoleMain(args);


                    NativeMethods.FreeConsole();
                    if (attached)
                    {
                        var hwnd = process.MainWindowHandle;
                        NativeMethods.PostMessage(hwnd, NativeMethods.WM_KEYDOWN, NativeMethods.VK_RETURN, 0);
                       
                    }
                }

                //Console.WriteLine("console found");
                //// we found a console attached to us, so restart ourselves without one
                //Process.Start(new ProcessStartInfo(Assembly.GetEntryAssembly().Location)
                //{
                //    CreateNoWindow = true,
                //    UseShellExecute = false
                //});
                //MessageBox.Show("Else");
            

            }

                

        }
        
        private static void ConsoleMain(string[] args)
        {
            Console.WriteLine("Console party");
            Console.WriteLine("Command line = {0}", Environment.CommandLine);
            Console.WriteLine("Exe path = {0}", args[0]);
            for (int ix = 1; ix <= args.Length - 1; ix++)
            {
                Console.WriteLine("  Argument({0}) = {1}", ix, args[ix]);
            }
            Console.WriteLine("Press ENTER to continue");
            Console.WriteLine("cls");
            //FreeConsole();
            //works only if console is in foreground
            //System.Windows.Forms.SendKeys.SendWait("{ENTER}");

            

            // http://msdn.microsoft.com/en-us/library/ms597014(v=vs.110).aspx
            //Application.Exit();

            // http://msdn.microsoft.com/en-us/library/system.environment.exit.aspx
            //Environment.Exit(0);  // <-- a bit redundant however



            //Console.ReadKey();
            //string s = Console.ReadLine();
        }

        public static Dictionary<int, int> GetAllProcessParentPids()
        {
            var childPidToParentPid = new Dictionary<int, int>();

            var processCounters = new SortedDictionary<string, PerformanceCounter[]>();
            var category = new PerformanceCounterCategory("Process");

            // As the base system always has more than one process running, 
            // don't special case a single instance return.
            var instanceNames = category.GetInstanceNames();
            foreach (string t in instanceNames)
            {
                try
                {
                    processCounters[t] = category.GetCounters(t);
                }
                catch (InvalidOperationException)
                {
                    // Transient processes may no longer exist between 
                    // GetInstanceNames and when the counters are queried.
                }
            }

            foreach (var kvp in processCounters)
            {
                int childPid = -1;
                int parentPid = -1;

                foreach (var counter in kvp.Value)
                {
                    if ("ID Process".CompareTo(counter.CounterName) == 0)
                    {
                        childPid = (int)(counter.NextValue());
                    }
                    else if ("Creating Process ID".CompareTo(counter.CounterName) == 0)
                    {
                        parentPid = (int)(counter.NextValue());
                    }
                }

                if (childPid != -1 && parentPid != -1)
                {
                    childPidToParentPid[childPid] = parentPid;
                }
            }

            return childPidToParentPid;
        }
    }
}
