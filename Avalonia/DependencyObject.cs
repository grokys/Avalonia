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
    using Avalonia.Media;
    using Avalonia.Threading;

    public class DependencyObject : DispatcherObject, IObservableDependencyObject
    {
        private static Dictionary<Type, Dictionary<string, DependencyProperty>> propertyDeclarations =
            new Dictionary<Type, Dictionary<string, DependencyProperty>>();

        private Dictionary<DependencyProperty, object> properties =
            new Dictionary<DependencyProperty, object>();

        private Dictionary<DependencyProperty, BindingExpressionBase> propertyBindings =
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
                val = this.GetDefaultValue(dp);

                if (val == null && dp.PropertyType.IsValueType)
                {
                    val = Activator.CreateInstance(dp.PropertyType);
                }
            }

            return val;
        }

        public void InvalidateProperty(DependencyProperty dp)
        {
            BindingExpressionBase binding;

            if (this.propertyBindings.TryGetValue(dp, out binding))
            {
                object value = this.GetValue(dp);
                object oldValue = value;

                value = binding.GetValue();

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

        public void SetBinding(DependencyProperty dp, string path)
        {
            this.SetBinding(dp, new Binding(path));
        }

        public void SetBinding(DependencyProperty dp, BindingBase binding)
        {
            Binding b = binding as Binding;

            if (b == null)
            {
                throw new NotSupportedException("Unsupported binding type.");
            }

            this.SetBinding(dp, b);
        }

        [AvaloniaSpecific]
        public BindingExpression SetBinding(DependencyProperty dp, Binding binding)
        {
            PropertyPathParser pathParser = new PropertyPathParser();
            BindingExpression expression = new BindingExpression(pathParser, this, dp, binding);
            object oldValue = this.GetValue(dp);
            object newValue = expression.GetValue();

            this.propertyBindings.Add(dp, expression);
            this.properties[dp] = newValue;

            if (!object.Equals(oldValue, newValue))
            {
                this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(dp, oldValue, newValue));
            }

            return expression;
        }

        public void SetValue(DependencyProperty dp, object value)
        {
            if (this.IsSealed)
            {
                throw new InvalidOperationException("Cannot manipulate property values on a sealed DependencyObject.");
            }

            if (value != DependencyProperty.UnsetValue && !dp.IsValidType(value))
            {
                throw new ArgumentException("Value is not of the correct type for this DependencyProperty.");
            }

            if (dp.ValidateValueCallback != null && !dp.ValidateValueCallback(value))
            {
                throw new Exception("Value does not validate.");
            }

            object oldValue = this.GetValue(dp);

            if (value == DependencyProperty.UnsetValue)
            {
                this.properties.Remove(dp);
                value = dp.DefaultMetadata.DefaultValue;
            }
            else
            {
                this.properties[dp] = value;
            }

            this.propertyBindings.Remove(dp);

            if (!object.Equals(oldValue, value))
            {
                this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(dp, oldValue, value));
            }
        }

        public void SetValue(DependencyPropertyKey key, object value)
        {
            this.SetValue(key.DependencyProperty, value);
        }

        internal static DependencyProperty PropertyFromName(Type type, string name)
        {
            Dictionary<string, DependencyProperty> list;
            DependencyProperty result;
            Type t = type;

            while (t != null)
            {
                if (propertyDeclarations.TryGetValue(t, out list))
                {
                    if (list.TryGetValue(name, out result))
                    {
                        return result;
                    }
                }

                t = t.BaseType;
            }

            throw new KeyNotFoundException(string.Format(
                "Dependency property '{0}' could not be found on type '{1}'.",
                name,
                type.FullName));
        }

        internal static void Register(Type t, DependencyProperty dp)
        {
            Dictionary<string, DependencyProperty> typeDeclarations;

            if (!propertyDeclarations.TryGetValue(t, out typeDeclarations))
            {
                typeDeclarations = new Dictionary<string, DependencyProperty>();
                propertyDeclarations.Add(t, typeDeclarations);
            }

            if (!typeDeclarations.ContainsKey(dp.Name))
            {
                typeDeclarations[dp.Name] = dp;
            }
            else
            {
                throw new ArgumentException("A property named " + dp.Name + " already exists on " + t.Name);
            }
        }

        internal bool IsRegistered(Type t, DependencyProperty dp)
        {
            Dictionary<string, DependencyProperty> typeDeclarations;
            DependencyProperty found;

            if (propertyDeclarations.TryGetValue(t, out typeDeclarations) &&
                typeDeclarations.TryGetValue(dp.Name, out found))
            {
                return found == dp;
            }

            return false;
        }

        internal bool IsUnset(DependencyProperty dependencyProperty)
        {
            return !this.properties.ContainsKey(dependencyProperty);
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

                if (metadata.Inherits)
                {
                    foreach (DependencyObject child in VisualTreeHelper.GetChildren(this))
                    {
                        child.InheritedValueChanged(e);
                    }
                }
            }
        }

        protected virtual bool ShouldSerializeProperty(DependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        private object GetDefaultValue(DependencyProperty dp)
        {
            PropertyMetadata metadata = dp.GetMetadata(this);
            FrameworkPropertyMetadata frameworkMetadata = metadata as FrameworkPropertyMetadata;
            object result = metadata.DefaultValue;

            if (frameworkMetadata != null && frameworkMetadata.Inherits)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(this);

                if (parent != null)
                {
                    result = parent.GetValue(dp);
                }
            }

            return result;
        }

        private void InheritedValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.IsRegistered(this.GetType(), e.Property) && !this.properties.ContainsKey(e.Property))
            {
                this.OnPropertyChanged(e);
            }
            else
            {
                foreach (DependencyObject child in VisualTreeHelper.GetChildren(this))
                {
                    child.InheritedValueChanged(e);
                }
            }
        }
    }
}