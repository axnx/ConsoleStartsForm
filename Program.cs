using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleStartsForm
{
    class Program
    {
        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SW_SHOW);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SW_HIDE);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        //[dllimport("kernel32.dll")]
        //static extern intptr getconsolewindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;



        static void Mainx(string[] args)
        {
            Console.WriteLine("Starting form");
            ConsoleStartsForm.Main mainform = new Main();
            // This will start the message loop, and show the mainform...
            HideConsoleWindow();
            Application.Run(mainform);
            ShowConsoleWindow();
        }


        [STAThread]
        static void Mainxx(string[] args)
        {

            if (args.Length > 0 && args[0] == "console")
            {
                Console.WriteLine("Hello world!");
                Console.ReadLine();
            }
            else {
                HideConsoleWindow();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }



        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcessId();

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr ProcessId);


        static int Mainxxx(string[] args)
        {
            IntPtr hConsole = GetConsoleWindow();
            IntPtr hProcessId = IntPtr.Zero;
            GetWindowThreadProcessId(hConsole, ref hProcessId);

            if (GetCurrentProcessId().Equals(hProcessId))
            {
                Console.WriteLine("I have my own console, press any key to exit");
                Console.ReadKey();
            }
            else
                Console.WriteLine("This console is not mine, good bye");

            return 0;
        }

        static int Main(string[] args)
        {
            IntPtr hConsole = GetConsoleWindow();
            IntPtr hProcessId = IntPtr.Zero;
            GetWindowThreadProcessId(hConsole, ref hProcessId);

            if (GetCurrentProcessId().Equals(hProcessId))
            {
                HideConsoleWindow();
                Application.Run(new Main());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            } 

            return 0;
        }
    }
}
