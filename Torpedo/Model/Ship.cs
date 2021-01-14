using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Torpedo
{
    public enum ShipType
    {
        Smallship,
        Destroyer,
        Submarine,
        Carrier,
        Battleship
    }
    //Neve+Id definiálja a hajókat
    public class Ship
    {
        public ShipType type;
        public int length;
        public Ship(ShipType shipType)
        {
            type = shipType;
            length = getLength(type);
        }
        private int getLength(ShipType ship)
        {
            int length;
            switch(ship)
            {
                case ShipType.Smallship: { length =1;break; }
                case ShipType.Destroyer: { length=2;break; }
                case ShipType.Submarine: { length=3;break; }
                case ShipType.Carrier: { length=4;break; }
                case ShipType.Battleship: { length=5;break; }
                default: { length = 0;break; }
            }
            return length;
        }        
    }
}
