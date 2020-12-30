namespace PasswordManager.Models
{
    public class Literals
    {
        public const string Login = "Login";
        public const string Password = "Password";
        public const string AppName = "Password Manager";
        public const string RegexLettersDigits = "[a-zA-Z0-9]{1,20}";
        public const string RegexLettersDigitsSpace = "[a-zA-Z0-9 ]{1,20}";
        public const string RegexLettersDigitsSpaceSpecials = "[a-zA-Z0-9 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]{1,20}";
        public const string RegexDigits = "[0-9]{1,2}";

        #region Messages

        public const string Information = "Informacja";
        public const string Warning = "Uwaga!";
        public const string MessageLoginError = "Podano błędny login lub hasło";
        public const string MessageAccountExist = "Taki użytkownik już istnieje";
        public const string MessageWrongPassword = "Podano błędne hasło";
        public const string MessageAccountNotExist = "Taki użytkownik nie istnieje";
        public const string MessagePasswordChanged = "Hasło zostało zmienione pomyślnie.";
        public const string MessageDeleteQuestion = "Czy na pewno usunąć?";

        #endregion Messages
    }
}
