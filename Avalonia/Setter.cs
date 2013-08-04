// -----------------------------------------------------------------------
// <copyright file="Setter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class Setter : SetterBase
    {
        private Dictionary<FrameworkElement, object> oldValues = new Dictionary<FrameworkElement, object>();

        public Setter()
        {
        }

        public Setter(DependencyProperty property, object value)
        {
            this.Property = property;
            this.Value = value;
        }

        public Setter(DependencyProperty property, object value, string targetName)
        {
            this.Property = property;
            this.Value = value;
            this.TargetName = targetName;
        }

        public DependencyProperty Property
        {
            get;
            set;
        }

        public string TargetName
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        internal void Attach(FrameworkElement frameworkElement)
        {
            object oldValue = DependencyProperty.UnsetValue;

            if (!frameworkElement.IsUnset(this.Property))
            {
                oldValue = frameworkElement.GetValue(this.Property);
            }

            this.oldValues.Add(frameworkElement, oldValue);

            frameworkElement.SetValue(this.Property, this.ConvertValue(this.Value));
        }

        internal void Detach(FrameworkElement frameworkElement)
        {
            frameworkElement.SetValue(this.Property, this.oldValues[frameworkElement]);
        }

        private object ConvertValue(object value)
        {
            if (value.GetType() == this.Property.PropertyType)
            {
                return value;
            }
            else if (value is string && this.Property.PropertyType.IsEnum)
            {
                return Enum.Parse(this.Property.PropertyType, (string)value);
            }
            else
            {
                TypeConverterAttribute attribute =
                    this.Property.PropertyType.GetCustomAttributes(typeof(TypeConverterAttribute), true)
                        .Cast<TypeConverterAttribute>()
                        .FirstOrDefault();

                if (attribute != null)
                {
                    Type converterType = Type.GetType(attribute.ConverterTypeName);
                    TypeConverter converter = (TypeConverter)Activator.CreateInstance(converterType);

                    if (converter.CanConvertFrom(value.GetType()))
                    {
                        return converter.ConvertFrom(value);
                    }
                }

                throw new NotSupportedException(string.Format(
                    "Could not convert the value '{0}' to '{1}",
                    value,
                    this.Property.PropertyType.Name));
            }
        }
    }
}
