using System;
using System.Collections.Generic;
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


        public void Check_for_win(Action onNoWin, Action<string> onWin)
        {
            Check_player_win(Players.X, 
                () => Check_player_win(Players.O, 
                    onNoWin,
                    onWin),
                onWin);
        }

        private void Check_player_win(Players player, Action onNoWin, Action<string> onWin)
        {
            var coords = Get_player_coordinates(player);
            Check_for_winning_lines(player, coords,
                onNoWin,
                onWin);
        }

        private IEnumerable<int> Get_player_coordinates(Players player)
        {
            return _fieldselections.Selections.Where(fs => fs.Player == player)
                                              .Select(fs => fs.Coordinate)
                                              .ToArray();
        }

        private readonly List<int[]> WINNING_LINES = new List<int[]> {
                new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
                new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
                new[]{0,4,8}, new[]{2,4,6}
            };

        private void Check_for_winning_lines(Players player, IEnumerable<int> coords, 
                                             Action onNoWin,
                                             Action<string> onWin)
        {
            if (WINNING_LINES.Any(wl => !wl.Except(coords).Any()))
                onWin("The winner: " + player);
            else
                onNoWin();
        }
    }
}