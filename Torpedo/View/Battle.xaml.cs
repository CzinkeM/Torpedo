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
        private GameType _actualGameType;
        private AIPlayer aIPlayer = new AIPlayer();

        private List<int[]> aiPrevShots = new List<int[]>();
        private List<int[]> aiPrevHits = new List<int[]>();


        public Battle(int width, int height,int[,] firstArray, int[,] secondArray)
        {
            InitializeComponent();
            _width = width;
            _height = height;
            _viewModel = new BattleViewModel( _width, _height, firstArray,secondArray);
            _actualPlayer = Player.FirstPlayer;
            _actualGameType = (GameType)Application.Current.Properties["gameType"];
            textRound.Content = _viewModel.rounds;

            textPointFirstPlayer.Content = $"Pont: {_viewModel.pointFirstPlayer}";
            textFirstPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.firstPlayerShipsCounts);
            textPointSecondPlayer.Content = $"Pont: {_viewModel.pointSecondPlayer}";
            textSecondPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.secondPlayerShipsCounts);

        }

        private void clickOnFirstPlayerCanvas(object sender, MouseButtonEventArgs e)
        {
            if(_actualGameType == GameType.Pvp)
            {
                if (_actualPlayer == Player.SecondPlayer)
                {
                    int[,] shipLayout = _viewModel._firstPlayerRefShipMatrix;
                    int[] shipCounts = _viewModel.firstPlayerShipsCounts;
                    int points = _viewModel.pointFirstPlayer;
                    _viewModel.guessTile(canvasFirstPlayer, ref shipLayout, ref shipCounts, ref _actualPlayer, _viewModel.GetPointOnCanvas(canvasFirstPlayer));
                    textRound.Content = _viewModel.rounds;
                    textPointFirstPlayer.Content = $"Pont: {_viewModel.pointFirstPlayer}";
                    textFirstPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.firstPlayerShipsCounts);
                    _viewModel.endGame(_viewModel.firstPlayerShipsCounts, _actualPlayer, this);
                }
            }
            else
            {
                canvasFirstPlayer.IsEnabled = false;
            }
            
        }

        private void clickOnSecondPlayerCanvas(object sender, MouseButtonEventArgs e)
        {
            if(_actualGameType == GameType.Pvp)
            {
                if (_actualPlayer == Player.FirstPlayer)
                {
                    int[,] shipLayout = _viewModel._secondPlayerRefShipmatrix;
                    int[] shipCounts = _viewModel.secondPlayerShipsCounts;
                    int points = _viewModel.pointSecondPlayer;
                    _viewModel.guessTile(canvasSecondPlayer, ref shipLayout, ref shipCounts, ref _actualPlayer, _viewModel.GetPointOnCanvas(canvasSecondPlayer));
                    _viewModel.increaseRound();
                    textRound.Content = _viewModel.rounds;
                    textPointSecondPlayer.Content = $"Pont: {_viewModel.pointSecondPlayer}";
                    textSecondPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.secondPlayerShipsCounts);
                    _viewModel.endGame(_viewModel.secondPlayerShipsCounts, _actualPlayer, this);
                }
            }
            else if(_actualGameType == GameType.PvAi)
            {
                if (_actualPlayer == Player.FirstPlayer)
                {
                    int[,] aiPlayerShipLayout = _viewModel._secondPlayerRefShipmatrix;
                    int[,] firstPlayerShipLayout = _viewModel._firstPlayerRefShipMatrix;
                    int[] shipCounts = _viewModel.secondPlayerShipsCounts;
                    int[] aiShipCounts = _viewModel.firstPlayerShipsCounts;
                    int points = _viewModel.pointSecondPlayer;
                    _viewModel.guessTile(canvasSecondPlayer, ref aiPlayerShipLayout, ref shipCounts, ref _actualPlayer, _viewModel.GetPointOnCanvas(canvasSecondPlayer));
                    _viewModel.increaseRound();
                    textRound.Content = _viewModel.rounds;
                    textPointFirstPlayer.Content = $"Pont: {_viewModel.pointSecondPlayer}";
                    textSecondPlayerShipCounts.Content = _viewModel.shipCountToString(_viewModel.secondPlayerShipsCounts);
                    _viewModel.endGame(_viewModel.secondPlayerShipsCounts, _actualPlayer, this);
                    //AI turn
                    int prevAiPoint = _viewModel.pointFirstPlayer;
                    int[] aiArray = aIPlayer.AIShoots(ref aiPrevHits, ref aiPrevShots);
                    aiPrevShots.Add(aiArray);
                    Vector aiShot = new Vector(aiArray[0], aiArray[1]);
                    _viewModel.guessTile(canvasFirstPlayer, ref firstPlayerShipLayout, ref aiShipCounts, ref _actualPlayer, aiShot);
                    if (_viewModel.pointFirstPlayer > prevAiPoint) aiPrevHits.Add(aiArray);
                    textPointSecondPlayer.Content = $"Pont: {_viewModel.pointFirstPlayer}";

                    _viewModel.endGame(_viewModel.firstPlayerShipsCounts, Player.AIPlayer, this);
                    //textRound.Content = _viewModel.rounds;

                }
            }
            
            
        }

        private void hint(object sender, KeyEventArgs e)
        {
            if (_actualGameType == GameType.PvAi && e.Key == Key.H && (Keyboard.Modifiers == (ModifierKeys.Control)))
            {
                _viewModel.showAiShips(_viewModel.originAiArray, canvasSecondPlayer);
            }
        }
        private void hideHint(object sender, KeyEventArgs e)
        {
            if (_actualGameType == GameType.PvAi && e.Key == Key.H && (Keyboard.Modifiers == (ModifierKeys.Control)))
            {
                _viewModel.restoreCanvas(_viewModel._secondPlayerRefShipmatrix, canvasSecondPlayer);
            }
        }
    }
}
