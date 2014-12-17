using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ttt.data;

namespace ttt
{
    public partial class Dialog : Form
    {
        private readonly Button[] _fieldbuttons;


        public Dialog()
        {
            InitializeComponent();

            _fieldbuttons = new[] {
                    btnField0, btnField1, btnField2,
                    btnField3, btnField4, btnField5,
                    btnField6, btnField7, btnField8
                };
        }


        private void btnNewGame_Click(object sender, EventArgs e)
        {
            On_new_game_requested();
        }

        private void btnField_clicked(object sender, EventArgs e)
        {
            var coord = _fieldbuttons.Select((b, i) => new {Button = b, Coord = i})
                                     .First(b => b.Button == sender)
                                     .Coord;
            On_player_drew(coord);
        }


        public void Display(Gamestate gamestate)
        {
            for (int i = 0; i < gamestate.Board.Fieldvalues.Length; i++)
                _fieldbuttons[i].Text = gamestate.Board.Fieldvalues[i] == Fieldvalues.Empty ? " " : 
                                        gamestate.Board.Fieldvalues[i] == Fieldvalues.X ? "X" : "O";
            lblMessage.Text = gamestate.Message;
        }


        public event Action On_new_game_requested;
        public event Action<int> On_player_drew;
    }
}
