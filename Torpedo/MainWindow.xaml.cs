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
            if (actualShipType == SHIP_SMALL && SHIP_SMALL_count > 0)
            {
                PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_SMALL, actualDirection);
                textTest1.Text = TwoDimensionalArrayToString(tableLayout);
                DrawTheShips(tableLayout, SHIP_SMALL, SHIP_SMALL_count);
                SHIP_SMALL_count--;
                radioTypeSmall.Content = $"{SHIP_SMALL} ({SHIP_SMALL_count})";
            }
            else if (actualShipType == SHIP_DESTOYER && SHIP_DESTROYER_count > 0)
            {
                PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_DESTORYER, actualDirection);
                textTest1.Text = TwoDimensionalArrayToString(tableLayout);
                DrawTheShips(tableLayout, SHIP_DESTOYER, SHIP_DESTROYER_count);
                SHIP_DESTROYER_count--;
                radioTypeDestroyer.Content = $"{SHIP_DESTOYER} ({SHIP_DESTROYER_count})";
            }
            else if (actualShipType == SHIP_SUBMARINE && SHIP_SUBMARINE_count > 0)
            {
                PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_SUBMARINE, actualDirection);
                textTest1.Text = TwoDimensionalArrayToString(tableLayout);
                DrawTheShips(tableLayout, SHIP_SUBMARINE, SHIP_SUBMARINE_count);
                SHIP_SUBMARINE_count--;
                radioTypeSubmarine.Content = $"{SHIP_SUBMARINE}({SHIP_SUBMARINE_count})";
            }
            else if (actualShipType == SHIP_CARRIER && SHIP_CARRIER_count > 0)
            {
                PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_CARRIER, actualDirection);
                textTest1.Text = TwoDimensionalArrayToString(tableLayout);
                DrawTheShips(tableLayout, SHIP_CARRIER, SHIP_CARRIER_count);
                SHIP_CARRIER_count--;
                radioTypeCarrier.Content = $"{SHIP_CARRIER}({SHIP_CARRIER_count})";
            }
            else if (actualShipType == SHIP_BATTLESHIP && SHIP_BATTLESHIP_count > 0)
            {
                PlaceShip(tableLayout, GetPoint(), SHIP_LENGTH_BATTLESHIP, actualDirection);
                textTest1.Text = TwoDimensionalArrayToString(tableLayout);
                DrawTheShips(tableLayout, SHIP_BATTLESHIP, SHIP_BATTLESHIP_count);
                SHIP_BATTLESHIP_count--;
                radioTypeBattleShip.Content = $"{SHIP_BATTLESHIP} ({SHIP_BATTLESHIP_count})";
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
            //Pass the tableLayout array to the next Window
        }

        private void PlaceShip(int[,] GameSpace,Vector startPosition,int lengthOfTheShip,string direction)
        {
            if(lengthOfTheShip <= 0) throw new ArgumentOutOfRangeException(lengthOfTheShip.ToString());
            if (startPosition.X > GameSpace.GetLength(0)) throw new ArgumentOutOfRangeException(startPosition.X.ToString());
            if (startPosition.Y > GameSpace.GetLength(1)) throw new ArgumentOutOfRangeException(startPosition.Y.ToString());
            int X = Convert.ToInt16(startPosition.X);
            int Y = Convert.ToInt16(startPosition.Y);
            
            if (direction == DIR_UP && !((Y - lengthOfTheShip+1) < 0))
            {
                bool freeSpace = true;
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    if (GameSpace[X, Y - i] == 0)
                    {
                        freeSpace = true;
                    }
                    else
                    {
                        freeSpace = false;
                        break;
                    }
                }
                if(freeSpace)
                {
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        GameSpace[X, Y - i] = lengthOfTheShip;
                    }
                }
            }else if(direction == DIR_DOWN && !((Y+lengthOfTheShip)>GameSpace.GetLength(1)))
            {
                bool freeSpace = true;
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    if (GameSpace[X, Y + i] == 0)
                    {
                        freeSpace = true;
                    }
                    else
                    {
                        freeSpace = false;
                        break;
                    }
                }
                if(freeSpace)
                {
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        GameSpace[X, Y + i] = lengthOfTheShip;
                    }
                }
                
            }else if(direction == DIR_LEFT && !((X-lengthOfTheShip+1)<0))
            {
                bool freeSpace = true;
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    if (GameSpace[X - i, Y] == 0)
                    {
                        freeSpace = true;
                    }
                    else
                    {
                        freeSpace = false;
                        break;
                    }
                }
                if (freeSpace)
                {
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        GameSpace[X-i, Y] = lengthOfTheShip;
                    }
                }
            }
            else if(direction == DIR_RIGHT && !((X+lengthOfTheShip)>GameSpace.GetLength(0)))
            {
                bool freeSpace = true;
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    if (GameSpace[X + i, Y] == 0)
                    {
                        freeSpace = true;
                    }
                    else
                    {
                        freeSpace = false;
                        break;
                    }
                }
                if (freeSpace)
                {
                    for (int i = 0; i < lengthOfTheShip; i++)
                    {
                        GameSpace[X + i, Y] = lengthOfTheShip;
                    }
                }
            }
            //Egy x hosszú hajó lehelyezése a StartPosition pontra
            //Megvizsgálja hogy a játéktérbe belefér-e a hajó(try-catch!?)
            //Megvizsgálja hogy nincs-e valami az útjában(a mátrixban más-e az értéke mint 0)
            //a hajó hosszának számával jelszi a hajó típusát a mátrixban(ha 4 hosszú akkor 4,4,4,4)
            //Forral beilleszti az értékeket a kapott mátrixba és a módosítottat adja vissza
            //Ezután rajzoljuk ki a canvasra, tehát a mátrixot, nem a ui-t manipuláljuk kézzel
            //mátrix módosítás -> ui frissítés
        }

        private void ShowErrorMessage(string ErrorMessage)
        {
            textTest2.Text = ErrorMessage;
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                actualDirection = DIR_UP;
            }
            if(e.Key == Key.Down)
            {
                actualDirection = DIR_DOWN;
            }
            if (e.Key == Key.Left)
            {
                actualDirection = DIR_LEFT;
            }
            if (e.Key == Key.Right)
            {
                actualDirection = DIR_RIGHT;
            }
            textTest2.Text = actualDirection;
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
            else ShowErrorMessage("Choose ship type");
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            gameCanvas.Children.Clear();
            InitializeShipsCount();
            InitializeRadioButtonContent();
        }
        //Todo összes lerakott hajó törlése gomb
        //Todo Hajó számláló kis ikonok és mellé a számuk textbe
        //Helper icon amire felugró segítség ablak mit hogyan kell
    }
}
