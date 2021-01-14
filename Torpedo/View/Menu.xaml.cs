using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Torpedo.ViewModel;

namespace Torpedo
{
    public partial class Menu : Window
    {
        Error error;
        int _colums;
        int _rows;
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
        //Kapcsolat icon ami githubra visz
    }
}
