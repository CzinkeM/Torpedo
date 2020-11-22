using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Torpedo
{
    public enum ShipName
    {
        Small,
        Destoyer,
        Submarine,
        Carrier,
        Battleship
    }
    //Kezdő és végpont definiál egy hajót a mátrixban
    public class Ship
    {

        public Ship(Vector StartPosition,ShipName shipName)
        {
            startingPosition = StartPosition;
            shipType = shipName;
            switch (shipName)
            {
                case ShipName.Small: { length = 1; return; }
                case ShipName.Destoyer: { length = 2; return; }
                case ShipName.Submarine: { length = 3; return; }
                case ShipName.Carrier: { length = 4; return; }
                case ShipName.Battleship: { length = 5; return; }
            }
        }
        public Vector startingPosition { get; }
        public ShipName shipType { get; }
        public int length { get; }
        
        
        
    }
}
