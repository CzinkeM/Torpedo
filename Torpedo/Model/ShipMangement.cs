using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Torpedo
{
    public enum GameSize
    {
        Small,
        Medium,
        Large
    }
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    class ShipMangement
    {
        //private Draw _draw = new Draw();
        List<Ship> placedShips = new List<Ship>();
        public ShipMangement()
        {

        }
        public int[] InitilaizeShipCount(GameSize gameSize)
        {
            int[] shipCounts = new int[5];
            if (gameSize == GameSize.Small)
            {
                shipCounts = new int[] { 1, 1, 1, 1, 1 };
            }
            else if (gameSize == GameSize.Medium)
            {
                shipCounts = new int[] { 2, 2, 2, 2, 2 };
            }
            else//Large
            {
                shipCounts = new int[] { 3, 3, 3, 3, 3 };
            }
            return shipCounts;
        }
        /*
        public int[,] PlaceShipIntoMatrix(Canvas canvas,int[,] gameTable,Vector startingPos,Vector gameTableSize, int length, Direction dir, Label label)
        {
            int[,] modifiedArray = gameTable;
            int gameWidth = Convert.ToInt32(gameTableSize.X);
            int gameHeight = Convert.ToInt32(gameTableSize.Y);
            int x = Convert.ToInt32(startingPos.X);
            int y = Convert.ToInt32(startingPos.Y);
            switch (dir)
            {
                case Direction.Up:
                    {
                        if (startingPos.Y - (length - 1) >= 0)
                        {
                            List<bool> thereIsFreeSpace = new List<bool>();
                            for (int i = 0; i < length; i++)
                            {
                                if (modifiedArray[x, y - i] == 0)
                                {
                                    thereIsFreeSpace.Add(true);
                                }
                                else thereIsFreeSpace.Add(false);                               
                            }
                            if (thereIsFreeSpace.Contains(false))
                            {
                                label.Content = "False in the list";
                                return null;

                            }
                            else
                            {                              
                                for (int j = 0; j < length; j++)
                                {
                                    modifiedArray[x, y - j] = length;
                                    Vector pos = new Vector(startingPos.X, startingPos.Y - j);
                                    _draw.DrawPoint(canvas, gameWidth, gameHeight, pos, Brushes.Red, length.ToString());
                                }
                                label.Content = "Its ok";
                                return modifiedArray;
                            }
                        }
                        else
                        {
                            label.Content = "Out of range";
                            return null;
                        }   

                    }
                case Direction.Down:
                    {
                        if (startingPos.Y + (length - 1) <= modifiedArray.GetLength(1)-1)
                        {
                            List<bool> thereIsFreeSpace = new List<bool>();
                            for (int i = 0; i < length; i++)
                            {
                                if (modifiedArray[x, y + i] == 0)
                                {
                                    thereIsFreeSpace.Add(true);
                                }
                                else thereIsFreeSpace.Add(false);
                            }
                            if (thereIsFreeSpace.Contains(false))
                            {
                                label.Content = "False in the list";
                                return null;

                            }
                            else
                            {
                                for (int j = 0; j < length; j++)
                                {
                                    modifiedArray[x, y + j] = length;
                                    Vector pos = new Vector(startingPos.X, startingPos.Y + j);
                                    _draw.DrawPoint(canvas, gameWidth, gameHeight, pos, Brushes.Red, "id");
                                }
                                label.Content = "Its ok";
                                return modifiedArray;
                            }
                        }
                        else
                        {
                            label.Content = "Out of range";
                            return null;
                        }
                    }
                case Direction.Left:
                    {
                        if (startingPos.X - (length - 1) >= 0)
                        {
                            List<bool> thereIsFreeSpace = new List<bool>();
                            for (int i = 0; i < length; i++)
                            {
                                if (modifiedArray[x -i , y] == 0)
                                {
                                    thereIsFreeSpace.Add(true);
                                }
                                else thereIsFreeSpace.Add(false);
                            }
                            if (thereIsFreeSpace.Contains(false))
                            {
                                label.Content = "False in the list";
                                return null;

                            }
                            else
                            {
                                for (int j = 0; j < length; j++)
                                {
                                    modifiedArray[x - j, y] = length;
                                    Vector pos = new Vector(startingPos.X-j, startingPos.Y);
                                    _draw.DrawPoint(canvas, gameWidth, gameHeight, pos, Brushes.Red, "id");
                                }
                                label.Content = "Its ok";
                                return modifiedArray;
                            }
                        }
                        else
                        {
                            label.Content = "Out of range";
                            return null;
                        }
                    }
                case Direction.Right:
                    {
                        if (startingPos.X + (length - 1) <= gameTable.GetLength(0)-1)
                        {
                            List<bool> thereIsFreeSpace = new List<bool>();
                            for (int i = 0; i < length; i++)
                            {
                                if (gameTable[x+i, y] == 0)
                                {
                                    thereIsFreeSpace.Add(true);
                                }
                                else thereIsFreeSpace.Add(false);
                            }
                            if (thereIsFreeSpace.Contains(false))
                            {
                                label.Content = "False in the list";
                                return null;

                            }
                            else
                            {
                                for (int j = 0; j < length; j++)
                                {
                                    gameTable[x+j, y] = length;
                                    Vector pos = new Vector(startingPos.X+j, startingPos.Y);
                                    _draw.DrawPoint(canvas, gameWidth, gameHeight, pos, Brushes.Red, "id");
                                }
                                label.Content = "Its ok";
                                return modifiedArray;
                            }
                        }
                        else
                        {
                            label.Content = "Out of range";
                            return null;
                        }
                    }
                default: throw new Exception($"No direcetion specified ({dir})");
            }
        }
        */
        public Vector GetPointOnCanvas(Canvas gameCanvas, int width, int height)
        {
            var mousePosition = Mouse.GetPosition(gameCanvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < width; i++)
            {
                if (i * (gameCanvas.Width / width) < mousePositionX)
                {
                    lowerLimitX = i;
                }
            }
            for (int j = 0; j < height; j++)
            {
                if (j * (gameCanvas.Height / height) < mousePositionY)
                {
                    lowerLimitY = j;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            return tileVector;
        }
        /*
        public void DrawShipOnCanvas(Canvas gameCanvas, ref int[,] gameTable,ref int[] shipCounts,ref List<Ship> allShip,int width, int height, ShipName actualShipName,Direction dir,Label label)
        {
            Error error = new Error(label);
            Vector gameTableSize = new Vector(width, height);
            Ship shipToPaste = new Ship(GetPointOnCanvas(gameCanvas, width, height), actualShipName);
            allShip.Add(shipToPaste);
            int[,] arrayToCheck = gameTable;
            if (actualShipName == ShipName.Small && shipCounts[0] > 0)
            {
                var table = PlaceShipIntoMatrix(gameCanvas, gameTable, shipToPaste.startingPosition, gameTableSize, shipToPaste.length, dir, label);
                if (table != null)
                {
                    error.ShowErrorMessage(Error.ERROR_DELETE_MESSAGE);
                    gameTable = table;
                    shipCounts[0]--;
                }
                else  error.ShowErrorMessage(Error.ERROR_NO_SPACE);
            }
            else if (actualShipName == ShipName.Destoyer && shipCounts[1] > 0)
            {
                var table = PlaceShipIntoMatrix(gameCanvas, gameTable, shipToPaste.startingPosition, gameTableSize, shipToPaste.length, dir, label);
                if (table != null)
                {
                    gameTable = table;
                    shipCounts[1]--;
                    error.ShowErrorMessage(Error.ERROR_DELETE_MESSAGE);

                }
                else error.ShowErrorMessage(Error.ERROR_NO_SPACE);
            }
            else if (actualShipName == ShipName.Submarine && shipCounts[2] > 0)
            {
                var table = PlaceShipIntoMatrix(gameCanvas, gameTable, shipToPaste.startingPosition, gameTableSize, shipToPaste.length, dir, label);
                if (table != null)
                {
                    gameTable = table;
                    shipCounts[2]--;
                    error.ShowErrorMessage(Error.ERROR_DELETE_MESSAGE);
                }
                else error.ShowErrorMessage(Error.ERROR_NO_SPACE);
            }
            else if (actualShipName == ShipName.Carrier && shipCounts[3] > 0)
            {
                var table =  PlaceShipIntoMatrix(gameCanvas, gameTable, shipToPaste.startingPosition, gameTableSize, shipToPaste.length, dir, label);
                if (table != null)
                {
                    gameTable = table;
                    shipCounts[3]--;
                    error.ShowErrorMessage(Error.ERROR_DELETE_MESSAGE);
                }
                else error.ShowErrorMessage(Error.ERROR_NO_SPACE);
            }
            else if (actualShipName == ShipName.Battleship && shipCounts[4] > 0)
            {
                var table = PlaceShipIntoMatrix(gameCanvas, gameTable, shipToPaste.startingPosition, gameTableSize, shipToPaste.length, dir, label);
                if (table != null)
                {
                    gameTable = table;
                    shipCounts[4]--;
                    error.ShowErrorMessage(Error.ERROR_DELETE_MESSAGE);
                }
                else error.ShowErrorMessage(Error.ERROR_NO_SPACE);
            }
            else error.ShowErrorMessage(Error.ERROR_NO_MORE_SHIP);
        }
        */
        public void DeleteAllShip(Canvas canvas,ref int[,] array,ref int[] arrayShipCounts,int width,int height,GameSize gameSize)
        {
            canvas.Children.Clear();
            arrayShipCounts = InitilaizeShipCount(gameSize);
            array = new int[width,height];            
        }
    }
}
