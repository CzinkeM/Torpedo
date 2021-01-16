using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Torpedo
{
    public class AIPlayer
    {

        public int[,] AIPlaceShips()
        {

            int[,] newAiShips = new int[10, 10];
            int[] freeDirection = new int[2];
            List<bool> down = new List<bool>();
            List<bool> right = new List<bool>();

            for (int i = 1; i <= 5; i++)
            {

                Vector anchor;

                do
                {

                    anchor = new Vector(new Random().Next(0, 10), new Random().Next(0, 10));
                    for (int j = 1; j < i; j++)
                    {
                        if (anchor.X + (i - 1) < 10 || anchor.Y + (i - 1) < 10)
                        {
                            if (anchor.X + (i - 1) < 10)//Lefele
                            {
                                if (newAiShips[Convert.ToInt32(anchor.X) + (i - 1), Convert.ToInt32(anchor.Y)] == 0)
                                {
                                    freeDirection[0] = 1;
                                    down.Add(true);
                                }
                                else down.Add(false);
                            }
                            if (anchor.Y + (i - 1) < 10)//Oldalara
                            {
                                if (newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y) + (i - 1)] == 0)
                                {
                                    freeDirection[1] = 1;
                                    right.Add(true);
                                }
                                else right.Add(false);
                            }
                        }

                    }
                    if (!down.Contains(false))
                    {
                        freeDirection[0] = 1;
                    }
                    if (!right.Contains(false))
                    {
                        freeDirection[1] = 1;
                    }

                } while (!((anchor.X + (i - 1) < 10
                            || anchor.Y + (i - 1) < 10) && (freeDirection[0] == 1 || freeDirection[1] == 1)));

                if (freeDirection[0] != 0 && freeDirection[1] != 0 )
                {
                    if (new Random().Next(0, 2) == 0)
                    {
                        newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y)] = i;
                        for (int j = 1; j < i; j++)
                        {
                            if ((Convert.ToInt32(anchor.X) + j) < 10)
                            {
                                newAiShips[Convert.ToInt32(anchor.X) + j, Convert.ToInt32(anchor.Y)] = i;
                            }
                        }
                    }
                    else
                    {
                        newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y)] = i;
                        for (int j = 1; j < i; j++)
                        {
                            if ((Convert.ToInt32(anchor.Y) + j) < 10)
                            {
                                newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y) + j] = i;
                            }
                        }
                    }
                }
                else if (freeDirection[0] != 0)
                {
                    newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y)] = i;
                    for (int j = 1; j < i; j++)
                    {
                        newAiShips[Convert.ToInt32(anchor.X) + j, Convert.ToInt32(anchor.Y)] = i;
                    }
                }
                else if (freeDirection[1] != 0)
                {
                    newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y)] = i;
                    for (int j = 1; j < i; j++)
                    {
                        newAiShips[Convert.ToInt32(anchor.X), Convert.ToInt32(anchor.Y) + j] = i;
                    }
                }

            }
            return newAiShips;
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
        public bool WasItAHit(ref List<ShipData> ships, int[] shot)
        {
            foreach (ShipData ship in ships)
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
        public bool Sinked(ref List<ShipData> ships)
        {
            foreach (ShipData ship in ships)
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
