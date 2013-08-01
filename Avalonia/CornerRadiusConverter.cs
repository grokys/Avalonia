// -----------------------------------------------------------------------
// <copyright file="CornerRadiusConverter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CornerRadiusConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            char[] separators = new char[] { ' ', ',' };
            string[] components = value.ToString().Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (components.Length == 1)
            {
                return new CornerRadius(double.Parse(components[0]));
            }
            else
            {
                if (components.Length == 4)
                {
                    return new CornerRadius(
                        double.Parse(components[0]),
                        double.Parse(components[1]),
                        double.Parse(components[2]),
                        double.Parse(components[3]));
                }
                else
                {
                    throw new NotSupportedException("Value is not valid: must contain one or four delineated lengths.");
                }
            }
        }
    }
}
