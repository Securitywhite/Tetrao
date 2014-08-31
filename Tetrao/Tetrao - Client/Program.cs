using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetrao___Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Main
                {
                    Email = args[0],
                    Password = args[1],
                    Hotel = args[2]
                });
            }
            catch
            {
                MessageBox.Show("Invalid Arguments");
                Environment.Exit(0);
            }
        }
    }
}
