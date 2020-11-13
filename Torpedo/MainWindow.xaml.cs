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
        const string DIR_UP = "up";
        const string DIR_DOWN = "down";
        const string DIR_LEFT = "left";
        const string DIR_RIGHT = "right";

        const int SHIP_DESTORYER = 3;

        int[,] tableLayout = new int[GameWidth, GameHeight];

        string actualDirection = DIR_UP;

        public MainWindow()
        {
            InitializeComponent();
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
            PlaceShip(tableLayout, GetPoint(), SHIP_DESTORYER, actualDirection);
            textTest1.Text = TwoDimensionalArrayToString(tableLayout);
            DrawTheShips(tableLayout);
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
        private void DrawTheShips(int[,] playgorund)
        {
            for (int i = 0; i < playgorund.GetLength(0); i++)
            {
                for (int j= 0; j < playgorund.GetLength(1); j++)
                {
                    if(playgorund[i,j] != 0)
                    {
                        DrawPoint(new Vector(i, j), Brushes.Red,"id");
                    }
                }
            }
        }
        

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {

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
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    GameSpace[X, Y - i] = lengthOfTheShip;
                }
            }else if(direction == DIR_DOWN && !((Y+lengthOfTheShip)>GameSpace.GetLength(1)))
            {
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    GameSpace[X, Y + i] = lengthOfTheShip;
                }
            }else if(direction == DIR_LEFT && !((X-lengthOfTheShip+1)<0))
            {
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    GameSpace[X -i, Y ] = lengthOfTheShip;
                }
            }
            else if(direction == DIR_RIGHT && !((X+lengthOfTheShip)>GameSpace.GetLength(0)))
            {
                for (int i = 0; i < lengthOfTheShip; i++)
                {
                    GameSpace[X + i, Y] = lengthOfTheShip;
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

        private void ShowErrorMessage()
        {
            throw new NotImplementedException();
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
        //Todo összes lerakott hajó törlése gomb
        //Todo Hajó számláló kis ikonok és mellé a számuk textbe
        //Helper icon amire felugró segítség ablak mit hogyan kell
    }
}
