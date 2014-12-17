using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            var dlg = new Dialog();
            var inter = new Interactions();

            var gamestate = inter.Start();
            dlg.Display(gamestate);

            dlg.On_new_game_requested += () => {
                gamestate = inter.New_game();
                dlg.Display(gamestate);
            };

            Application.Run(dlg);
        }
    }
}
