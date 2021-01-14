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
using Torpedo.Model;
using Torpedo.ViewModel;

namespace Torpedo.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Battle : Window
    {
        private BattleViewModel _viewModel;
        private int _width;
        private int _height;
        private int[] count = new int[] { 1, 1, 1, 1, 1 };
        private Player _actualPlayer;

        public Battle(int width, int height,int[,] firstArray, int[,] secondArray)
        {
            InitializeComponent();
            _width = width;
            _height = height;
            _viewModel = new BattleViewModel( _width, _height, firstArray,secondArray);
            _actualPlayer = Player.FirstPlayer;

            textRound.Content = _viewModel.rounds;

            textPointFirstPlayer.Content = $"Pont: {_viewModel.pointFirstPlayer}";
            textFirstPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.firstPlayerShipsCounts);
            textPointSecondPlayer.Content = $"Pont: {_viewModel.pointSecondPlayer}";
            textSecondPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.secondPlayerShipsCounts);

        }

        private void clickOnFirstPlayerCanvas(object sender, MouseButtonEventArgs e)
        {
            if(_actualPlayer == Player.SecondPlayer)
            {
                int[,] shipLayout = _viewModel._firstPlayerRefShipMatrix;
                int[] shipCounts = _viewModel.firstPlayerShipsCounts;
                int points = _viewModel.pointFirstPlayer;
                _viewModel.guessTile(canvasFirstPlayer, ref shipLayout, ref shipCounts, ref _actualPlayer);
                textRound.Content = _viewModel.rounds;
                textPointFirstPlayer.Content = $"Pont: {_viewModel.pointFirstPlayer}";
                textFirstPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.firstPlayerShipsCounts);
                _viewModel.endGame(_viewModel.firstPlayerShipsCounts,_actualPlayer,this);
            }
        }

        private void clickOnSecondPlayerCanvas(object sender, MouseButtonEventArgs e)
        {
            if(_actualPlayer == Player.FirstPlayer)
            {
                int[,] shipLayout = _viewModel._secondPlayerRefShipmatrix;
                int[] shipCounts = _viewModel.secondPlayerShipsCounts;
                int points = _viewModel.pointSecondPlayer;
                _viewModel.guessTile(canvasSecondPlayer, ref shipLayout, ref shipCounts, ref _actualPlayer);
                textRound.Content = _viewModel.rounds;
                textPointSecondPlayer.Content = $"Pont: {_viewModel.pointSecondPlayer}";
                textSecondPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.secondPlayerShipsCounts);
                _viewModel.endGame(_viewModel.secondPlayerShipsCounts, _actualPlayer, this);
            }
            
        }
    }
}
