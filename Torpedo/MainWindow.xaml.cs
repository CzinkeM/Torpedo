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
        const int GameWidth = 5;
        const int GameHeight = 5;
        int[,] tableLayout = new int[GameWidth, GameHeight];
        private int shipCount = 3;
        

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
            shape.StrokeThickness = 1.5;
            shape.Uid = Id;
            Canvas.SetLeft(shape, position.X * unitX);
            Canvas.SetTop(shape, position.Y * unitY);
            gameCanvas.Children.Add(shape);
        }

        private void GetPoint(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(gameCanvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            for (int i = 1; i < 5; i++)
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
            var generatedId = lowerLimitX + "" + lowerLimitY;
            textTest2.Text = "Cordinates: " + lowerLimitX + ":" + lowerLimitY + ", value:" + tableLayout[lowerLimitX, lowerLimitY].ToString();
            if (tableLayout[lowerLimitX, lowerLimitY] != 1 && shipCount > 0)
            {
                tableLayout[lowerLimitX, lowerLimitY] = 1;
                DrawPoint(tileVector, Brushes.Red,generatedId);
                shipCount -= 1;
            }
            else if(tableLayout[lowerLimitX, lowerLimitY] != 0 && shipCount >= 0)
            {
                tableLayout[lowerLimitX, lowerLimitY] = 0;
                gameCanvas.Children.Remove(FindUid(gameCanvas, generatedId));
                shipCount +=1;
            }
            
            textTest1.Text = shipCount.ToString();
            textTest2.Text = generatedId;
            
            string cordinatesAsString = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cordinatesAsString += ","+tableLayout[i, j].ToString();
                }
            }
            
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
    }
}
