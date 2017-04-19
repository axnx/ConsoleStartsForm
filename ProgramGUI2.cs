using ConsoleStartsForm;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace ConsoleStartsForm
{
    static class ProgramGUI2
    {
        [STAThread]
        static void Main(string[] args)
        {

            IntPtr ptr = NativeMethods.GetForegroundWindow();
            int u;
            NativeMethods.GetWindowThreadProcessId(ptr, out u);
            Process process = Process.GetProcessById(u);

            if (args.Length > 0)
            {
                bool attached = true;
                // Command line given, display console
                AllocConsole();
                ConsoleMain(args);
                //if (attached)
                //{
                //    var hwnd = process.MainWindowHandle;
                //    NativeMethods.PostMessage(hwnd, NativeMethods.WM_KEYDOWN, NativeMethods.VK_RETURN, 0);
                //}
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }
        private static void ConsoleMain(string[] args)
        {
            Console.WriteLine("Command line = {0}", Environment.CommandLine);
            for (int ix = 0; ix < args.Length; ++ix)
                Console.WriteLine("Argument{0} = {1}", ix + 1, args[ix]);
            //Console.ReadLine();
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}