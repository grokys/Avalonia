// -----------------------------------------------------------------------
// <copyright file="Direct2D1FormattedText.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media
{
    using Avalonia.Media;
    using Avalonia.Platform;
    using SharpDX.DirectWrite;

    public class Direct2D1FormattedText : IPlatformFormattedText
    {
        public Direct2D1FormattedText(string text, Typeface typeface, double fontSize)
        {
            Factory factory = ((Direct2D1PlatformInterface)PlatformInterface.Instance).DirectWriteFactory;

            TextFormat format = new TextFormat(
                factory, 
                typeface.FontFamily.Source, 
                (float)fontSize);

            this.DirectWriteTextLayout = new TextLayout(
                factory,
                text ?? string.Empty,
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
            get { return this.DirectWriteTextLayout.Metrics.WidthIncludingTrailingWhitespace; }
        }

        public double Height
        {
            get { return this.DirectWriteTextLayout.Metrics.Height; }
        }

        public int GetCaretIndex(Point p)
        {
            SharpDX.Bool isTrailingHit;
            SharpDX.Bool isInside;

            HitTestMetrics result = this.DirectWriteTextLayout.HitTestPoint(
                (float)p.X,
                (float)p.Y,
                out isTrailingHit,
                out isInside);

            return result.TextPosition + (isTrailingHit ? 1 : 0);
        }

        public Point GetCaretPosition(int caretIndex)
        {
            float x;
            float y;
            this.DirectWriteTextLayout.HitTestTextPosition(caretIndex, false, out x, out y);
            return new Point(x, y);
        }
    }
}
