// -----------------------------------------------------------------------
// <copyright file="BrushConverter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class BrushConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            PropertyInfo p = typeof(Colors).GetProperty(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            if (p != null)
            {
                return new SolidColorBrush((Color)p.GetValue(null));
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
