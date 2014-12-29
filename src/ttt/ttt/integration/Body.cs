using System;
using ttt.data;
using ttt.operation;

namespace ttt.integration
{
    class Body
    {
        private readonly Fieldselections _fieldselections;
        private readonly Gamerules _rules;
        private readonly Mapping _map;

        public Body(Fieldselections fieldselections, Gamerules rules, Mapping map)
        {
            _fieldselections = fieldselections;
            _rules = rules;
            _map = map;
        }


        public void Start()
        {
            New_game();
        }


        public void New_game()
        {
            _fieldselections.Clear();
            var player = _rules.Identify_current_player();
            var gs = _map.To_gamestate_for(player);
            Gamestate_changed(gs);
        }


        public void Draw(int coordinate)
        {
            Gamestate gs = null;
            var player = _rules.Identify_current_player();
            _fieldselections.Append(player, coordinate,
                () => _rules.Check_for_end_of_game(
                    () => _rules.Check_for_win(
                        () => {
                            player = _rules.Identify_current_player();
                            gs = _map.To_gamestate_for(player);
                        },
                        msg => { gs = _map.To_gamestate_for(msg); }),
                    msg => { gs = _map.To_gamestate_for(msg); }),
                msg => { gs = _map.To_gamestate_for(msg); });
            Gamestate_changed(gs);
        }


        public event Action<Gamestate> Gamestate_changed;
    }
}