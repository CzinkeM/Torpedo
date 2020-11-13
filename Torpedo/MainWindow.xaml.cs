using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Torpedo
{
    public partial class MainWindow : Window
    {
        const int GameWidth = 10;
        const int GameHeight = 10;

        int[,] tableLayout = new int[GameWidth, GameHeight];

        const string DIR_UP = "up";
        const string DIR_DOWN = "down";
        const string DIR_LEFT = "left";
        const string DIR_RIGHT = "right";

        const string SHIP_SMALL = "smell_ship";
        const string SHIP_DESTOYER = "destroyer";
        const string SHIP_SUBMARINE = "submarine";
        const string SHIP_CARRIER = "aircraft_carrier";
        const string SHIP_BATTLESHIP = "battleship";

        //Initiallize ship count depened on the game type
        int SHIP_SMALL_count = 0;
        int SHIP_DESTROYER_count = 0;
        int SHIP_SUBMARINE_count = 0;
        int SHIP_CARRIER_count = 0;
        int SHIP_BATTLESHIP_count = 0;

        const int SHIP_LENGTH_SMALL = 1;     
        const int SHIP_LENGTH_DESTORYER = 2;     
        const int SHIP_LENGTH_SUBMARINE = 3;     
        const int SHIP_LENGTH_CARRIER = 3;     
        const int SHIP_LENGTH_BATTLESHIP = 4;     
        

        string actualDirection = DIR_UP;
        string actualShipType = SHIP_SMALL;

        string gameSize = "medium";

        public MainWindow()
        {
            InitializeComponent();
            InitializeShipsCount();
            InitializeRadioButtonContent();
        }
        private void InitializeRadioButtonContent()
        {
            radioTypeSmall.Content = $"{SHIP_SMALL} ({SHIP_SMALL_count})";
            radioTypeDestroyer.Content = $"{SHIP_DESTOYER} ({SHIP_DESTROYER_count})";
            radioTypeSubmarine.Content = $"{SHIP_SUBMARINE} ({SHIP_SUBMARINE_count})";
            radioTypeCarrier.Content = $"{SHIP_CARRIER} ({SHIP_CARRIER_count})";
            radioTypeBattleShip.Content = $"{SHIP_BATTLESHIP} ({SHIP_BATTLESHIP_count})";
            radioTypeSmall.IsChecked = true;
        }

        private void InitializeShipsCount()
        {
            if(!String.IsNullOrWhiteSpace(gameSize))
            {
                if (gameSize == "small")
                {
                    SHIP_SMALL_count = 1;
                    SHIP_DESTROYER_count = 1;
                    SHIP_CARRIER_count = 1;
                    SHIP_SUBMARINE_count = 1;
                    SHIP_BATTLESHIP_count = 1;
                }
                else if (gameSize == "medium")
                {
                    SHIP_SMALL_count = 2;
                    SHIP_DESTROYER_count = 2;
                    SHIP_CARRIER_count = 2;
                    SHIP_SUBMARINE_count = 2;
                    SHIP_BATTLESHIP_count = 2;
                }
                else if (gameSize == "Large")
                {
                    SHIP_SMALL_count = 3;
                    SHIP_DESTROYER_count = 3;
                    SHIP_CARRIER_count = 3;
                    SHIP_SUBMARINE_count = 3;
                    SHIP_BATTLESHIP_count = 3;
                }
                else throw new ArgumentException("Game Size not passed");
            }
        }

        private void DrawPoint(Vector position, Brush brush,string Id)
        {
            var shape = new Rectangle();
            shape.Fill = brush;
            var unitX = gameCanvas.Width / GameWidth;
            var unitY = gameCanvas.Height / GameHeight;
            shape.Width = unitX;
            shape.Height = unitY;
            shape.Stroke = Brushes.Black;
            shape.StrokeThickness = 1;
            shape.Uid = Id;
            Canvas.SetLeft(shape, position.X * unitX);
            Canvas.SetTop(shape, position.Y * unitY);
            gameCanvas.Children.Add(shape);
        }

        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            ChooseActiveShipType();
            //SMALL
            if (actualShipType == SHIP_SMALL && SHIP_SMALL_count > 0)
            {
                int[,] previousTableState = tableLayout;
                tableLayout = PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_SMALL, actualDirection);

                if (tableLayout == null)
                {

                    ShowErrorMessage("Cannot place ship here");
                    tableLayout = previousTableState;
                }
                else{
                    DrawTheShips(tableLayout, SHIP_SMALL, SHIP_SMALL_count);
                    SHIP_SMALL_count--;
                    radioTypeSmall.Content = $"{SHIP_SMALL} ({SHIP_SMALL_count})";
                    ShowErrorMessage("");
                }
                
                //DESTROYER
            }else if (actualShipType == SHIP_DESTOYER && SHIP_DESTROYER_count > 0)
            {
                int[,] previousTableState = tableLayout;
                tableLayout = PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_DESTORYER, actualDirection);

                if (tableLayout == null)
                {

                    ShowErrorMessage("Cannot place ship here");
                    tableLayout = previousTableState;
                }
                else
                {
                    DrawTheShips(tableLayout, SHIP_DESTOYER, SHIP_DESTROYER_count);
                    SHIP_DESTROYER_count--;
                    radioTypeDestroyer.Content = $"{SHIP_DESTOYER} ({SHIP_DESTROYER_count})";
                    ShowErrorMessage("");
                }

                //SUBMARINE
            }else if (actualShipType == SHIP_SUBMARINE && SHIP_SUBMARINE_count > 0)
            {
                int[,] previousTableState = tableLayout;
                tableLayout = PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_SUBMARINE, actualDirection);

                if (tableLayout == null)
                {

                    ShowErrorMessage("Cannot place ship here");
                    tableLayout = previousTableState;
                }
                else
                {
                    DrawTheShips(tableLayout, SHIP_SUBMARINE, SHIP_SUBMARINE_count);
                    SHIP_SUBMARINE_count--;
                    radioTypeSubmarine.Content = $"{SHIP_SUBMARINE} ({SHIP_SUBMARINE_count})";
                    ShowErrorMessage("");
                }

                //CARRIER
            }else if (actualShipType == SHIP_CARRIER && SHIP_CARRIER_count > 0)
            {
                int[,] previousTableState = tableLayout;
                tableLayout = PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_CARRIER, actualDirection);

                if (tableLayout == null)
                {

                    ShowErrorMessage("Cannot place ship here");
                    tableLayout = previousTableState;
                }
                else
                {
                    DrawTheShips(tableLayout, SHIP_CARRIER, SHIP_CARRIER_count);
                    SHIP_CARRIER_count--;
                    radioTypeCarrier.Content = $"{SHIP_CARRIER} ({SHIP_CARRIER_count})";
                    ShowErrorMessage("");
                }

                //BATTLESHP
            }else if (actualShipType == SHIP_BATTLESHIP && SHIP_BATTLESHIP_count > 0)
            {
                int[,] previousTableState = tableLayout;
                tableLayout = PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_BATTLESHIP, actualDirection);

                if (tableLayout == null)
                {

                    ShowErrorMessage("Cannot place ship here");
                    tableLayout = previousTableState;
                }
                else
                {
                    DrawTheShips(tableLayout, SHIP_BATTLESHIP, SHIP_BATTLESHIP_count);
                    SHIP_BATTLESHIP_count--;
                    radioTypeBattleShip.Content = $"{SHIP_BATTLESHIP} ({SHIP_BATTLESHIP_count})";
                    ShowErrorMessage("");
                }


            }
            else ShowErrorMessage("There is no more ship");
            
            
        }
        private Vector GetPoint()
        {
            var mousePosition = Mouse.GetPosition(gameCanvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < GameWidth; i++)
            {
                if (i * (gameCanvas.Width / GameWidth) < mousePositionX)
                {
                    lowerLimitX = i;
                }
                if (i * (gameCanvas.Height / GameHeight) < mousePositionY)
                {
                    lowerLimitY = i;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            return tileVector;
        }
        private string TwoDimensionalArrayToString(int[,] ArrayToConvert)
        {
            string ResultString = "";
            for (int i = 0; i < ArrayToConvert.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayToConvert.GetLength(1); j++)
                {
                    ResultString = ResultString + "," + ArrayToConvert[j, i].ToString();
                }
                ResultString = ResultString + "\n";
            }
            return ResultString;
        }
        private void DrawTheShips(int[,] playgorund,string shipType,int shipCount)
        {
            string id = shipType + "_" + shipCount;
            for (int i = 0; i < playgorund.GetLength(0); i++)
            {
                for (int j= 0; j < playgorund.GetLength(1); j++)
                {
                    if(playgorund[i,j] != 0)
                    {
                        DrawPoint(new Vector(i, j), Brushes.Red,id);
                    }
                }
            }
        }
        

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {
            //TODO
            //Pass the tableLayout array to the next Window
        }

        private int[,] PlaceShip(int[,] GameSpace,Vector startPosition,int lengthOfTheShip,string direction)
        {
            int[,] modifiedArray = new int[GameWidth, GameHeight];            
            int X = Convert.ToInt16(startPosition.X);
            int Y = Convert.ToInt16(startPosition.Y);
            if (direction == DIR_UP)
            {
                if(Y-(lengthOfTheShip-1)>= 0)
                {
                    var freeSpaces = new List<bool>();
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        if(GameSpace[X,Y-i] == 0)
                        {
                            freeSpaces.Add(true);
                        }
                        else
                        {
                            freeSpaces.Add(false);
                        }
                    }
                    if(freeSpaces.Contains(false))
                    {
                        modifiedArray = null;
                    }
                    else
                    {
                        for (int j = 0; j < lengthOfTheShip; j++)
                        {
                            GameSpace[X, Y-j] = lengthOfTheShip;                        
                        }
                        modifiedArray = GameSpace;
                    }                    
                }
                else modifiedArray = null;
            }else if (direction == DIR_DOWN)
            {
                if (Y + (lengthOfTheShip - 1) <= GameSpace.GetLength(1)-1)
                {
                    var freeSpaces = new List<bool>();
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        if (GameSpace[X, Y + i] == 0)
                        {
                            freeSpaces.Add(true);
                        }
                        else
                        {
                            freeSpaces.Add(false);
                        }
                    }
                    if (freeSpaces.Contains(false))
                    {
                        modifiedArray = null;
                    }
                    else
                    {
                        for (int j = 0; j < lengthOfTheShip; j++)
                        {
                            GameSpace[X, Y + j] = lengthOfTheShip;
                        }
                        modifiedArray = GameSpace;
                    }
                }
                else modifiedArray = null;
            }else if (direction == DIR_LEFT)
            {
                if (X - (lengthOfTheShip - 1) >= 0)
                {
                    var freeSpaces = new List<bool>();
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        if (GameSpace[X-i, Y] == 0)
                        {
                            freeSpaces.Add(true);
                        }
                        else
                        {
                            freeSpaces.Add(false);
                        }
                    }
                    if (freeSpaces.Contains(false))
                    {
                        modifiedArray = null;
                    }
                    else
                    {
                        for (int j = 0; j < lengthOfTheShip; j++)
                        {
                            GameSpace[X -j,Y] = lengthOfTheShip;
                        }
                        modifiedArray = GameSpace;
                    }
                }
                else modifiedArray = null;
            }
            else if (direction == DIR_RIGHT)
            {
                if (X + (lengthOfTheShip - 1) <= GameSpace.GetLength(0) -1)
                {
                    var freeSpaces = new List<bool>();
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        if (GameSpace[X+i, Y ] == 0)
                        {
                            freeSpaces.Add(true);
                        }
                        else
                        {
                            freeSpaces.Add(false);
                        }
                    }
                    if (freeSpaces.Contains(false))
                    {
                        modifiedArray = null;
                    }
                    else
                    {
                        for (int j = 0; j < lengthOfTheShip; j++)
                        {
                            GameSpace[X+j, Y] = lengthOfTheShip;
                        }
                        modifiedArray = GameSpace;
                    }
                }
                else modifiedArray = null;
            }



            return modifiedArray;
        }

        private void ShowErrorMessage(string ErrorMessage)
        {
            ErrorLabel.Content = ErrorMessage;
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                actualDirection = DIR_UP;
                imgOrientation.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
            }
            if(e.Key == Key.Down)
            {
                actualDirection = DIR_DOWN;
                imgOrientation.Source = new BitmapImage(new Uri("ori_down.png", UriKind.Relative));
            }
            if (e.Key == Key.Left)
            {
                actualDirection = DIR_LEFT;
                imgOrientation.Source = new BitmapImage(new Uri("ori_left.png", UriKind.Relative));
            }
            if (e.Key == Key.Right)
            {
                actualDirection = DIR_RIGHT;
                imgOrientation.Source = new BitmapImage(new Uri("ori_right.png", UriKind.Relative));
            }
        }

        private void ChooseActiveShipType()
        {
            if ((bool)radioTypeSmall.IsChecked)
            {
                actualShipType = SHIP_SMALL;
            }
            else if ((bool)radioTypeDestroyer.IsChecked)
            {
                actualShipType = SHIP_DESTOYER;
            }
            else if ((bool)radioTypeSubmarine.IsChecked)
            {
                actualShipType = SHIP_SUBMARINE;
            }
            else if ((bool)radioTypeCarrier.IsChecked)
            {
                actualShipType = SHIP_CARRIER;
            } if ((bool)radioTypeBattleShip.IsChecked)
            {
                actualShipType = SHIP_BATTLESHIP;
            }
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            gameCanvas.Children.Clear();
            tableLayout = new int[GameHeight, GameWidth];
            InitializeShipsCount();
            InitializeRadioButtonContent();
        }
        private void ClickInfoButton(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("Ez egy infós ablak");
        }
        //Todo összes lerakott hajó törlése gomb
        //Todo Hajó számláló kis ikonok és mellé a számuk textbe
        //Helper icon amire felugró segítség ablak mit hogyan kell
    }
}
