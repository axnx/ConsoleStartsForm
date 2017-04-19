using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleStartsForm
{
    class ProgramGUI
    {

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
                Console.WriteLine("Console party");
            }
    }
}
