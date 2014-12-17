using System;

namespace ttt
{
    public class Interactions
    {
        public Board Start()
        {
            return New_game();
        }

        public Board New_game()
        {
            var board = new Board();
            board.Fieldvalues[0] = DateTime.Now.Second % 2 == 0 ? Fieldvalues.X : Fieldvalues.O;
            return board;
        }
    }
}