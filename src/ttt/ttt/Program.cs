using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ttt.data;
using ttt.integration;
using ttt.operation;

namespace ttt
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

            var fs = new Fieldselections();
            var gr = new Gamerules(fs);
            var map = new Mapping(fs);

            var inter = new Body(fs, gr, map);
            var dlg = new Head(inter);

            Application.Run(dlg);
        }
    }
}
