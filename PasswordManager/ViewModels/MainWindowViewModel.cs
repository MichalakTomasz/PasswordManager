using PasswordManager.EntityModels;
using PasswordManager.Models;
using PasswordManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows;

namespace PasswordManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IGeneratorService _generatorService;
        private readonly IDataService _dataService;

        public MainWindowViewModel(IGeneratorService generatorService, IDataService dataService)
        {
            _generatorService = generatorService;
            _dataService = dataService;
        }

        private IEnumerable<PasswordSet> _passwords;
        public IEnumerable<PasswordSet> Passwords
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
            new DelegateCommand(ExecuteGenerateKeyCommad));

        void ExecuteGenerateKeyCommad()
        {
            KeyValue = _generatorService.GenerateKey(KeyLength, KeyType);
        }

        private DelegateCommand _copyCommand;
        public DelegateCommand CopyCommand =>
            _copyCommand ?? (_copyCommand = new DelegateCommand(ExecuteCopyCommand));

        void ExecuteCopyCommand()
        {
            Clipboard.SetText(KeyValue); 
        }
    }
}
