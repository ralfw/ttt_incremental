using System;
using ttt.data;

namespace ttt
{
    public class Interactions
    {
        public Gamestate Start()
        {
            return New_game();
        }

        public Gamestate New_game()
        {
            var gs = new Gamestate();
            gs.Board.Fieldvalues[0] = DateTime.Now.Second % 2 == 0 ? Fieldvalues.X : Fieldvalues.O;
            gs.Message = "Current player: " + (gs.Board.Fieldvalues[0] == Fieldvalues.X ? "O" : "X");
            return gs;
        }
    }
}