using ttt.data;

namespace ttt
{
    class Interactions
    {
        private readonly Fieldselections _fieldselections;
        private readonly Gamerules _rules;

        public Interactions(Fieldselections fieldselections, Gamerules rules)
        {
            _fieldselections = fieldselections;
            _rules = rules;
        }

        public Gamestate Start()
        {
            return New_game();
        }


        public Gamestate New_game()
        {
            _fieldselections.Clear();
            var player = _rules.Identify_current_player();
            return Generate_gamestate_for_player(player);
        }


        public Gamestate Draw(int coordinate)
        {
            Gamestate gs = null;
            var player = _rules.Identify_current_player();
            _fieldselections.Append(player, coordinate,
                () => {
                    player = _rules.Identify_current_player();
                    gs = Generate_gamestate_for_player(player);
                },
                msg => { gs = Generate_gamestate_for_status(msg); });
            return gs;
        }


        #region mapping
        Gamestate Generate_gamestate_for_player(Players currentPlayer)
        {
            var msg = string.Format("Player: {0}", currentPlayer.ToString());
            return Generate_gamestate_for_status(msg);
        }

        Gamestate Generate_gamestate_for_status(string statusMsg)
        {
            var gs = new Gamestate();
            foreach (var t in _fieldselections.Selections)
                gs.Board.Fieldvalues[t.Coordinate] = t.Player == Players.X
                                                         ? Fieldvalues.X
                                                         : Fieldvalues.O;
            gs.Message = statusMsg;
            return gs;
        }
        #endregion
    }
}