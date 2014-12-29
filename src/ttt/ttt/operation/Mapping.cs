using ttt.data;

namespace ttt.operation
{
    class Mapping
    {
        private readonly Fieldselections _fieldselections;

        public Mapping(Fieldselections fieldselections)
        {
            _fieldselections = fieldselections;
        }


        public Gamestate To_gamestate_for(Players currentPlayer)
        {
            var msg = string.Format("Player: {0}", currentPlayer.ToString());
            return To_gamestate_for(msg);
        }

        public Gamestate To_gamestate_for(string statusMsg)
        {
            var gs = new Gamestate();
            foreach (var t in _fieldselections.Selections)
                gs.Board.Fieldvalues[t.Coordinate] = t.Player == Players.X
                                                         ? Fieldvalues.X
                                                         : Fieldvalues.O;
            gs.Message = statusMsg;
            return gs;
        }
    }
}