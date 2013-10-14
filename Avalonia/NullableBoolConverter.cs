using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Avalonia
{
    public sealed class NullableBoolConverter : TypeConverter
    {
        /// <summary>
        /// Initializes a new instance of the NullableBoolConverter class. 
        /// </summary>
        public NullableBoolConverter()
        {
        }

        /// <summary> 
        /// Returns whether this converter can convert an object of the given 
        /// type to the type of this converter.
        /// </summary> 
        /// <param name="sourceType">
        /// A type that represents the type that you want to convert from.
        /// </param> 
        /// <returns>
        /// true if sourceType is a String type or a Boolean? type that can be
        /// assigned from sourceType; otherwise, false. 
        /// </returns> 
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return TypeConverters.CanConvertFrom<bool?>(sourceType) ||
                (sourceType == typeof(bool));
        }

        /// <summary>
        /// Converts the specified object to a bool?. 
        /// </summary> 
        /// <param name="value">Object to convert into a bool?.</param>
        /// <returns>A bool? that represents the converted text.</returns> 
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text;

            if (value is bool)
            {
                return (bool?)value;
            }
            text = value as string;
            if (text != null || value == null)
            {
                return (!string.IsNullOrEmpty(text)) ?
                    (bool?)bool.Parse(text) :
                    null;
            }

            return TypeConverters.ConvertFrom<bool?>(this, value);
        }
    } 

}
