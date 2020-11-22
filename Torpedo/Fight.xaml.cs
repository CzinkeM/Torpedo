using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private GameType _gameType;
        public Fight(int width, int height, GameType gameType)
        {
            _height = height;
            _width = width;
            _gameType = gameType;
            _shipLayoutFirstPlayer = (int[,]) Application.Current.Properties["first"];
            _shipLayoutSecondPlayer = (int[,]) Application.Current.Properties["second"];
            InitializeComponent();
        }

        private void SecondPlayerPickTile(object sender, MouseButtonEventArgs e)
        {
            Vector pointOnCanvas = _shipMangement.GetPointOnCanvas(canvas1stPlayer, _width, _height);
            PickTile(canvas1stPlayer, canvas2ndPlayer, _shipLayoutFirstPlayer, pointOnCanvas);
            _rounds++;
        }

        private void FirstPlayerPickTile(object sender, MouseButtonEventArgs e)
        {
            Vector pointOnCnvas = _shipMangement.GetPointOnCanvas(canvas2ndPlayer, _width, _height);
            PickTile(canvas2ndPlayer,canvas1stPlayer,_shipLayoutSecondPlayer,pointOnCnvas);
            _rounds++;
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
                    PassTurn(canvasFromPick,canvasToPass);
                    //Berajzolni a pontot
                    //Megnézni milyen számú
                    //Növelni a találatok számát
                }
                else
                {
                    color = Brushes.AliceBlue;
                    _draw.DrawPoint(canvasFromPick, _width, _height, clickedPoint, color, "notHit");
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
