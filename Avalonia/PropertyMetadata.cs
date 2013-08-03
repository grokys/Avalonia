// -----------------------------------------------------------------------
// <copyright file="PropertyMetadata.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;

    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);

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
                return this.coerceValueCallback;
            }

            set
            {
                this.CheckNotSealed();
                this.coerceValueCallback = value;
            }
        }

        protected bool IsSealed
        {
            get { return this.isSealed; }
        }

        internal void Merge(PropertyMetadata baseMetadata, DependencyProperty dp, Type targetType)
        {
            this.Merge(baseMetadata, dp);
            this.OnApply(dp, targetType);
            this.isSealed = true;
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
            if (this.defaultValue == null)
            {
                this.defaultValue = baseMetadata.defaultValue;
            }

            if (this.propertyChangedCallback == null)
            {
                this.propertyChangedCallback = baseMetadata.propertyChangedCallback;
            }

            if (this.coerceValueCallback == null)
            {
                this.coerceValueCallback = baseMetadata.coerceValueCallback;
            }
        }

        protected virtual void OnApply(DependencyProperty dp, Type targetType)
        {
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