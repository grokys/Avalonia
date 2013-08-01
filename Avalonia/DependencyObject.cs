// -----------------------------------------------------------------------
// <copyright file="DependencyObject.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Data;
    using Avalonia.Threading;

    public class DependencyObject : DispatcherObject, IObservableDependencyObject
    {
        private static Dictionary<Type, Dictionary<string, DependencyProperty>> propertyDeclarations =
            new Dictionary<Type, Dictionary<string, DependencyProperty>>();

        private Dictionary<DependencyProperty, object> properties =
            new Dictionary<DependencyProperty, object>();

        private Dictionary<DependencyProperty, BindingExpressionBase> propertyExpressions =
            new Dictionary<DependencyProperty, BindingExpressionBase>();

        private Dictionary<string, List<DependencyPropertyChangedEventHandler>> propertyChangedHandlers =
            new Dictionary<string, List<DependencyPropertyChangedEventHandler>>();

        public bool IsSealed
        {
            get { return false; }
        }

        public DependencyObjectType DependencyObjectType
        {
            get { return DependencyObjectType.FromSystemType(this.GetType()); }
        }

        void IObservableDependencyObject.AttachPropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler)
        {
            List<DependencyPropertyChangedEventHandler> handlers;

            if (!this.propertyChangedHandlers.TryGetValue(propertyName, out handlers))
            {
                handlers = new List<DependencyPropertyChangedEventHandler>();
                this.propertyChangedHandlers.Add(propertyName, handlers);
            }

            handlers.Add(handler);
        }

        void IObservableDependencyObject.RemovePropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler)
        {
            List<DependencyPropertyChangedEventHandler> handlers;

            if (this.propertyChangedHandlers.TryGetValue(propertyName, out handlers))
            {
                handlers.Remove(handler);
            }
        }

        public void ClearValue(DependencyProperty dp)
        {
            if (this.IsSealed)
            {
                throw new InvalidOperationException("Cannot manipulate property values on a sealed DependencyObject");
            }

            this.properties[dp] = null;
        }

        public void ClearValue(DependencyPropertyKey key)
        {
            this.ClearValue(key.DependencyProperty);
        }

        public void CoerceValue(DependencyProperty dp)
        {
            PropertyMetadata pm = dp.GetMetadata(this);
            if (pm.CoerceValueCallback != null)
            {
                pm.CoerceValueCallback(this, this.GetValue(dp));
            }
        }

        public LocalValueEnumerator GetLocalValueEnumerator()
        {
            return new LocalValueEnumerator(this.properties);
        }

        public object GetValue(DependencyProperty dp)
        {
            object val;

            if (!this.properties.TryGetValue(dp, out val))
            {
                val = dp.DefaultMetadata.DefaultValue;
                this.properties[dp] = val;
            }

            return val;
        }

        public void InvalidateProperty(DependencyProperty dp)
        {
            BindingExpressionBase binding;

            if (this.propertyExpressions.TryGetValue(dp, out binding))
            {
                object value = this.GetValue(dp);
                object oldValue = value;

                value = binding.GetCurrentValue();

                if (!object.Equals(oldValue, value))
                {
                    this.properties[dp] = value;
                    this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(
                        dp,
                        oldValue,
                        value));
                }
            }
        }

        public object ReadLocalValue(DependencyProperty dp)
        {
            object val = this.properties[dp];
            return val == null ? DependencyProperty.UnsetValue : val;
        }

        public void SetValue(DependencyProperty dp, object value)
        {
            if (this.IsSealed)
            {
                throw new InvalidOperationException("Cannot manipulate property values on a sealed DependencyObject");
            }

            if (!(value == DependencyProperty.UnsetValue || value is BindingExpressionBase || dp.IsValidType(value)))
            {
                throw new ArgumentException("value not of the correct type for this DependencyProperty");
            }

            ValidateValueCallback validate = dp.ValidateValueCallback;

            if (validate != null && !validate(value))
            {
                throw new Exception("Value does not validate");
            }
            else
            {
                object oldValue = this.GetValue(dp);
                BindingExpressionBase binding = value as BindingExpressionBase;

                if (value == DependencyProperty.UnsetValue)
                {
                    this.properties.Remove(dp);
                    this.propertyExpressions.Remove(dp);
                    value = this.GetValue(dp);
                }
                else
                {
                    if (binding != null)
                    {
                        this.propertyExpressions.Add(dp, binding);
                        value = binding.GetCurrentValue();
                    }
                    else
                    {
                        this.propertyExpressions.Remove(dp);
                    }

                    this.properties[dp] = value;
                }

                if (!object.Equals(oldValue, value))
                {
                    this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(dp, oldValue, value));
                }
            }
        }

        public void SetValue(DependencyPropertyKey key, object value)
        {
            this.SetValue(key.DependencyProperty, value);
        }

        internal static DependencyProperty FromName(Type type, string name)
        {
            Dictionary<string, DependencyProperty> list;
            DependencyProperty result;

            if (propertyDeclarations.TryGetValue(type, out list))
            {
                if (list.TryGetValue(name, out result))
                {
                    return result;
                }
            }

            throw new KeyNotFoundException("Dependency property not found.");
        }

        internal bool IsUnset(DependencyProperty dependencyProperty)
        {
            return !this.properties.ContainsKey(dependencyProperty);
        }

        internal static void Register(Type t, DependencyProperty dp)
        {
            if (!propertyDeclarations.ContainsKey(t))
            {
                propertyDeclarations[t] = new Dictionary<string, DependencyProperty>();
            }

            Dictionary<string, DependencyProperty> typeDeclarations = propertyDeclarations[t];
            if (!typeDeclarations.ContainsKey(dp.Name))
            {
                typeDeclarations[dp.Name] = dp;
            }
            else
            {
                throw new ArgumentException("A property named " + dp.Name + " already exists on " + t.Name);
            }
        }

        protected virtual void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            PropertyMetadata pm = e.Property.GetMetadata(this);

            if (pm != null)
            {
                if (pm.PropertyChangedCallback != null)
                {
                    pm.PropertyChangedCallback(this, e);
                }
            }

            List<DependencyPropertyChangedEventHandler> handlers;

            if (this.propertyChangedHandlers.TryGetValue(e.Property.Name, out handlers))
            {
                foreach (var handler in handlers.ToArray())
                {
                    handler(this, e);
                }
            }

            FrameworkPropertyMetadata metadata = e.Property.GetMetadata(this) as FrameworkPropertyMetadata;
            UIElement uiElement = this as UIElement;

            if (metadata != null && uiElement != null)
            {
                if (metadata.AffectsArrange)
                {
                    uiElement.InvalidateArrange();
                }

                if (metadata.AffectsMeasure)
                {
                    uiElement.InvalidateMeasure();
                }

                if (metadata.AffectsRender)
                {
                    uiElement.InvalidateVisual();
                }
            }
        }

        protected virtual bool ShouldSerializeProperty(DependencyProperty dp)
        {
            throw new NotImplementedException();
        }
    }
}