using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PasswordManager.BaseClasses
{
    public abstract class WrapperBase : ValidatableBase, IChangeTracking
    {
        private Dictionary<string, object> _originalValues = new Dictionary<string, object>();

        protected override bool SetProperty<TProp>(ref TProp storage, TProp value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(value, storage))
                return true;

            ChangeOriginalValue(value, propertyName);
            RaisePropertyChanged($"IsChanged{propertyName}");
            RaisePropertyChanged(nameof(IsChanged));
            var result = base.SetProperty(ref storage, value, propertyName);

            return result;
        }

        public bool IsChanged => _originalValues.Any();
        private void ChangeOriginalValue<T>(T NewValue, string propertyName)
        {
            if (!_originalValues.ContainsKey(propertyName))
                _originalValues[propertyName] = NewValue;
            else
                if (Equals(_originalValues[propertyName], NewValue))
                _originalValues.Remove(propertyName);
        }

        protected bool GetIsChanged(string propertyName)
            => _originalValues.ContainsKey(propertyName);

        public void AcceptChanges()
            => _originalValues.Clear();

        protected T GetOriginalValue<T>(string propertyName)
        {
            if (_originalValues.ContainsKey(propertyName))
                return (T)_originalValues[propertyName];
            else
                return (T)this.GetType().GetProperty(propertyName).GetValue(this);
        }
    }
}
