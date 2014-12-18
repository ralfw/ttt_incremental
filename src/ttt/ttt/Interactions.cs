using System;
using System.Collections.Generic;
using System.Linq;
using ttt.data;

namespace ttt
{
    public class Interactions
    {
        private List<FieldSelected> _fieldSelections;


        public Gamestate Start()
        {
            return New_game();
        }

        public Gamestate New_game()
        {
            Reset_fields();
            var player = Identify_current_player();
            return Generate_gamestate(player);
        }

        private void Reset_fields()
        {
            _fieldSelections = new List<FieldSelected>();
        }


        public Gamestate Draw(int coordinate)
        {
            var player = Identify_current_player();
            Place_symbol_on_field(player, coordinate);
            player = Identify_current_player();
            return Generate_gamestate(player);
        }

        private Players Identify_current_player()
        {
            if (_fieldSelections.Count == 0) return Players.X;
            return _fieldSelections.Last().Player == Players.X ? Players.O : Players.X;
        }


        void Place_symbol_on_field(Players player, int coordinate)
        {
            _fieldSelections.Add(new FieldSelected{Player = player, Coordinate = coordinate});
        }


        Gamestate Generate_gamestate(Players currentPlayer)
        {
            var gs = new Gamestate();
            foreach (var t in _fieldSelections)
                gs.Board.Fieldvalues[t.Coordinate] = t.Player == Players.X
                                                         ? Fieldvalues.X
                                                         : Fieldvalues.O;
            gs.Message = string.Format("Player: {0}", currentPlayer.ToString());
            return gs;
        }
    }
}