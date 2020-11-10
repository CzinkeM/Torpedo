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
        private int GameWidth = 5;
        private int GameHeight = 5;
        bool[,] tableLayout = new bool[5, 5];
        

        public MainWindow()
        {
            InitializeComponent();
        }
        private void DrawPoint(Vector position, Brush brush)
        {
            var shape = new Rectangle();
            shape.Fill = brush;
            var unitX = gameCanvas.Width / GameWidth;
            var unitY = gameCanvas.Height / GameHeight;
            shape.Width = unitX;
            shape.Height = unitY;
            Canvas.SetLeft(shape, position.X * unitX);
            Canvas.SetTop(shape, position.Y * unitY);
            gameCanvas.Children.Add(shape);
        }

        private void GetPoint(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(gameCanvas);
            var mousePositionX = mousePosition.X;
            var mousePositionY = mousePosition.Y;
            clickedPoint.Text = mousePosition.X.ToString() + ":" + mousePosition.Y.ToString();
            var lowerLimitX = 0;
            var lowerLimitY = 0;
            var upperLimitX = 1;
            var upperLimitY = 1;
            for (int i = 1; i < 5; i++)
            {
                if (i*(gameCanvas.Width/GameWidth) < mousePositionX)
                {
                    lowerLimitX = i;
                    upperLimitX = i + 1;
                }
                if(i*(gameCanvas.Height/GameHeight) < mousePositionY)
                {
                    lowerLimitY = i;
                    upperLimitY = i + 1;
                }
            }
            var tileVector = new Vector(lowerLimitX, lowerLimitY);
            if (tableLayout[lowerLimitX, lowerLimitY] != true)
            {
                tableLayout[lowerLimitX, lowerLimitY] = true;
                DrawPoint(tileVector, Brushes.AliceBlue);
            }
            else
            {
                tableLayout[lowerLimitX, lowerLimitY] = false;
                DrawPoint(tileVector, Brushes.White);
            }
            string cordinatesAsString = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cordinatesAsString += ","+tableLayout[i, j].ToString();
                }
            }
            textCordinates.Text = cordinatesAsString;
            
        }
    }
}
