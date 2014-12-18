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
            return Generate_gamestate_for_player(player);
        }

        private void Reset_fields()
        {
            _fieldSelections = new List<FieldSelected>();
        }


        public Gamestate Draw(int coordinate)
        {
            Gamestate gs = null;
            Check_for_occupied_field(coordinate,
                () => {
                    var player = Identify_current_player();
                    Place_symbol_on_field(player, coordinate);
                    player = Identify_current_player();
                    gs = Generate_gamestate_for_player(player);
                },
                msg => { gs = Generate_gamestate_for_status(msg); });
            return gs;
        }

        private void Check_for_occupied_field(int coordinate,
                                              Action onUnoccupied,
                                              Action<string> onOccupied)
        {
            if (_fieldSelections.Any(fs => fs.Coordinate == coordinate))
                onOccupied("Invalid selection!");
            else
                onUnoccupied();
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


        Gamestate Generate_gamestate_for_player(Players currentPlayer)
        {
            var msg = string.Format("Player: {0}", currentPlayer.ToString());
            return Generate_gamestate_for_status(msg);
        }

        Gamestate Generate_gamestate_for_status(string statusMsg)
        {
            var gs = new Gamestate();
            foreach (var t in _fieldSelections)
                gs.Board.Fieldvalues[t.Coordinate] = t.Player == Players.X
                                                         ? Fieldvalues.X
                                                         : Fieldvalues.O;
            gs.Message = statusMsg;
            return gs;
        }
    }
}