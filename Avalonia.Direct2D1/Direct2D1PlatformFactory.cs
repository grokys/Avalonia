using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Direct2D1.Media;
using Avalonia.Platform;

namespace Avalonia.Direct2D1
{
    public class Direct2D1PlatformFactory : PlatformFactory
    {
        public Direct2D1PlatformFactory()
        {
            this.Dispatcher = new WindowMessageDispatcher();
            this.DirectWriteFactory = new SharpDX.DirectWrite.Factory();
        }

        public SharpDX.DirectWrite.Factory DirectWriteFactory
        {
            get;
            private set;
        }

        public override IPlatformDispatcher Dispatcher 
        { 
            get; 
            protected set; 
        }

        public override PlatformPresentationSource CreatePresentationSource()
        {
            return new HwndSource();
        }

        public override IPlatformFormattedText CreateFormattedText(string text)
        {
            return new Direct2D1FormattedText(text);
        }
    }
}
