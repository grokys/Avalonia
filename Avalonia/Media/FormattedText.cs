// -----------------------------------------------------------------------
// <copyright file="FormattedText.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Platform;

    public class FormattedText
    {
        public FormattedText(
            string textToFormat,
            CultureInfo culture,
            FlowDirection flowDirection,
            Typeface typeface,
            double emSize,
            Brush foreground)
        {
            this.Foreground = foreground;
            this.PlatformImpl = PlatformInterface.Instance.CreateFormattedText(
                textToFormat, 
                typeface, 
                emSize);
            this.Text = textToFormat;
        }

        public Brush Foreground
        {
            get;
            private set;
        }

        public string Text 
        { 
            get; 
            private set; 
        }

        public double Width
        {
            get
            {
                return this.PlatformImpl.Width;
            }
        }

        public double Height
        {
            get
            {
                return this.PlatformImpl.Height;
            }
        }

        [AvaloniaSpecific]
        public IPlatformFormattedText PlatformImpl
        {
            get;
            private set;
        }

        internal int GetCaretIndex(Point p)
        {
            return this.PlatformImpl.GetCaretIndex(p);
        }

        internal Point GetCaretPosition(int caretIndex)
        {
            return this.PlatformImpl.GetCaretPosition(caretIndex);
        }
    }
}
