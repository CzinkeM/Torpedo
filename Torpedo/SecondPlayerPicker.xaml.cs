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
    public partial class SecondPlayerPicker : Window
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
        List<Ship> deployedShips = new List<Ship>();

        public SecondPlayerPicker(GameSize gameSize,GameType gameType,int gameWidth,int gameHeight)
        {
            _gameWidth = gameWidth;
            _gameHeight = gameHeight;
            _shipLayout = new int[_gameWidth, _gameHeight];
            actualGameSize = gameSize;
            actualGameType = gameType;
            InitializeComponent();
            count = shipMangement.InitilaizeShipCount(actualGameSize);
            shipMangement.DeleteAllShip(gameCanvas_2, ref _shipLayout, ref count, _gameWidth, _gameHeight, actualGameSize);
            InitializeRadioButtonContent();
            radioTypeSmall_2.IsChecked = true;
            setActualShipType();
        }
        private void setActualShipType()
        {
            if ((bool)radioTypeSmall_2.IsChecked) actualShipType = ShipName.Small;
            else if ((bool)radioTypeDestroyer_2.IsChecked) actualShipType = ShipName.Destoyer;
            else if ((bool)radioTypeSubmarine_2.IsChecked) actualShipType = ShipName.Submarine;
            else if ((bool)radioTypeCarrier_2.IsChecked) actualShipType = ShipName.Carrier;
            else if ((bool)radioTypeBattleShip_2.IsChecked) actualShipType = ShipName.Battleship;
            else throw new ArgumentException(actualShipType + " is not specified");
        }
        private void InitializeRadioButtonContent()
        {
            radioTypeSmall_2.Content = $"{ShipName.Small} ({count[0]})";
            radioTypeDestroyer_2.Content = $"{ShipName.Destoyer} ({count[1]})";
            radioTypeSubmarine_2.Content = $"{ShipName.Submarine} ({count[2]})";
            radioTypeCarrier_2.Content = $"{ShipName.Carrier} ({count[3]})";
            radioTypeBattleShip_2.Content = $"{ShipName.Battleship} ({count[4]})";

        }
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            setActualShipType();
            shipMangement.DrawShipOnCanvas(gameCanvas_2, ref _shipLayout, ref count, ref deployedShips, _gameWidth, _gameHeight, actualShipType, actualDir, labelError_2);
            InitializeRadioButtonContent();
        }


        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {
            var allZero = true;
            foreach (var value in count)
            {
                if (value != 0)
                {
                    allZero = false;
                    break;
                }
            }
            if (allZero)
            {
                Application.Current.Properties["second"] = _shipLayout;
                Fight fight = new Fight(_gameWidth,_gameWidth,deployedShips);
                fight.Show();
                this.Close();
            }
            else new Error(labelError_2).ShowErrorMessage(Error.ERROR_SHIP_AVAILABLE);
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                actualDir = Direction.Up;
                imgOrientation_2.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
            }
            if (e.Key == Key.Down)
            {
                actualDir = Direction.Down;
                imgOrientation_2.Source = new BitmapImage(new Uri("ori_down.png", UriKind.Relative));
            }
            if (e.Key == Key.Left)
            {
                actualDir = Direction.Left;
                imgOrientation_2.Source = new BitmapImage(new Uri("ori_left.png", UriKind.Relative));
            }
            if (e.Key == Key.Right)
            {
                actualDir = Direction.Right;
                imgOrientation_2.Source = new BitmapImage(new Uri("ori_right.png", UriKind.Relative));
            }
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            shipMangement.DeleteAllShip(gameCanvas_2, ref _shipLayout, ref count, _gameWidth, _gameHeight, actualGameSize);
            InitializeRadioButtonContent();
        }
        private void ClickInfoButton(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("Ez egy infós ablak");
        }
    }
}
