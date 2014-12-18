namespace ttt.integration
{
    class App
    {
        private readonly Dialog _dlg;
        private readonly Interactions _inter;

        public App(Dialog dlg, Interactions inter)
        {
            _dlg = dlg;
            _inter = inter;

            _dlg.On_new_game_requested += () => {
                var gamestate = _inter.New_game();
                _dlg.Display(gamestate);
            };

            dlg.On_player_drew += coord => {
                var gamestate = inter.Draw(coord);
                dlg.Display(gamestate);
            };
        }


        public void Run()
        {
            var gamestate = _inter.Start();
            _dlg.Display(gamestate);
        }
    }
}