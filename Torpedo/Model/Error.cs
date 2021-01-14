using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Torpedo
{
    class Error
    {
        private Label _errorLabel;
        public const string ERROR = "Some error";
        public const string ERROR_NO_MORE_SHIP = "no more ship left";
        public const string ERROR_NO_SPACE = "there is no more space";
        public const string ERROR_CHOOSE_GAMETYPE = "Choose gametype";
        public const string ERROR_DELETE_MESSAGE = "";
        public const string ERROR_NOT_VALID_NAME = "The given name is not valid";
        public Error(Label LabelToShowError)
        {
            _errorLabel = LabelToShowError;
        }
    }
}
