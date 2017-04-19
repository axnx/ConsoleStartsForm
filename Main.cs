using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleStartsForm
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hallo");
            Console.Out.WriteLine("ConsoleOUT");
            logger.Trace("Hello - Trace"); //Will log
            logger.Debug("Hello - Debug"); //Will log
            logger.Info("Hello - Info");   //Will log
            logger.Warn("Hello - Warn");   //Will log
            logger.Error("Hello - Error"); //Will log
            logger.Fatal("Hello - Fatal"); //Will log
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        private void button2_Click(object sender, EventArgs e)
        {
            //FreeConsole();
            Program.HideConsoleWindow();
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void button3_Click(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.ShowConsoleWindow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConfigNlogForms.config();
            logger.Trace("Hello - Trace"); //Will log
            logger.Debug("Hello - Debug"); //Will log
            logger.Info("Hello - Info");   //Will log
            logger.Warn("Hello - Warn");   //Will log
            logger.Error("Hello - Error"); //Will log
            logger.Fatal("Hello - Fatal"); //Will log
        }
    }
}
