using System;
using System.Collections.Generic;
using System.Linq;

namespace TorpedoAI
{
    class AIPlayer
    {
        /*Ez az ai a generalt lepes valid lesz.*/
        public int[] AIShoots(ref List<int[]> lastHit, ref List<int[]> prevShots)
        {
            int[] newShot;

            do
            {
                if (lastHit.Count == 0)
                {
                    int y = new Random().Next(0, 10);
                    int x = new Random().Next(0, 10);
                    newShot = new int[2] { y, x };
                }
                else
                {
                    int[] minusOrPlus = { -1, 1 };
                    int randomMinusOrPlus = minusOrPlus[new Random().Next(0, 2)];

                    if (new Random().Next(0, 2) == 0)
                    {
                        int y = lastHit.Last()[0] + randomMinusOrPlus;
                        int x = lastHit.Last()[1];
                        newShot = new int[2] { y, x };
                    }
                    else
                    {
                        int y = lastHit.Last()[0];
                        int x = lastHit.Last()[1] + randomMinusOrPlus;
                        newShot = new int[2] { y, x };
                    }
                }
            } while (!ValidateShot(newShot, ref prevShots));

            if (lastHit.Count != 0)
            {
                lastHit.Last()[2] = lastHit.Last()[2] - 1;
                if (lastHit.Last()[2] == 0)
                {
                    lastHit.Remove(lastHit.Last());
                }
            }
            return newShot;
        }

        /*ValidateShot ellenorzi hogy az adott pozicioba lotunk e mar.*/
        public bool ValidateShot( int[] shot, ref List<int[]> prevShots)
        {
            foreach (var coordinates in prevShots)
            {
                if (coordinates[0] == shot[0]
                    && coordinates[1] == shot[1])
                {
                    return false;
                }
            }
            prevShots.Add(shot);
            return true;
        }
        /*WasItAHit metodus visszaadja hogy volt e talalat vagy sem.*/
        public bool WasItAHit(ref List<Ship> ships, int[] shot)
        {
            foreach (Ship ship in ships)
            {
                for (int i = 0; i < ship.Coordinates.Length/2; i++)
                {
                    if (ship.Coordinates[i, 0] == shot[0] && ship.Coordinates[i, 1] == shot[1])
                    {
                        ship.Size -= 1; 
                        return true;
                    }
                }
            }
            return false;
        }
        /*A Sinked methodus megvizsgalja hogy van e elsullyedt hajonk.*/
        public bool Sinked(ref List<Ship> ships)
        {
            foreach (Ship ship in ships)
            {
                if (ship.Size == 0)
                {
                    ships.Remove(ship);
                    return true;
                }
            }
            return false;
        }

        /*A round metodus csak ideiglenesen implementalja egy kornek a lefutasat*/
        //public void Round(List<Ship> ships, List<int[]> prevShots, List<int[]> lastHit)
        //{
        //    int[] aishot = AIShoots(ref lastHit, ref prevShots);
            
        //    if (WasItAHit(ref ships, aishot))
        //    {
        //        if (lastHit.Count == 0)
        //        {
        //            lastHit.Add(new int[3] { aishot[0], aishot[1], 4 });
        //        }
        //        else
        //        {
        //            lastHit.Add(new int[3] { aishot[0], aishot[1], 3 });
        //        }
        //        if (Sinked(ref ships))
        //        {
        //            Console.WriteLine("Talált, süllyedt.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Talált.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Nem Talált.");
        //    }

        //}

    }
}
