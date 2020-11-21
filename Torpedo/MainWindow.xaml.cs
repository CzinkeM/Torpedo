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
        Vector gameDimension = new Vector(GameWidth, GameHeight);
        int[] count = new int[] { 0, 0, 0, 0, 0 };
        int[,] shipLayout = new int[GameWidth, GameHeight];
        ShipMangement shipMangement = new ShipMangement();
        Direction actualDir = Direction.Up;
        ShipName actualShipType;
        GameSize actualGameSize = GameSize.Small;
        List<Ship> deployedShips = new List<Ship>();

        public MainWindow()
        {
            InitializeComponent();
            radioTypeSmall.IsChecked = true;
            count = shipMangement.InitilaizeShipCount(actualGameSize);//pass the getted gamesize
            InitializeRadioButtonContent();
            setActualShipType();
        }
        private void setActualShipType()
        {
            if ((bool)radioTypeSmall.IsChecked) actualShipType = ShipName.Small;
            else if ((bool)radioTypeDestroyer.IsChecked) actualShipType = ShipName.Destoyer;
            else if ((bool)radioTypeSubmarine.IsChecked) actualShipType = ShipName.Submarine;
            else if ((bool)radioTypeCarrier.IsChecked) actualShipType = ShipName.Carrier;
            else if ((bool)radioTypeBattleShip.IsChecked) actualShipType = ShipName.Battleship;
            else throw new ArgumentException(actualShipType + " is not specified");
        }
        private void InitializeRadioButtonContent()
        {
            radioTypeSmall.Content = $"{ShipName.Small} ({count[0]})";
            radioTypeDestroyer.Content = $"{ShipName.Destoyer} ({count[1]})";
            radioTypeSubmarine.Content = $"{ShipName.Submarine} ({count[2]})";
            radioTypeCarrier.Content = $"{ShipName.Carrier} ({count[3]})";
            radioTypeBattleShip.Content = $"{ShipName.Battleship} ({count[4]})";
            
        }
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            setActualShipType();
            shipMangement.DrawShipOnCanvas(gameCanvas, ref shipLayout, ref count, ref deployedShips, GameWidth, GameHeight, actualShipType, actualDir, ErrorLabel);
            InitializeRadioButtonContent();
        }
        

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {

            //TODO
            //Pass the tableLayout array to the next Window
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                actualDir = Direction.Up;
                imgOrientation.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
            }
            if(e.Key == Key.Down)
            {
                actualDir = Direction.Down;
                imgOrientation.Source = new BitmapImage(new Uri("ori_down.png", UriKind.Relative));
            }
            if (e.Key == Key.Left)
            {
                actualDir = Direction.Left;
                imgOrientation.Source = new BitmapImage(new Uri("ori_left.png", UriKind.Relative));
            }
            if (e.Key == Key.Right)
            {
                actualDir = Direction.Right;
                imgOrientation.Source = new BitmapImage(new Uri("ori_right.png", UriKind.Relative));
            }
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            shipMangement.DeleteAllShip(gameCanvas,ref shipLayout,ref count,GameWidth,GameHeight,actualGameSize);
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
