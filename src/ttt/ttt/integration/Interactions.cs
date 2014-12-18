using ttt.data;
using ttt.operation;

namespace ttt.integration
{
    class Interactions
    {
        private readonly Fieldselections _fieldselections;
        private readonly Gamerules _rules;
        private readonly Mapping _map;

        public Interactions(Fieldselections fieldselections, Gamerules rules, Mapping map)
        {
            _fieldselections = fieldselections;
            _rules = rules;
            _map = map;
        }


        public Gamestate Start()
        {
            return New_game();
        }


        public Gamestate New_game()
        {
            _fieldselections.Clear();
            var player = _rules.Identify_current_player();
            return _map.To_gamestate_for(player);
        }


        public Gamestate Draw(int coordinate)
        {
            Gamestate gs = null;
            var player = _rules.Identify_current_player();
            _fieldselections.Append(player, coordinate,
                () => {
                    player = _rules.Identify_current_player();
                    gs = _map.To_gamestate_for(player);
                },
                msg => { gs = _map.To_gamestate_for(msg); });
            return gs;
        }
    }
}