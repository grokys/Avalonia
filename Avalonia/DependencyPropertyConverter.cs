// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyConverter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Xaml;

    public class DependencyPropertyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            IAmbientProvider ambient = (IAmbientProvider)context.GetService(typeof(IAmbientProvider));
            IXamlSchemaContextProvider schema = (IXamlSchemaContextProvider)context.GetService(typeof(IXamlSchemaContextProvider));

            // Get the XamlType which represents the <Style> element.
            XamlType styleType = schema.SchemaContext.GetXamlType(typeof(Style));

            // The first ambient value should be the enclosing <Style>.
            Style style = (Style)ambient.GetFirstAmbientValue(styleType);

            // Get the target type for the style from this.
            Type targetType = style.TargetType;

            // And get the dependency property.
            return DependencyObject.FromName(targetType, (string)value);
        }
    }
}
