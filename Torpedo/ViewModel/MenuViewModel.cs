using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Torpedo.ViewModel
{
    public enum GameType
    {
        PvAi,
        Pvp
    }

    public class MenuViewModel
    {
        public const string prop_name = "playerName";
        public const string prop_gameType = "gameType";
        private Label _labelMessage;
        private int _width= 10;
        private int _height = 10;
        private Menu _menu;
        
        public MenuViewModel(Label label, Menu menu)
        {
            _labelMessage = label;
            _menu = menu;
        }
        private void SaveName(String name)
        {
            Application.Current.Properties[prop_name] = name;
        }
        private bool ValidateName(TextBox text)
        {
            string name = text.Text.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(name)) return true;
            else return false;
        }
        private bool ValidateGameType(RadioButton radioButtonVsAi, RadioButton radioButtonVsPlayer)
        {
            if (radioButtonVsAi.IsChecked == true) return true;
            else if (radioButtonVsPlayer.IsChecked == true) return true;
            else return false;
            
        }
        private void SaveGameType(GameType gameType)
        {
            Application.Current.Properties[prop_gameType] = gameType;
        }
        public void StartGame(TextBox name, RadioButton ai, RadioButton pvp)
        {
            if (ValidateName(name) && ValidateGameType(ai, pvp))
            {
                _labelMessage.Content = Error.ERROR_DELETE_MESSAGE;
                if (ai.IsChecked == true) SaveGameType(GameType.PvAi);
                else SaveGameType(GameType.Pvp);
                SaveName(name.Text.Trim());
                FirstPlayerPicker firstPlayerPicker = new FirstPlayerPicker(_width, _height);
                _menu.Close();
                firstPlayerPicker.Show();
                
            } else if (!ValidateGameType(ai, pvp)) _labelMessage.Content = Error.ERROR_CHOOSE_GAMETYPE;
            else _labelMessage.Content = Error.ERROR_NOT_VALID_NAME;
            
        }
    }
}
