// -----------------------------------------------------------------------
// <copyright file="ImageSourceConverter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using Avalonia.Media.Imaging;

    public class ImageSourceConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            BitmapDecoder decoder = BitmapDecoder.Create(
                new Uri((string)value, UriKind.Relative),
                BitmapCreateOptions.None,
                BitmapCacheOption.None);
            return decoder.Frames[0];
        }
    }
}
