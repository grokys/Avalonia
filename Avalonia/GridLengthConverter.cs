// -----------------------------------------------------------------------
// <copyright file="GridLengthConverter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class GridLengthConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string s = value.ToString().Trim();
            GridUnitType type = GridUnitType.Pixel;
            double val = 1;
            double scale = 1;
            bool canBeEmptyString = false;

            if (s == "Auto")
            {
                type = GridUnitType.Auto;
            }
            else if (s.EndsWith("*"))
            {
                type = GridUnitType.Star;
                s = s.Substring(0, s.Length - 1);
                canBeEmptyString = true;
            }
            else if (s.EndsWith("px"))
            {
                s = s.Substring(0, s.Length - 2);
            }
            else if (s.EndsWith("in"))
            {
                s = s.Substring(0, s.Length - 2);
                scale = 96;
            }
            else if (s.EndsWith("cm"))
            {
                s = s.Substring(0, s.Length - 2);
                scale = 96.0 / 2.54;
            }
            else if (s.EndsWith("pt"))
            {
                s = s.Substring(0, s.Length - 2);
                scale = 96.0 / 72.0;
            }

            if (type != GridUnitType.Auto)
            {
                s = s.Trim();

                if (s.Length > 0)
                {
                    val = double.Parse(s);
                }
                else if (!canBeEmptyString)
                {
                    throw new NotSupportedException("Could not convert grid length value.");
                }
            }

            return new GridLength(val * scale, type);
        }
    }
}
