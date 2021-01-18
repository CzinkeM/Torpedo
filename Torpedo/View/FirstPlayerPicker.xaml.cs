using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Torpedo.ViewModel;

namespace Torpedo
{
    public partial class FirstPlayerPicker : Window
    {
        private Draw _draw;
        private PickerViewModel _viewModel;
        private int _width;
        private int _height;
        Dir _actualDirection;
        ShipType _actualShipType;
        public FirstPlayerPicker(int width, int height)
        {
            InitializeComponent();
            _viewModel = new PickerViewModel(gameCanvas_1, width, height, ErrorLabel_1,this);
            _viewModel.SetRadioButtonText(radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1);
            _width = width;
            _height = height;
            _draw = new Draw(gameCanvas_1, width, height);
            _actualDirection = Dir.up;
            radioTypeSmall_1.IsChecked = true;
            _actualShipType = ShipType.Smallship;
            orientImage.Source = new BitmapImage(new Uri("ori_up.png", UriKind.Relative));
        }
       
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            _viewModel.setActualShipTypeWithRadio(ref _actualShipType,radioTypeSmall_1,radioTypeDestroyer_1,radioTypeSubmarine_1,radioTypeCarrier_1,radioTypeBattleShip_1);
            Ship ship = new Ship(_actualShipType);
            Vector clickedPoint = _viewModel.GetPointOnCanvas();
            _viewModel.PlaceShip(ship,ref _viewModel.shipCounts, clickedPoint,_actualDirection,_actualShipType);
            _viewModel.SetRadioButtonText(radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1);
        }
        

        private void ConfirmChoosing(object sender, RoutedEventArgs e)
        {
            _viewModel.SetRadioButtonText(radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1);
            GameType gametype =(GameType) Application.Current.Properties["gameType"];
            _viewModel.saveArray(gametype, this, gameCanvas_1);
            _viewModel.SetRadioButtonText(radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1);
        }

        private void gameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            RadioButton[] radioButtonArray = new RadioButton[] { radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1 };
            _viewModel.setActualShipTypeWithKey(e,ref _actualShipType,radioButtonArray);
            _viewModel.setDirectionWithArrows(e,ref _actualDirection,orientImage);
        }

        private void ClickClearButton(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearCanvas(ref _viewModel.shipMatrix,ref _viewModel.shipCounts,_width,_height,gameCanvas_1);
            _viewModel.SetRadioButtonText(radioTypeSmall_1, radioTypeDestroyer_1, radioTypeSubmarine_1, radioTypeCarrier_1, radioTypeBattleShip_1);
        }
        private void gameCanvas_1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.setDirectionWithMouse(ref _actualDirection,orientImage);
        }

        private void ClickInfoButton(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowInstructions();
        }
    }
}
