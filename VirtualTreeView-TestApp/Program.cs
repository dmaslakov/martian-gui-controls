// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VirtualTreeViewTestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
