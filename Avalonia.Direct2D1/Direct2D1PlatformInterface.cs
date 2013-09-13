// -----------------------------------------------------------------------
// <copyright file="Direct2D1PlatformInterface.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Avalonia.Direct2D1.Input;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Direct2D1.Media;
    using Avalonia.Direct2D1.Media.Imaging;
    using Avalonia.Input;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;
    using Avalonia.Platform;
    using SharpDX.Direct2D1;
    using SharpDX.WIC;

    public class Direct2D1PlatformInterface : PlatformInterface
    {
        private Dictionary<IntPtr, Action> timerCallbacks = new Dictionary<IntPtr, Action>();

        private ImagingFactory wicFactory = new ImagingFactory();

        public Direct2D1PlatformInterface()
        {
            this.Dispatcher = new WindowMessageDispatcher();
            this.Direct2DFactory = new Factory();
            this.DirectWriteFactory = new SharpDX.DirectWrite.Factory();
        }

        public override TimeSpan CaretBlinkTime
        {
            get 
            {
                uint t = UnmanagedMethods.GetCaretBlinkTime();
                return new TimeSpan(0, 0, 0, 0, (int)t);
            }
        }

        public Factory Direct2DFactory
        {
            get;
            private set;
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

        public override KeyboardDevice KeyboardDevice
        {
            get { return Win32KeyboardDevice.Instance; }
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

        public override IPlatformBitmapDecoder CreateBitmapDecoder(BitmapContainerFormat format)
        {
            return new WicBitmapDecoder(this.wicFactory, format);
        }

        public override IPlatformBitmapDecoder CreateBitmapDecoder(
            Stream stream,
            BitmapCreateOptions createOptions, 
            BitmapCacheOption cacheOption)
        {
            return new WicBitmapDecoder(this.wicFactory, stream);
        }

        public override IPlatformStreamGeometry CreateStreamGeometry()
        {
            return new Direct2D1StreamGeometry(new PathGeometry(this.Direct2DFactory));
        }

        public override object StartTimer(TimeSpan interval, Action callback)
        {
            IntPtr handle = UnmanagedMethods.SetTimer(
                IntPtr.Zero, 
                IntPtr.Zero, 
                (uint)interval.TotalMilliseconds, 
                (hWnd, uMsg, nIDEvent, dwTime) => callback());
            return handle;
        }

        public override void KillTimer(object handle)
        {
            this.timerCallbacks.Remove((IntPtr)handle);
            UnmanagedMethods.KillTimer(IntPtr.Zero, (IntPtr)handle);
        }

        internal void TriggerTimer(IntPtr handle)
        {
            this.timerCallbacks[handle]();
        }
    }
}
