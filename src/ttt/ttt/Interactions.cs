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
            return new Gamestate {Message = "Current player: X"};
        }

        public Gamestate Draw(int coordinate)
        {
            var gs = New_game();
            gs.Board.Fieldvalues[coordinate] = Fieldvalues.X;
            gs.Message = "Current player: O";
            return gs;
        }
    }
}