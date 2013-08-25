// -----------------------------------------------------------------------
// <copyright file="Direct2D1PlatformFactory.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1
{
    using Avalonia.Direct2D1.Input;
using Avalonia.Direct2D1.Media;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Platform;

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

        public override MouseDevice MouseDevice
        {
            get { return Win32MouseDevice.Instance; }
        }

        public override PlatformPresentationSource CreatePresentationSource()
        {
            return new HwndSource();
        }

        public override IPlatformFormattedText CreateFormattedText(
            string text, 
            Typeface typeface, 
            double fontSize)
        {
            return new Direct2D1FormattedText(text, typeface, fontSize);
        }
    }
}
