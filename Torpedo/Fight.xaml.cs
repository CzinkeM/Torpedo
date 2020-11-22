using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Torpedo
{
    public partial class Fight : Window
    {
        private int[,] _shipLayoutFirstPlayer;
        private int[,] _shipLayoutSecondPlayer;

        private int _width;
        private int _height;

        private int _points;
        private int _rounds;

        private ShipMangement _shipMangement = new ShipMangement();
        private Draw _draw = new Draw();
        public Fight(int width, int height, List<Ship> placedShips)
        {
            _height = height;
            _width = width;
            _shipLayoutFirstPlayer = (int[,]) Application.Current.Properties["first"];
            _shipLayoutSecondPlayer = (int[,]) Application.Current.Properties["second"];
            InitializeComponent();
        }

        private void SecondPlayerPickTile(object sender, MouseButtonEventArgs e)
        {
            Vector pointOnCanvas = _shipMangement.GetPointOnCanvas(canvas1stPlayer,_width,_height);
            PickTile(canvas1stPlayer,canvas2ndPlayer, _shipLayoutFirstPlayer, pointOnCanvas);
        }

        private void FirstPlayerPickTile(object sender, MouseButtonEventArgs e)
        {
            Vector pointOnCnvas = _shipMangement.GetPointOnCanvas(canvas2ndPlayer, _width, _height);
            PickTile(canvas2ndPlayer,canvas1stPlayer,_shipLayoutSecondPlayer,pointOnCnvas);
        }
        private void PickTile(Canvas canvasFromPick,Canvas canvasToPass, int[,] shipLayout,Vector clickedPoint)
        {
            if(canvasFromPick.IsHitTestVisible)
            {
                int x = Convert.ToInt32(clickedPoint.X);
                int y = Convert.ToInt32(clickedPoint.Y);
                Brush color;
                if (shipLayout[x, y] != 0)
                {
                    color = Brushes.Red;
                    _draw.DrawPoint(canvasFromPick, _width, _height, clickedPoint, color, "hit");
                    _points++;
                    _rounds++;
                    PassTurn(canvasFromPick,canvasToPass);
                    //Berajzolni a pontot
                    //Megnézni milyen számú
                    //Növelni a találatok számát
                }
                else
                {
                    color = Brushes.AliceBlue;
                    _draw.DrawPoint(canvasFromPick, _width, _height, clickedPoint, color, "notHit");
                    _rounds++;
                    PassTurn(canvasFromPick,canvasToPass);
                    //Berajzolni a nemtalált pontot
                }
            }
            else
            {
                canvasFromPick.IsHitTestVisible = true;
            }
            
        }

        private void PassTurn(Canvas canvas,Canvas toPassTurn)
        {
            canvas.IsHitTestVisible = false;
            toPassTurn.IsHitTestVisible = true;
            //throw new NotImplementedException();
        }
    }
}
