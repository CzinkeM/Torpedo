using System;
using System.Collections.Generic;
using System.Linq;

namespace Torpedo
{
    public class AIPlayer
    {

        public List<Ship> AIPlaceShips()
        {
            List<Ship> ships = new List<Ship>() { new Ship(new int[5, 2], 5) ,
                                                  new Ship(new int[4, 2], 4) ,
                                                  new Ship(new int[3, 2], 3) ,
                                                  new Ship(new int[3, 2], 3) ,
                                                  new Ship(new int[2, 2], 2) };

            foreach (Ship ship in ships)
            {
                do
                {
                    ship.Coordinates.SetValue(new Random().Next(0, 10), 0, 0);
                    ship.Coordinates.SetValue(new Random().Next(0, 10), 0, 1);

                } while (!(ship.Coordinates[0, 0] + ship.Size - 1 < 10
                            || ship.Coordinates[0, 1] + ship.Size - 1 < 10));

                if (ship.Coordinates[0, 0] + ship.Size - 1 < 10
                    && ship.Coordinates[0, 1] + ship.Size - 1 < 10)
                {
                    if (new Random().Next(0, 2) == 0)
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            ship.Coordinates.SetValue(ship.Coordinates[0, 0], i, 0);
                            ship.Coordinates.SetValue(ship.Coordinates[0, 1] + i, i, 1);
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            ship.Coordinates.SetValue(ship.Coordinates[0, 0] + i, i, 0);
                            ship.Coordinates.SetValue(ship.Coordinates[0, 1], i, 1);
                        }
                    }
                }
                else
                {
                    int[] anchorCoordinates = new int[2] { ship.Coordinates[0, 0], ship.Coordinates[0, 1] };
                    int fixedCoordinateIndex = Array.IndexOf(anchorCoordinates, anchorCoordinates.Max());
                    int newCoordinateIndex = Array.IndexOf(anchorCoordinates, anchorCoordinates.Min());

                    for (int i = 1; i < ship.Size; i++)
                    {
                        ship.Coordinates.SetValue(ship.Coordinates[0, fixedCoordinateIndex], i, fixedCoordinateIndex);
                        ship.Coordinates.SetValue(ship.Coordinates[0, newCoordinateIndex] + i, i, newCoordinateIndex);
                    }
                }
            }
            return ships;
        }

        /*Ez az ai, a generalt lepes valid lesz.*/
        public int[] AIShoots(ref List<int[]> lastHit, ref List<int[]> prevShots)
        {
            int[] newShot;
            List<int[]> list;
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
                    do
                    {
                        list = PredictedShoots(lastHit[lastHit.Count - 1], prevShots) ;
                        if (list.Count == 0)
                        {
                            lastHit.Remove(lastHit[lastHit.Count - 1]);

                        }
                    } while (list.Count == 0);

                    int index = new Random().Next(list.Count);
                    newShot = list[index];

                }
            } while (!ValidateShot(newShot, ref prevShots));
   
            return newShot;
        }

        public List<int[]> PredictedShoots(int[] lastHit, List<int[]> prevShots)
        {

            List<int[]> predictedShoots = new List<int[]>() { new int[]{ lastHit[0] - 1, lastHit[1] },
                                                             new int[]{ lastHit[0] + 1, lastHit[1] },
                                                             new int[]{ lastHit[0], lastHit[1] - 1 },
                                                             new int[]{ lastHit[0], lastHit[1] + 1 } };

            List<int[]> validPredictedShoots = new List<int[]>() ;
            bool notFound = true;
            foreach (int[] item in predictedShoots)
            {

                foreach (int[] prevShot in prevShots)
                {

                    if ((item[0] == prevShot[0] && item[1] == prevShot[1])
                        || ((item[0]<=0 || item[0]>=9) || (item[1] <= 0 || item[1] >= 9)))
                    {
                        notFound = false;
                    }
                }
                if (notFound)
                {
                    validPredictedShoots.Add(item);
                }
                notFound = true;
            }
            return validPredictedShoots;
        }

        /*ValidateShot ellenorzi hogy az adott pozicioba lotunk e mar.*/
        public bool ValidateShot( int[] shot, ref List<int[]> prevShots)
        {
            if((shot[0] < 0 && shot[0] > 9) 
                || (shot[1] < 0 && shot[1] > 9))
            {
                return false;
            }
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
