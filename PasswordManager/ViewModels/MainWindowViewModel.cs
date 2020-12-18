using PasswordManager.Models;
using PasswordManager.Services;
using PasswordManager.Validation;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.ViewModels
{
    public class MainWindowViewModel : ValidatableBase
    {
        private readonly IGeneratorService _generatorService;
        private readonly IDataService _dataService;
        private readonly ICopyService _copyService;
        private readonly IAccountService _accountService;

        public MainWindowViewModel(IGeneratorService generatorService, IDataService dataService, 
            ICopyService copyService, IAccountService acconuntService)
        {
            _generatorService = generatorService;
            _dataService = dataService;
            _copyService = copyService;
            _accountService = acconuntService;            
        }

        private void Passwords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var newItem = e.NewItems[0] as PasswordWrapper;
                    if (newItem != null)
                        _dataService.AddPassword(newItem, _accountService.LoggedUser);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var passwordWrapper = e.OldItems[0] as PasswordWrapper;
                    if (passwordWrapper != null)
                        _dataService.DeletePassword(passwordWrapper.Id);
                    break;
            }
        }

        private void SetTitle()
        {
            Title = $"{Literals.AppName} - {_accountService.LoggedUser.Username}";
        }

        private ObservableCollection<PasswordWrapper> _passwords;
        public ObservableCollection<PasswordWrapper> Passwords
        {
            get { return _passwords; }
            set { SetProperty(ref _passwords, value); }
        }

        private string _keyValue;
        public string KeyValue
        {
            get { return _keyValue; }
            set { SetProperty(ref _keyValue, value); }
        }

        private string _login;
        [RegularExpression(Literals.RegexLettersDigits)]
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _comment;
        [RegularExpression(Literals.RegexLettersDigits)]
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        private string _keyName;
        [RegularExpression(Literals.RegexLettersDigits)]
        public string KeyName
        {
            get { return _keyName; }
            set { SetProperty(ref _keyName, value); }
        }

        private int _KeyLength;
        [RegularExpression(Literals.RegexDigits)]
        public int KeyLength
        {
            get { return _KeyLength; }
            set { SetProperty(ref _KeyLength, value); }
        }

        private bool _chars;
        public bool Chars
        {
            get { return _chars; }
            set { SetProperty(ref _chars, value); }
        }

        private bool _capitalLetters;
        public bool CapitalLetters
        {
            get { return _capitalLetters; }
            set { SetProperty(ref _capitalLetters, value); }
        }

        private bool _smallLetters;
        public bool SmallLetters
        {
            get { return _smallLetters; }
            set { SetProperty(ref _smallLetters, value); }
        }

        private bool _digits;
        public bool Digits
        {
            get { return _digits; }
            set { SetProperty(ref _digits, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private PasswordWrapper _selectedItem;
        public PasswordWrapper SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private KeyTypes KeyType
        {
            get
            {
                var keyType = new KeyTypes();
                if (Digits)
                    keyType.Digits = true;

                if (CapitalLetters)
                    keyType.CapitalLetters  = true;

                if (SmallLetters)
                    keyType.SmallLetters = true;

                if (Chars)
                    keyType.Chars = true;

                return keyType;
            }
        } 

        private DelegateCommand _generateKeyCommand;
        public DelegateCommand GenerateKeyCommand =>
            _generateKeyCommand ?? (_generateKeyCommand = 
            new DelegateCommand(ExecuteGenerateKeyCommad, CanGenerateCommand)
            .ObservesProperty(() => Digits)
            .ObservesProperty(() => CapitalLetters)
            .ObservesProperty(() => SmallLetters)
            .ObservesProperty(() => Chars));

        private bool CanGenerateCommand()
            => Digits || CapitalLetters || SmallLetters || Chars;

        void ExecuteGenerateKeyCommad()
        {
            KeyValue = _generatorService.GenerateKey(KeyLength, KeyType);
        }

        private DelegateCommand _copyCommand;
        public DelegateCommand CopyCommand =>
            _copyCommand ?? (_copyCommand = 
            new DelegateCommand(ExecuteCopyCommand, CanExecuteCopyCommand)
            .ObservesProperty(() => KeyValue));

        private bool CanExecuteCopyCommand()
            => KeyValue?.Length > 0;

        void ExecuteCopyCommand()
            => _copyService.CopyText(KeyValue);

        private DelegateCommand _savePasswordCommand;
        public DelegateCommand SavePasswordCommand =>
            _savePasswordCommand ?? (_savePasswordCommand = 
            new DelegateCommand(ExecuteSavePasswordCommand, CanExecutePasswordCommand)
            .ObservesProperty(() => KeyName)
            .ObservesProperty(() => KeyValue));

        void ExecuteSavePasswordCommand()
        {
            var password = new PasswordWrapper
            {
                Username = _accountService.LoggedUser.Username,
                Name = KeyName,
                Password = KeyValue,
                Comment = Comment
            };
            Passwords.Add(password);
        }

        bool CanExecutePasswordCommand()
            => !string.IsNullOrWhiteSpace(KeyValue) &&
            !string.IsNullOrWhiteSpace(KeyName);

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedComand));

        void ExecuteLoadedComand()
        {
            SetTitle();
            
            Passwords = new ObservableCollection<PasswordWrapper>(_dataService.GetPasswords());
            Passwords.CollectionChanged += Passwords_CollectionChanged;
            KeyLength = 10;
        }

        private DelegateCommand<RoutedEventArgs> _passwordChangedCommand;
        public DelegateCommand<RoutedEventArgs> PasswordChangedCommand =>
            _passwordChangedCommand ?? (_passwordChangedCommand = new DelegateCommand<RoutedEventArgs>(ExecutePasswordChangedCommand));

        void ExecutePasswordChangedCommand(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            
            KeyValue = passwordBox.Password;
        }
        private DelegateCommand _deleteItemCommand;
        public DelegateCommand DeleteItemCommand =>
            _deleteItemCommand ?? (_deleteItemCommand = new DelegateCommand(ExecuteDeleteItemCommand));

        void ExecuteDeleteItemCommand()
            => _passwords.Remove(SelectedItem);
    }
}
