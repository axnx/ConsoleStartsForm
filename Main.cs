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

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hallo");
            Console.Out.WriteLine("ConsoleOUT");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        private void button2_Click(object sender, EventArgs e)
        {
            FreeConsole();
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void button3_Click(object sender, EventArgs e)
        {
            AllocConsole();
        }
    }
}
