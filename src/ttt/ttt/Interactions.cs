using System;
using System.Collections.Generic;
using ttt.data;

namespace ttt
{
    public class Interactions
    {
        private List<int> _coordinates;


        public Gamestate Start()
        {
            return New_game();
        }

        public Gamestate New_game()
        {
            _coordinates = new List<int>();
            return Generate_gamestate();
        }

        public Gamestate Draw(int coordinate)
        {
            Place_symbol(coordinate);
            return Generate_gamestate();
        }


        void Place_symbol(int coordinate)
        {
            _coordinates.Add(coordinate);
        }


        Gamestate Generate_gamestate()
        {
            var gs = new Gamestate();
            for (var i = 0; i < _coordinates.Count; i++)
                gs.Board.Fieldvalues[_coordinates[i]] = i%2 == 0 ? Fieldvalues.X : Fieldvalues.O;
            gs.Message = string.Format("Player: {0}", _coordinates.Count%2 == 0 ? "X" : "O");
            return gs;
        }
    }
}