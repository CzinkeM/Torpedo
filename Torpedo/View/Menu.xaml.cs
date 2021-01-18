using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Torpedo.View;
using Torpedo.ViewModel;

namespace Torpedo
{
    public partial class Menu : Window
    {
        Error error;
        private MenuViewModel _viewModel;
        public Menu()
        {
            InitializeComponent();
            _viewModel = new MenuViewModel(labelError,this);
            error = new Error(labelError);
        }

        private void clickStartButton(object sender, RoutedEventArgs e)
        {
            _viewModel.StartGame(inputName, radioAi, radioPlayer);
        }

        private void clickRankListButtin(object sender, RoutedEventArgs e)
        {
            Ranking ranking = new Ranking();
            ranking.Show();
            this.Close();
        }
    }
}
