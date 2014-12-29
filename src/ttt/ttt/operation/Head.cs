using System;
using System.Linq;
using System.Windows.Forms;
using ttt.data;
using ttt.integration;

namespace ttt.operation
{
    partial class Head : Form
    {
        private readonly Body _inter;
        private readonly Button[] _fieldbuttons;


        public Head(Body inter)
        {
            _inter = inter;
            _inter.Gamestate_changed += this.Display;

            InitializeComponent();

            _fieldbuttons = new[] {
                    btnField0, btnField1, btnField2,
                    btnField3, btnField4, btnField5,
                    btnField6, btnField7, btnField8
                };

            _inter.Start();
        }


        private void btnNewGame_Click(object sender, EventArgs e)
        {
            _inter.New_game();
        }

        private void btnField_clicked(object sender, EventArgs e)
        {
            var coord = _fieldbuttons.Select((b, i) => new {Button = b, Coord = i})
                                     .First(b => b.Button == sender)
                                     .Coord;
            _inter.Draw(coord);
        }


        public void Display(Gamestate gamestate)
        {
            for (int i = 0; i < gamestate.Board.Fieldvalues.Length; i++)
                _fieldbuttons[i].Text = gamestate.Board.Fieldvalues[i] == Fieldvalues.Empty ? " " : 
                                        gamestate.Board.Fieldvalues[i] == Fieldvalues.X ? "X" : "O";
            lblMessage.Text = gamestate.Message;
        }
    }
}
