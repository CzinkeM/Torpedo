using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Torpedo.View;

namespace Torpedo.ViewModel
{
    enum Dir
    {
        up,
        down,
        left,
        right
    }
    class PickerViewModel
    {
        public const string prop_playerArray = "1stPlayerArray";
        public const string prop_2ndPlayerArray = "2ndPlayerArray";
        private Canvas _canvas;
        private int _width;
        private int _height;
        private Draw _draw;
        public int[,] shipMatrix;
        public int[] shipCounts = new int[] { 1, 1, 1, 1, 1 };
        public string[] shipNames = new string[] { "Smallship","Destroyer","Submarine","Carrier","Battleship" };
        private Label _toShowMessage;
        private FirstPlayerPicker _window;
        private bool secondPlayerPicked = false;

        public int[,] firstArrayFinal;
        public int[,] secondArrayFinal;

        public PickerViewModel(Canvas canvas,int width, int height, Label message, FirstPlayerPicker window)
        {
            shipMatrix = new int[width, height];
            _canvas = canvas;
            _width = width;
            _height = height;
            _draw = new Draw(canvas, _width, _height);
            _toShowMessage = message;
            _window = window;

        }
        public Vector GetPointOnCanvas()
        {
            var mousePosition = Mouse.GetPosition(_canvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < _width; i++)
            {
                if (i * (_canvas.Width / _width) < mousePositionX)
                {
                    lowerLimitX = i;
                }
            }
            for (int j = 0; j < _height; j++)
            {
                if (j * (_canvas.Height / _height) < mousePositionY)
                {
                    lowerLimitY = j;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            return tileVector;
        }
        
        public void PlaceShip(Ship ship, ref int[] counts, Vector clickedCordinates,Dir actualDirection ,ShipType actualShipType)//getPointOncanvas-t kell passolni neki
        {
            PlaceShipToArray(actualDirection,clickedCordinates,shipMatrix,ship,actualShipType,counts);
        }
        private int[,] PlaceShipToArray(Dir direction,Vector vector,int[,] ShipMatrix, Ship ship,ShipType actualShipType, int[] counts)
        {
            int[,] modifiedShipMatrix = shipMatrix;
            int x = Convert.ToInt32(vector.X);
            int y = Convert.ToInt32(vector.Y);
            switch (direction)
            {
                case Dir.up: 
                    {
                        if (vector.Y - (ship.length - 1) >= 0)
                        {
                            List<bool> freeSpace = new List<bool>();
                            for (int i = 0; i < ship.length; i++)
                            {
                                if (modifiedShipMatrix[x, y - i] == 0)
                                {
                                    freeSpace.Add(true);
                                }
                                else freeSpace.Add(false);
                            }
                            if (!freeSpace.Contains(false) && EnoughShipToPlace(actualShipType,ref counts))
                            {
                                for (int i = 0; i < ship.length; i++)
                                {
                                    modifiedShipMatrix[x, y - i] = ship.length;
                                    Vector shipPosition = new Vector(x, y - i);
                                    _draw.DrawPoint(shipPosition, Brushes.Red, ship.length.ToString());
                                }return modifiedShipMatrix;
                            }
                            else if (freeSpace.Contains(false))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_SPACE;
                                return null;
                            }
                            else if (!EnoughShipToPlace(actualShipType, ref counts))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_MORE_SHIP;
                                return null;
                            }
                            else return null;
                        }
                        else return null; 
                    }
                case Dir.down: 
                    { 
                        if(vector.Y+(ship.length-1)<=ShipMatrix.GetLength(1)-1)
                        {
                            List<bool> freeSpace = new List<bool>();
                            for (int i = 0; i < ship.length; i++)
                            {
                                if (modifiedShipMatrix[x, y + i] == 0)
                                {
                                    freeSpace.Add(true);
                                }
                                else freeSpace.Add(false);
                            }
                            if (!freeSpace.Contains(false) && EnoughShipToPlace(actualShipType, ref counts))
                            {
                                for (int i = 0; i < ship.length; i++)
                                {
                                    modifiedShipMatrix[x, y + i] = ship.length;
                                    Vector shipPosition = new Vector(x, y + i);
                                    _draw.DrawPoint(shipPosition, Brushes.Red, ship.length.ToString());
                                }
                                return modifiedShipMatrix;
                            }
                            else if (freeSpace.Contains(false))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_SPACE;
                                return null;
                            }
                            else if (!EnoughShipToPlace(actualShipType, ref counts))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_MORE_SHIP;
                                return null;
                            }
                            else return null;

                        }
                        else return null; 
                    }
                case Dir.left: 
                    {
                        if(vector.X-(ship.length-1)>=0)
                        {
                            List<bool> freeSpace = new List<bool>();
                            for (int i = 0; i < ship.length; i++)
                            {
                                if (modifiedShipMatrix[x-i, y] == 0)
                                {
                                    freeSpace.Add(true);
                                }
                                else freeSpace.Add(false);
                            }
                            if (!freeSpace.Contains(false) && EnoughShipToPlace(actualShipType, ref counts))
                            {
                                for (int i = 0; i < ship.length; i++)
                                {
                                    modifiedShipMatrix[x - i, y] = ship.length;
                                    Vector shipPosition = new Vector(x - i, y);
                                    _draw.DrawPoint(shipPosition, Brushes.Red, ship.length.ToString());
                                }
                                return modifiedShipMatrix;
                            }
                            else if (freeSpace.Contains(false))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_SPACE;
                                return null;
                            }
                            else if (!EnoughShipToPlace(actualShipType, ref counts))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_MORE_SHIP;
                                return null;
                            }
                            else return null;
                        }
                        else return null; 
                    }
                case Dir.right: 
                    {
                        if(vector.X+(ship.length-1)<=ShipMatrix.GetLength(0)-1)
                        {
                            List<bool> freeSpace = new List<bool>();
                            for (int i = 0; i < ship.length; i++)
                            {
                                if (modifiedShipMatrix[x + i, y] == 0)
                                {
                                    freeSpace.Add(true);
                                }
                                else freeSpace.Add(false);
                            }
                            if (!freeSpace.Contains(false) && EnoughShipToPlace(actualShipType, ref counts))
                            {
                                for (int i = 0; i < ship.length; i++)
                                {
                                    modifiedShipMatrix[x + i, y] = ship.length;
                                    Vector shipPosition = new Vector(x + i, y);
                                    _draw.DrawPoint(shipPosition, Brushes.Red, ship.length.ToString());
                                }
                                return modifiedShipMatrix;
                            }
                            else if (freeSpace.Contains(false))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_SPACE;
                                return null;
                            }
                            else if (!EnoughShipToPlace(actualShipType, ref counts))
                            {
                                _toShowMessage.Content = Error.ERROR_NO_MORE_SHIP;
                                return null;
                            }
                            else return null;
                        }
                        else return null; 
                    }
                default: { break; }
            }
            return modifiedShipMatrix;
        }
        public void setDirectionWithArrows(KeyEventArgs e, ref Dir actualDirection, Image orientation)
        {
            switch(e.Key)
            {
               case Key.Up:
                    {
                        actualDirection = Dir.up;
                        //Set direction image
                        RotateImage(orientation, actualDirection);
                        break;
                    }
               case Key.Down:
                    {
                        actualDirection = Dir.down;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                case Key.Left:
                    {
                        actualDirection = Dir.left;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                case Key.Right:
                    {
                        actualDirection = Dir.right;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                default:
                    {
                        actualDirection = actualDirection;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
            }
        }
        public void setActualShipTypeWithKey(KeyEventArgs e,ref ShipType actualShipType, params RadioButton[] radios)
        {
            switch(e.Key)
            {
                case Key.D1:
                    {
                        actualShipType = ShipType.Smallship;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
                case Key.D2:
                    {
                        actualShipType = ShipType.Destroyer;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
                case Key.D3:
                    {
                        actualShipType = ShipType.Submarine;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
                case Key.D4:
                    {
                        actualShipType = ShipType.Carrier;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
                case Key.D5:
                    {
                        actualShipType = ShipType.Battleship;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
                default:
                    {
                        actualShipType = actualShipType;
                        getActualShipType(actualShipType, radios);
                        break;
                    }
            }
        }
        public void setDirectionWithMouse(ref Dir actualDirection, Image orientation)
        {
            switch(actualDirection)
            {
                case Dir.up:
                    {
                        actualDirection = Dir.right;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                case Dir.down:
                    {
                        actualDirection = Dir.left;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                case Dir.left:
                    {
                        actualDirection = Dir.up;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
                case Dir.right:
                    {
                        actualDirection = Dir.down;
                        RotateImage(orientation, actualDirection);
                        break;
                    }
            }
        }
        public void setActualShipTypeWithRadio(ref ShipType actualShipType,params RadioButton[] radioButtons)
        {
            ShipType[] directions = new ShipType[] { ShipType.Smallship, ShipType.Destroyer, ShipType.Submarine, ShipType.Carrier, ShipType.Battleship };
            if (radioButtons.Length == 5)
            {
                if((bool)radioButtons[0].IsChecked)
                {
                    actualShipType = directions[0];
                }
                else if ((bool)radioButtons[1].IsChecked)
                {
                    actualShipType = directions[1];
                }
                else if ((bool)radioButtons[2].IsChecked)
                {
                    actualShipType = directions[2];
                }
                else if ((bool)radioButtons[3].IsChecked)
                {
                    actualShipType = directions[3];
                }else if((bool)radioButtons[4].IsChecked)
                {
                    actualShipType = directions[4];
                }
            }
            else throw new ArgumentOutOfRangeException(radioButtons.Length.ToString());
        }
        private void getActualShipType(ShipType shiptype, params RadioButton[] radioButtons)
        {
            if(shiptype == ShipType.Smallship)
            {
                radioButtons[0].IsChecked = true;
            }
            else if(shiptype == ShipType.Destroyer)
            {
                radioButtons[1].IsChecked = true;
            }
            else if (shiptype == ShipType.Submarine)
            {
                radioButtons[2].IsChecked = true;
            }
            else if (shiptype == ShipType.Carrier)
            {
                radioButtons[3].IsChecked = true;
            }
            else if (shiptype == ShipType.Battleship)
            {
                radioButtons[4].IsChecked = true;
            }//kivétel kezelés
        }
        public void ClearCanvas(ref int[,] shipMatrix, ref int[] shipCounts,int width, int height,Canvas canvas)
        {
            int[] newShipCounts = new int[] { 1, 1, 1, 1, 1 };
            int[,] newShipMatrix = new int[width, height];
            shipMatrix = newShipMatrix;
            shipCounts = newShipCounts;
            canvas.Children.Clear();
        }
        public void ShowInstructions()
        {
            string instructions = "A hajókat orientációnak megfelelően a kiválasztott ponttól rakja le a játék. A lehelyezés orientációját jobb egér klikkel vagy a nyilak segítségével tudja kiválasztani. A hajókat a radiogombok-kal vagy 1-5 számokkal tudja kiválasztani.";
            MessageBox.Show(instructions);
        }
        private bool EnoughShipToPlace(ShipType actualShipType,ref int[] countOfShips)
        {
            if (actualShipType == ShipType.Smallship && countOfShips[0] > 0)
            {
                countOfShips[0]--;
                return true;
            }
            else if (actualShipType == ShipType.Destroyer && countOfShips[1] > 0)
            {
                countOfShips[1]--;
                return true;
            }
            else if (actualShipType == ShipType.Submarine && countOfShips[2] > 0)
            {
                countOfShips[2]--;
                return true;
            }
            else if (actualShipType == ShipType.Carrier && countOfShips[3] > 0)
            {
                countOfShips[3]--;
                return true;
            }
            else if (actualShipType == ShipType.Battleship && countOfShips[4] > 0)
            {
                countOfShips[4]--;
                return true;
            }
            else return false;
        }
        private void RotateImage(Image img, Dir dir)
        {
            if(dir == Dir.up) img.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
            else if (dir == Dir.down) img.Source = new BitmapImage(new Uri("ori_down.png", UriKind.Relative));
            else if (dir == Dir.left) img.Source = new BitmapImage(new Uri("ori_left.png", UriKind.Relative));
            else if (dir == Dir.right) img.Source = new BitmapImage(new Uri("ori_right.png", UriKind.Relative));
        }
        public void SetRadioButtonText(params RadioButton[] shipRadioButtons)
        {
            if (shipRadioButtons.Length == 5 && shipNames.Length == 5)
            {
                shipRadioButtons[0].Content = $"{shipNames[0]} ({shipCounts[0]})";
                shipRadioButtons[1].Content = $"{shipNames[1]} ({shipCounts[1]})";
                shipRadioButtons[2].Content = $"{shipNames[2]} ({shipCounts[2]})";
                shipRadioButtons[3].Content = $"{shipNames[3]} ({shipCounts[3]})";
                shipRadioButtons[4].Content = $"{shipNames[4]} ({shipCounts[4]})";
            }
            else throw new ArgumentOutOfRangeException($"Out of range, ${shipNames.Length} or ${shipRadioButtons.Length}");
        }
        public void saveArray(GameType gameType, Window actualWindow, Canvas canvas)
        {
            if (gameType == GameType.Pvp && !shipCounts.Contains(1))
            {
                if (!secondPlayerPicked)
                {
                    firstArrayFinal = shipMatrix;
                    Application.Current.Properties[prop_playerArray] = shipMatrix;
                    ClearCanvas(ref shipMatrix, ref shipCounts, _width, _height, canvas);
                    secondPlayerPicked = true;
                }
                else
                {
                    secondArrayFinal = shipMatrix;
                    Application.Current.Properties[prop_2ndPlayerArray] = shipMatrix;
                    Battle battleWindow = new Battle(_width, _height, firstArrayFinal, secondArrayFinal);
                    battleWindow.Show();
                    actualWindow.Close();
                }
            }
            else if (gameType == GameType.PvAi && !shipCounts.Contains(1))
            {
                firstArrayFinal = shipMatrix;
                Application.Current.Properties[prop_playerArray] = shipMatrix;
                //Pass the generated ai shipLayout
                Battle battleWindow = new Battle(_width, _height, firstArrayFinal, firstArrayFinal);
                battleWindow.Show();
                actualWindow.Close();
            }
            else _toShowMessage.Content = "There are more ship to place";
            //guoard sentence
            
            
            
        }
    }
}
