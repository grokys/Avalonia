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
    using System.Windows.Markup;
    using System.Xaml;
    using Avalonia.Controls;

    [XamlSetTypeConverter("ReceiveTypeConverter")]
    public class Setter : SetterBase, ISupportInitialize
    {
        private Dictionary<FrameworkElement, object> oldValues = new Dictionary<FrameworkElement, object>();

        private string propertyName;

        private IServiceProvider serviceProvider;

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

        public static void ReceiveTypeConverter(object targetObject, XamlSetTypeConverterEventArgs eventArgs)
        {
            // The DependencyProperty refered to by Property may depend on the value of TargetName,
            // but we don't know that yet, so defer loading Property until EndInit().
            if (eventArgs.Member.Name == "Property")
            {
                Setter setter = (Setter)targetObject;
                setter.propertyName = (string)eventArgs.Value;
                setter.serviceProvider = eventArgs.ServiceProvider;
                eventArgs.Handled = true;
            }
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            if (this.propertyName != null)
            {
                if (this.TargetName == null)
                {
                    this.Property = DependencyPropertyConverter.Resolve(this.serviceProvider, this.propertyName);
                }
                else
                {
                    // TargetName is specified so we need to look in the containing template for the named element
                    IAmbientProvider ambient = (IAmbientProvider)this.serviceProvider.GetService(typeof(IAmbientProvider));
                    IXamlSchemaContextProvider schema = (IXamlSchemaContextProvider)this.serviceProvider.GetService(typeof(IXamlSchemaContextProvider));

                    // Look up the FrameworkTemplate.Template property in the xaml schema.
                    XamlType frameworkTemplateType = schema.SchemaContext.GetXamlType(typeof(FrameworkTemplate));
                    XamlMember templateProperty = frameworkTemplateType.GetMember("Template");

                    // Get the value of the first ambient FrameworkTemplate.Template property.
                    TemplateContent templateContent = (TemplateContent)ambient.GetFirstAmbientValue(new[] { frameworkTemplateType }, templateProperty).Value;

                    // Look in the template for the type of TargetName.
                    Type targetType = templateContent.GetTypeForName(this.TargetName);

                    // Finally, find the dependency property on the type.
                    this.Property = DependencyObject.PropertyFromName(targetType, this.propertyName);
                }
            }
        }

        internal override void Attach(FrameworkElement frameworkElement)
        {
            object oldValue = DependencyProperty.UnsetValue;

            if (!frameworkElement.IsUnset(this.Property))
            {
                oldValue = frameworkElement.GetValue(this.Property);
            }

            this.oldValues.Add(frameworkElement, oldValue);

            frameworkElement.SetValue(this.Property, this.ConvertValue(this.Value));
        }

        internal override void Detach(FrameworkElement frameworkElement)
        {
            frameworkElement.SetValue(this.Property, this.oldValues[frameworkElement]);
            this.oldValues.Remove(frameworkElement);
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
                else
                {
                    return Convert.ChangeType(value, this.Property.PropertyType);
                }

                throw new NotSupportedException(string.Format(
                    "Could not convert the value '{0}' to '{1}",
                    value,
                    this.Property.PropertyType.Name));
            }
        }
    }
}
