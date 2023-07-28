using System;
using Utils.Disposables;

namespace Core.ObservableProperties
{
    public abstract class ObservableProperty<T>
    {
        public delegate void OnPropertyChanged(T newValue, T oldValue);

        protected event OnPropertyChanged OnChanged;

        private T propertyValue;

        public T Value
        {
            get => propertyValue;
            set => SetValue(value);
        }

        private void SetValue(T value)
        {
            var isEquals = propertyValue?.Equals(value) ?? false;
            if (isEquals)
            {
                return;
            }

            var oldValue = propertyValue;

            propertyValue = value;

            OnChangedCallback(value, oldValue);
        }

        private void OnChangedCallback(T newValue, T oldValue)
            => OnChanged?.Invoke(newValue, oldValue);

        public IDisposable Subscribe(OnPropertyChanged call)
        {
            OnChanged += call;

            return new ActionDisposable(() => OnChanged -= call);
        }
    }
}