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

            var fs = new Fieldselections();
            var gr = new Gamerules(fs);
            var inter = new Interactions(fs, gr);
            var dlg = new Dialog();

            var gamestate = inter.Start();
            dlg.Display(gamestate);

            dlg.On_new_game_requested += () => {
                gamestate = inter.New_game();
                dlg.Display(gamestate);
            };

            dlg.On_player_drew += coord => {
                gamestate = inter.Draw(coord);
                dlg.Display(gamestate);
            };

            Application.Run(dlg);
        }
    }
}
