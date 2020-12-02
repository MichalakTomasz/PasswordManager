using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PasswordManager.Validation
{
    public class ValidatableBase : BindableBase, INotifyDataErrorInfo
    {
        protected virtual void Validate<TProp>(TProp value, string propertyName)
        {
            if (_errorDictionary.ContainsKey(propertyName))
                _errorDictionary.Remove(propertyName);

            var validationContext = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var validationResult = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResult);

            if (validationResult.Any())
            {
                var errorList = validationResult.Select(v => v.ErrorMessage);
                _errorDictionary.Add(propertyName, errorList);
            }
        }

        public bool HasErrors => _errorDictionary.Any();
        protected bool IsValid => !HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorChanged([CallerMemberName] string propertyName = null)
        {
            var handler = ErrorsChanged;
            handler?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected override bool SetProperty<TProp>(
            ref TProp storage, TProp value, [CallerMemberName] string propertyName = null)
        {
            Validate(value, propertyName);
            var result = base.SetProperty(ref storage, value, propertyName);
            RaisePropertyChanged(nameof(IsValid));
            return result;
        }

        protected override bool SetProperty<TProp>(ref TProp storage, TProp value,
            Action onChanged, [CallerMemberName] string propertyName = null)
        {
            Validate(value, propertyName);
            var result = base.SetProperty(ref storage, value, onChanged, propertyName);
            RaisePropertyChanged(nameof(IsValid));
            return result;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(propertyName) &&
                _errorDictionary.ContainsKey(propertyName) &&
                _errorDictionary[propertyName].Count() > 0)
                return _errorDictionary[propertyName];
            else
                return null;
        }

        private Dictionary<string, IEnumerable<string>> _errorDictionary =
            new Dictionary<string, IEnumerable<string>>();
    }
}
