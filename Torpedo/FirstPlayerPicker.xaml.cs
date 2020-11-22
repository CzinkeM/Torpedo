using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Torpedo
{
    public partial class FirstPlayerPicker : Window
    {
        int _gameWidth;
        int _gameHeight;
        int[] count = new int[] { 0, 0, 0, 0, 0 };
        int[,] _shipLayout;
        ShipMangement shipMangement = new ShipMangement();
        Direction actualDir = Direction.Up;
        ShipName actualShipType;
        GameType actualGameType;
        GameSize actualGameSize;
        Error _error;
        List<Ship> deployedShips = new List<Ship>();

        public FirstPlayerPicker(GameSize gameSize, GameType gameType, int gameWidth, int gameHeight)
        {
            _error = new Error(ErrorLabel_1);
            _gameWidth = gameWidth;
            _gameHeight = gameHeight;
            _shipLayout = new int[_gameWidth, _gameHeight]; 
            actualGameSize = gameSize;
            actualGameType = gameType;
            InitializeComponent();
            count = shipMangement.InitilaizeShipCount(actualGameSize);
            shipMangement.DeleteAllShip(gameCanvas_1, ref _shipLayout, ref count, _gameWidth, _gameHeight, actualGameSize);
            InitializeRadioButtonContent();
            radioTypeSmall_1.IsChecked = true;
            setActualShipType();
        }
        private void setActualShipType()
        {
            if ((bool)radioTypeSmall_1.IsChecked) actualShipType = ShipName.Small;
            else if ((bool)radioTypeDestroyer_1.IsChecked) actualShipType = ShipName.Destoyer;
            else if ((bool)radioTypeSubmarine_1.IsChecked) actualShipType = ShipName.Submarine;
            else if ((bool)radioTypeCarrier_1.IsChecked) actualShipType = ShipName.Carrier;
            else if ((bool)radioTypeBattleShip_1.IsChecked) actualShipType = ShipName.Battleship;
            else throw new ArgumentException(actualShipType + " is not specified");
        }
        private void InitializeRadioButtonContent()
        {
            radioTypeSmall_1.Content = $"{ShipName.Small} ({count[0]})";
            radioTypeDestroyer_1.Content = $"{ShipName.Destoyer} ({count[1]})";
            radioTypeSubmarine_1.Content = $"{ShipName.Submarine} ({count[2]})";
            radioTypeCarrier_1.Content = $"{ShipName.Carrier} ({count[3]})";
            radioTypeBattleShip_1.Content = $"{ShipName.Battleship} ({count[4]})";
            
        }
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            setActualShipType();
            shipMangement.DrawShipOnCanvas(gameCanvas_1, ref _shipLayout, ref count, ref deployedShips, _gameWidth, _gameHeight, actualShipType, actualDir, ErrorLabel_1);
            InitializeRadioButtonContent();
        }
        

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {
            var allZero = true;
            foreach(var value in count)
            {
                if(value!=0)
                {
                    allZero = false;
                    break;
                }
            }

            if (actualGameType == GameType.PlayerVsPlayer && allZero)
            {
                Application.Current.Properties["first"] = _shipLayout;
                SecondPlayerPicker second = new SecondPlayerPicker(actualGameSize, actualGameType, _gameWidth, _gameHeight);
                second.Show();
                this.Close();
            }
            else if (actualGameType == GameType.PlayerVsAi && allZero)
            {
                Application.Current.Properties["player"] = _shipLayout;
            }
            else _error.ShowErrorMessage(Error.ERROR_SHIP_AVAILABLE);
            //TODO
            //Pass the tableLayout array to the next Window
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                actualDir = Direction.Up;
                imgOrientation_1.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
            }
            if(e.Key == Key.Down)
            {
                actualDir = Direction.Down;
                imgOrientation_1.Source = new BitmapImage(new Uri("ori_down.png", UriKind.Relative));
            }
            if (e.Key == Key.Left)
            {
                actualDir = Direction.Left;
                imgOrientation_1.Source = new BitmapImage(new Uri("ori_left.png", UriKind.Relative));
            }
            if (e.Key == Key.Right)
            {
                actualDir = Direction.Right;
                imgOrientation_1.Source = new BitmapImage(new Uri("ori_right.png", UriKind.Relative));
            }
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            shipMangement.DeleteAllShip(gameCanvas_1,ref _shipLayout,ref count,_gameWidth,_gameHeight,actualGameSize);
            InitializeRadioButtonContent();
        }
        private void ClickInfoButton(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("Ez egy infós ablak");
        }
    }
}
