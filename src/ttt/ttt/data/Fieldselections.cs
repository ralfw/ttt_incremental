using System;
using System.Collections.Generic;
using System.Linq;

namespace ttt
{
    class Fieldselections
    {
        private List<FieldSelected> _fieldSelections;

        public Fieldselections() { Clear(); }


        public void Clear()
        {
            _fieldSelections = new List<FieldSelected>();
        }


        public void Append(Players player, int coordinate, 
                           Action onUnoccupied,
                           Action<string> onOccupied)
        {
            Check_for_occupied_field(coordinate,
                () => {
                    Place_symbol_on_field(player, coordinate);
                    onUnoccupied();
                },
                onOccupied);
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

        private void Place_symbol_on_field(Players player, int coordinate)
        {
            _fieldSelections.Add(new FieldSelected { Player = player, Coordinate = coordinate });
        }


        public FieldSelected[] Selections { get { return _fieldSelections.ToArray(); } }
    }
}