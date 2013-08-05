using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using SharpDX.DirectWrite;

namespace Avalonia.Direct2D1.Media
{
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
