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

                if (lastHit.Count != 0)
                {
                    do
                    {
                        int[] lastInList = lastHit[lastHit.Count - 1];
                        list = PredictedShoots(lastInList, prevShots);
                        if (list.Count == 0)
                        {
                            lastHit.Remove(lastHit[lastHit.Count - 1]);
                        }
                    } while (lastHit.Count != 0 && list.Count == 0);
                    if (lastHit.Count == 0 && list.Count == 0)
                    {
                        int y = new Random().Next(0, 10);
                        int x = new Random().Next(0, 10);
                        newShot = new int[2] { y, x };
                    }
                    else
                    {
                        int index = new Random().Next(list.Count);
                        newShot = list[index];
                    }
                }
                else
                {
                    int y = new Random().Next(0, 10);
                    int x = new Random().Next(0, 10);
                    newShot = new int[2] { y, x };
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
}
