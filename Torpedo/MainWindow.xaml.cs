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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int GameWidth = 10;
        const int GameHeight = 10;
        const string DIR_UP = "up";
        const string DIR_DOWN = "down";
        const string DIR_LEFT = "left";
        const string DIR_RIGHT = "right";

        const int SHIP_DESTORYER = 3;
        const int SHIP_BOAT = 1;

        int[,] tableLayout = new int[GameWidth, GameHeight];
        private int shipCount = 3;
        private int differentShipCount = 1;
        

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

        private Vector GetPoint(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(gameCanvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < GameWidth; i++)
            {
                if (i*(gameCanvas.Width/GameWidth) < mousePositionX)
                {
                    lowerLimitX = i;
                }
                if(i*(gameCanvas.Height/GameHeight) < mousePositionY)
                {
                    lowerLimitY = i;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            return tileVector;
        }
        private static UIElement FindUid(DependencyObject parent, string uid)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            if (count == 0) return null;
            for (int i = 0; i < count; i++)
            {
                var el = VisualTreeHelper.GetChild(parent, i) as UIElement;
                if (el == null) continue;
                if (el.Uid == uid)
                {
                    return el;
                }
            }
            return null;
        }

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {
            //Pass the matrix
            //OpenOtherWindows
        }

        private int[,] PlaceShip(int[,] GameSpace,Vector startPosition,int lengthOfTheShip,string direction)
        {
            if(lengthOfTheShip <= 0) throw new ArgumentOutOfRangeException(lengthOfTheShip.ToString());
            if (startPosition.X > GameSpace.GetLength(0)) throw new ArgumentOutOfRangeException(startPosition.X.ToString());
            if (startPosition.Y > GameSpace.GetLength(1)) throw new ArgumentOutOfRangeException(startPosition.Y.ToString());
            int X = Convert.ToInt16(startPosition.X);
            int Y = Convert.ToInt16(startPosition.Y);
            int[,] ModifiedMatrix = GameSpace;
            switch(direction)
            {
                case DIR_UP:
                    {
                        for (int i = 0; i < lengthOfTheShip; i++)
                        {
                            if (ModifiedMatrix[X - i, Y] == 0)
                            {
                                ModifiedMatrix[X - i, Y] = lengthOfTheShip;
                            }
                            else
                            {
                                ShowErrorMessage();
                                break;
                            }

                        }
                        return ModifiedMatrix;
                    }
                case DIR_DOWN:
                    {
                        for (int i = 0; i < lengthOfTheShip; i++)
                        {
                            if (ModifiedMatrix[X + i, Y] == 0)
                            {
                                ModifiedMatrix[X + i, Y] = lengthOfTheShip;
                            }
                            else
                            {
                                ShowErrorMessage();
                                break;
                            }

                        }
                        return ModifiedMatrix;
                    }
                case DIR_LEFT:
                    {
                        for (int i = 0; i < lengthOfTheShip; i++)
                        {
                            if (ModifiedMatrix[X, Y-i] == 0)
                            {
                                ModifiedMatrix[X, Y-i] = lengthOfTheShip;
                            }
                            else
                            {
                                ShowErrorMessage();
                                break;
                            }

                        }
                        return ModifiedMatrix;
                    }
                case DIR_RIGHT:
                    {
                        for (int i = 0; i < lengthOfTheShip; i++)
                        {
                            if (ModifiedMatrix[X, Y+i] == 0)
                            {
                                ModifiedMatrix[X, Y+i] = lengthOfTheShip;
                            }
                            else
                            {
                                ShowErrorMessage();
                                break;
                            }

                        }
                        return ModifiedMatrix;
                    }

            }
            return ModifiedMatrix;
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
        //Todo összes lerakott hajó törlése gomb
        //Todo Hajó számláló kis ikonok és mellé a számuk textbe
        //Helper icon amire felugró segítség ablak mit hogyan kell
    }
}
