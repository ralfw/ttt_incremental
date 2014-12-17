using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public void Display(Board board)
        {
            for (int i = 0; i < board.Fieldvalues.Length; i++)
                _fieldbuttons[i].Text = board.Fieldvalues[i] == Fieldvalues.Empty ? " " : 
                                        board.Fieldvalues[i] == Fieldvalues.X ? "X" : "O";
        }
    }
}
