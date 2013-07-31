namespace Avalonia
{
    using System;

    public class PropertyMetadata
    {
        private object defaultValue;
        private bool isSealed;
        private PropertyChangedCallback propertyChangedCallback;
        private CoerceValueCallback coerceValueCallback;

        public PropertyMetadata()
            : this(null, null, null)
        {
        }

        public PropertyMetadata(object defaultValue)
            : this(defaultValue, null, null)
        {
        }

        public PropertyMetadata(PropertyChangedCallback propertyChangedCallback)
            : this(null, propertyChangedCallback, null)
        {
        }

        public PropertyMetadata(
            object defaultValue, 
            PropertyChangedCallback propertyChangedCallback)
            : this(defaultValue, propertyChangedCallback, null)
        {
        }

        public PropertyMetadata(
            object defaultValue, 
            PropertyChangedCallback propertyChangedCallback, 
            CoerceValueCallback coerceValueCallback)
        {
            this.CheckNotUnset(defaultValue);
            this.defaultValue = defaultValue;
            this.propertyChangedCallback = propertyChangedCallback;
            this.coerceValueCallback = coerceValueCallback;
        }

        public object DefaultValue
        {
            get 
            {
                return this.defaultValue; 
            }

            set
            {
                this.CheckNotSealed();
                this.CheckNotUnset(value);
                this.defaultValue = value;
            }
        }

        public PropertyChangedCallback PropertyChangedCallback
        {
            get 
            {
                return this.propertyChangedCallback; 
            }

            set
            {
                this.CheckNotSealed();
                this.propertyChangedCallback = value;
            }
        }

        public CoerceValueCallback CoerceValueCallback
        {
            get 
            { 
                return coerceValueCallback; 
            }

            set
            {
                CheckNotSealed();
                coerceValueCallback = value;
            }
        }

        protected bool IsSealed
        {
            get { return this.isSealed; }
        }

        protected void CheckNotSealed()
        {
            if (this.IsSealed)
            {
                throw new InvalidOperationException("Cannot change metadata once it has been applied to a property");
            }
        }

        protected virtual void Merge(PropertyMetadata baseMetadata, DependencyProperty dp)
        {
            if (defaultValue == null)
            {
                defaultValue = baseMetadata.defaultValue;
            }

            if (propertyChangedCallback == null)
            {
                propertyChangedCallback = baseMetadata.propertyChangedCallback;
            }

            if (coerceValueCallback == null)
            {
                coerceValueCallback = baseMetadata.coerceValueCallback;
            }
        }

        protected virtual void OnApply(DependencyProperty dp, Type targetType)
        {
        }

        internal void Merge(PropertyMetadata baseMetadata, DependencyProperty dp, Type targetType)
        {
            Merge(baseMetadata, dp);
            OnApply(dp, targetType);
            isSealed = true;
        }

        private void CheckNotUnset(object value)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                throw new ArgumentException("Cannot set property metadata's default value to 'Unset'");
            }
        }
    }
}