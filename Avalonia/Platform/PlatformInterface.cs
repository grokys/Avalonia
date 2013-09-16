// -----------------------------------------------------------------------
// <copyright file="PlatformInterface.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using System.IO;
    using System.Reflection;
    using Avalonia.Input;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;

    /// <summary>
    /// Provides platform-specific implementations.
    /// </summary>
    public abstract class PlatformInterface
    {
        /// <summary>
        /// The platform factory instance.
        /// </summary>
        private static PlatformInterface instance;

        /// <summary>
        /// Gets or sets the application-wide instance of the <see cref="PlatformInterface"/>.
        /// </summary>
        public static PlatformInterface Instance
        {
            get
            {
                if (instance == null)
                {
#if WINDOWS
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    Assembly platform = Assembly.LoadFile(Path.Combine(path, "Avalonia.Direct2D1.dll"));
                    Type factoryType = platform.GetType("Avalonia.Direct2D1.Direct2D1PlatformInterface");
                    instance = (PlatformInterface)Activator.CreateInstance(factoryType);
#else
                    throw new NotSupportedException("This platform is not supported.");
#endif
                }

                return instance;
            }
            
            set
            {
                instance = value;
            }
        }

        public abstract TimeSpan CaretBlinkTime
        {
            get;
        }

        /// <summary>
        /// Gets the platform-specific dispatcher implementation.
        /// </summary>
        public abstract IPlatformDispatcher Dispatcher
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the platform-specific keyboard device.
        /// </summary>
        public abstract KeyboardDevice KeyboardDevice
        {
            get;
        }

        /// <summary>
        /// Gets the platform-specific mouse device.
        /// </summary>
        public abstract MouseDevice MouseDevice
        {
            get;
        }

        /// <summary>
        /// Creates a new platform-specific bitmap decoder for the specified container format.
        /// </summary>
        /// <param name="format">The bitmap container format.</param>
        /// <returns>An <see cref="IPlatformBitmapDecoder"/>.</returns>
        public abstract IPlatformBitmapDecoder CreateBitmapDecoder(BitmapContainerFormat format);

        /// <summary>
        /// Creates a new platform-specific bitmap decoder to decode a stream.
        /// </summary>
        /// <param name="stream">The stream containing the bitmap.</param>
        /// <param name="cacheOption">The cache option.</param>
        /// <returns>An <see cref="IPlatformBitmapDecoder"/>.</returns>
        public abstract IPlatformBitmapDecoder CreateBitmapDecoder(
            Stream stream, 
            BitmapCacheOption cacheOption);

        /// <summary>
        /// Creates a new platform-specific bitmap encoder for the specified container format.
        /// </summary>
        /// <param name="format">The bitmap container format.</param>
        /// <returns>An <see cref="IPlatformBitmapEncoder"/>.</returns>
        public abstract IPlatformBitmapEncoder CreateBitmapEncoder(BitmapContainerFormat format);

        /// <summary>
        /// Creates a new platform-specific render target bitmap.
        /// </summary>
        /// <param name="pixelWidth">The width in pixels.</param>
        /// <param name="pixelHeight">The height in pixels.</param>
        /// <param name="dpiX">The horizontal resolution.</param>
        /// <param name="dpiY">The vertical resolution.</param>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <returns>An <see cref="IPlatformRenderTargetBitmap"/>.</returns>
        public abstract IPlatformRenderTargetBitmap CreateRenderTargetBitmap(
            int pixelWidth,
            int pixelHeight,
            double dpiX,
            double dpiY,
            PixelFormat pixelFormat);

        /// <summary>
        /// Create a new platform-specific <see cref="FormattedText"/>.
        /// </summary>
        /// <param name="textToFormat">The text.</param>
        /// <param name="typeface">The typeface.</param>
        /// <param name="fontSize">The font size.</param>
        /// <returns>The formatted text object.</returns>
        public abstract IPlatformFormattedText CreateFormattedText(
            string textToFormat,
            Typeface typeface,
            double fontSize);

        /// <summary>
        /// Creates a new platform-specific <see cref="PresentationSource"/>.
        /// </summary>
        /// <returns>
        /// The newly created presentation source.
        /// </returns>
        public abstract PlatformPresentationSource CreatePresentationSource();

        /// <summary>
        /// Creates a new platform-specific stream geometry;
        /// </summary>
        /// <returns>
        /// An <see cref="IPlatformStreamGeometry"/>.
        /// </returns>
        public abstract IPlatformStreamGeometry CreateStreamGeometry();

        /// <summary>
        /// Starts a new timer.
        /// </summary>
        /// <param name="interval">The timer interval.</param>
        /// <param name="callback">The timer callback.</param>
        /// <returns>A timer handle.</returns>
        public abstract object StartTimer(TimeSpan interval, Action callback);

        /// <summary>
        /// Kills a running timer.
        /// </summary>
        /// <param name="handle">The timer handle.</param>
        public abstract void KillTimer(object handle);
    }
}
