// -----------------------------------------------------------------------
// <copyright file="Direct2D1FormattedText.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using Avalonia.Platform;
    using SharpDX.DirectWrite;

    public class Direct2D1FormattedText : IPlatformFormattedText
    {
        public Direct2D1FormattedText(string text)
        {
            Factory factory = ((Direct2D1PlatformFactory)PlatformFactory.Instance).DirectWriteFactory;

            TextFormat format = new TextFormat(factory, "Segoe UI", 16);

            this.DirectWriteTextLayout = new TextLayout(
                factory,
                text,
                format,
                float.MaxValue,
                float.MaxValue);
        }

        public TextLayout DirectWriteTextLayout
        {
            get;
            private set;
        }

        public double Width
        {
            get { return this.DirectWriteTextLayout.Metrics.Width; }
        }

        public double Height
        {
            get { return this.DirectWriteTextLayout.Metrics.Height; }
        }
    }
}
