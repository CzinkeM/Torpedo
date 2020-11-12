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

    public partial class Menu : Window
    {
        private const string TYPE_VS_AI = "ai";
        private const string TYPE_PLAYER_VS_PLAYER = "player";
        private const string ERROR_MSG = "Please choose gametype";
        public Menu()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            var PickTile = new MainWindow();
            if ((bool)radioPlayer.IsChecked)
            {
                PickTile.Show();
                this.Close();
                //TODO:Pass gametype
            }
            else if((bool) radioAi.IsChecked)
            {
                PickTile.Show();
                this.Close();
                //TODO:Pass gametype
            }else
            {
                labelError.Foreground = Brushes.Red;
                labelError.Content = ERROR_MSG;
            }

        }
    }
}
