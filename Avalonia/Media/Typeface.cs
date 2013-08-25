// -----------------------------------------------------------------------
// <copyright file="Typeface.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    public class Typeface
    {
        public Typeface(string typefaceName)
        {
            this.FontFamily = new FontFamily(typefaceName);
            this.Style = FontStyles.Normal;
        }

        public Typeface(FontFamily fontFamily, FontStyle style, FontWeight weight, FontStretch stretch)
        {
            this.FontFamily = fontFamily;
            this.Style = style;
            this.Weight = weight;
            this.Stretch = stretch;
        }

        public FontFamily FontFamily { get; private set; }
        public FontStretch Stretch { get; private set; }
        public FontStyle Style { get; private set; }
        public FontWeight Weight { get; private set; }
    }
}
