using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo
{
    public class Ship
    {
        public Ship(int[,] coordinates, int size)
        {
            Coordinates = coordinates;
            Size = size;
        }

        public int[,] Coordinates {get; set;  }

        public int Size { get; set; }

    }
}
