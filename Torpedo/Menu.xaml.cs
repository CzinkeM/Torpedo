using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Torpedo
{
    public partial class Menu : Window
    {
        Error error;
        int _colums;
        int _rows;
        public Menu()
        {
            InitializeComponent();
            error = new Error(labelError);
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            if (isGameSizeChecked())
            {
                if ((bool)radioPlayer.IsChecked)
                {
                    FirstPlayerPicker shipPlacer = new FirstPlayerPicker(GetGameSize(), GameType.PlayerVsPlayer,_colums,_rows);
                    shipPlacer.Show();
                    this.Close();            
                }
                else if ((bool)radioAi.IsChecked)
                {
                    FirstPlayerPicker shipPlacer = new FirstPlayerPicker(GetGameSize(), GameType.PlayerVsAi,_colums,_rows);
                    shipPlacer.Show();
                    this.Close();
                }
                else
                {
                    error.ShowErrorMessage(Error.ERROR_CHOOSE_GAMETYPE);
                    labelError.Foreground = Brushes.Red;
                }
            }
            else
            {
                error.ShowErrorMessage(Error.ERROR_CHOOSE_GAMESIZE);
                labelError.Foreground = Brushes.Red;
            }
        }
        private bool isGameSizeChecked()
        {
            if ((bool)radioSizeSmall.IsChecked)
            {
                return true;
            }
            else if ((bool)radioSizeMedium.IsChecked)
            {
                return true;
            }
            else if ((bool)radioSizeLarge.IsChecked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private GameSize GetGameSize()
        {
            if ((bool)radioSizeSmall.IsChecked)
            {
                _colums = 10;
                _rows = 10;
                return GameSize.Small;
            }
            else if ((bool)radioSizeMedium.IsChecked)
            {
                _colums = 15;
                _rows = 15;
                return GameSize.Medium;
            }
            else
            {
                _colums = 20;
                _rows = 20;
                return GameSize.Large;
            }
        }

        //Todo név megadási lehetőség
        //Játék kiválasztása (kicsi, közepes, nagy) 5, 10 ,25 méretű pályák
        //Szabályzat ismertetésére felugró ablak
        //Kapcsolat icon ami githubra visz
    }
}
