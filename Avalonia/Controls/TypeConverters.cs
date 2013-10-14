using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Controls
{
    /// <summary>
    /// Common TypeConverter functionality.
    /// </summary> 
    internal static partial class TypeConverters
    {
        /// <summary> 
        /// Returns a value that indicates whether this converter can convert an
        /// object of the given type to an instance of the expected type.
        /// </summary> 
        /// <typeparam name="T">Expected type of the converter.</typeparam>
        /// <param name="sourceType">
        /// The type of the source that is being evaluated for conversion. 
        /// </param> 
        /// <returns>
        /// true if the converter can convert the provided type to an instance 
        /// of a String or the expected type; otherwise, false.
        /// </returns>
        internal static bool CanConvertFrom<T>(Type sourceType)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException("sourceType");
            }
            return (sourceType == typeof(string)) || typeof(T).IsAssignableFrom(sourceType);
        }

        /// <summary> 
        /// Attempts to convert a specified object to an instance of the
        /// expected type.
        /// </summary> 
        /// <typeparam name="T">Expected type of the converter.</typeparam> 
        /// <param name="converter">TypeConverter instance.</param>
        /// <param name="value">The object being converted.</param> 
        /// <returns>
        /// The instance of the expected type created from the converted object.
        /// </returns> 
        internal static object ConvertFrom<T>(TypeConverter converter, object value)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }

            string text = value as string;
            if (text != null || value == null)
            {
                // Convert text using ConvertFromString
                return converter.ConvertFromString(text);
            }
            else if (value is T)
            {
                // There's nothing to convert if it's already the correct type
                return value;
            }
            else
            {
                // Otherwise throw an error 
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Attempts to convert the specified text to an instance of the 
        /// expected type. 
        /// </summary>
        /// <typeparam name="T">Expected type of the converter.</typeparam> 
        /// <param name="text">The text being converted.</param>
        /// <param name="knownValues">
        /// Dictionary mapping known names to values. 
        /// </param>
        /// <returns>
        /// The instance of the expected type created from the converted text. 
        /// </returns> 
        internal static object ConvertFromString<T>(string text, IDictionary<string, T> knownValues)
        {
            if (knownValues == null)
            {
                throw new ArgumentNullException("knownValues");
            }

            if (text == null)
            {
                throw new FormatException();
            }

            T value;
            if (!knownValues.TryGetValue(text, out value))
            {
                throw new FormatException();
            }
            return value;
        }
    } 

}
