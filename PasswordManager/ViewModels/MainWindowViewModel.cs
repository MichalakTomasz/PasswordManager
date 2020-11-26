﻿using PasswordManager.EntityModels;
using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IGeneratorService _generatorService;
        private readonly IDataService _dataService;
        private readonly ICopyService _copyService;
        private readonly IAccountService _accountService;
        private readonly IAesCryptographicService _aesCryptographicService;
        private readonly IDataBinarySerializeService _dataBinarySerializeService;

        public MainWindowViewModel(IGeneratorService generatorService, IDataService dataService, 
            ICopyService copyService, IAccountService acconuntService, 
            IAesCryptographicService aesCryptographicService, IDataBinarySerializeService dataBinarySerializeService)
        {
            _generatorService = generatorService;
            _dataService = dataService;
            _copyService = copyService;
            _accountService = acconuntService;
            _aesCryptographicService = aesCryptographicService;
            _dataBinarySerializeService = dataBinarySerializeService;
        }

        private void SetTitle()
        {
            Title = $"{Literals.AppName} - {_accountService.LoggedUser.Username}";
        }

        private ObservableCollection<PasswordModel> _passwords;
        public ObservableCollection<PasswordModel> Passwords
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
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        private string _keyName;
        public string KeyName
        {
            get { return _keyName; }
            set { SetProperty(ref _keyName, value); }
        }

        private int _KeyLength = 10;
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
            var passwordByteBuffer = _dataBinarySerializeService.Serialize<string>(KeyValue);
            var encryptedPassword = _aesCryptographicService.Encrypt(passwordByteBuffer);

            var passwordSet = new PasswordSet
            {
                Name = KeyName,
                EncryptedPassword = encryptedPassword,
                User = _accountService.LoggedUser,
                Username = _accountService.LoggedUser.Username
            };
            _dataService.SavePassword(passwordSet);
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
            
            Passwords = new ObservableCollection<PasswordModel>(_dataService.GetPasswords());
        }

        private DelegateCommand<RoutedEventArgs> _passwordChangedCommand;
        public DelegateCommand<RoutedEventArgs> PasswordChangedCommand =>
            _passwordChangedCommand ?? (_passwordChangedCommand = new DelegateCommand<RoutedEventArgs>(ExecutePasswordChangedCommand));

        void ExecutePasswordChangedCommand(RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            
            KeyValue = passwordBox.Password;
        }
    }
}
