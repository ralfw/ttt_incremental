using System;
using System.Linq;
using ttt.data;

namespace ttt.operation
{
    class Gamerules
    {
        private readonly Fieldselections _fieldselections;

        public Gamerules(Fieldselections fieldselections)
        {
            _fieldselections = fieldselections;
        }


        public Players Identify_current_player()
        {
            if (_fieldselections.Selections.Length == 0) return Players.X;
            return _fieldselections.Selections.Last().Player == Players.X ? Players.O : Players.X;
        }


        public void Check_for_end_of_game(Action onGameOn, Action<string> onGameOver)
        {
            if (_fieldselections.Selections.Count() < 9)
                onGameOn();
            else
                onGameOver("Game over! Both win ;-)");
        }
    }
}