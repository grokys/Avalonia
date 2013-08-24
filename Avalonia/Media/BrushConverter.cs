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
            string s = (string)value;

            if (s.StartsWith("#"))
            {
                s = s.Substring(1);

                if (s.Length == 6)
                {
                    s = "ff" + s;
                }

                if (s.Length != 8)
                {
                    throw new NotSupportedException("Invalid color string.");
                }

                return new SolidColorBrush(Color.FromUInt32(uint.Parse(s, NumberStyles.HexNumber)));
            }
            else
            {
                PropertyInfo p = typeof(Colors).GetProperty(s, BindingFlags.Public | BindingFlags.Static);

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
}
