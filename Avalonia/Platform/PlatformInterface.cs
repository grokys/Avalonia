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
        /// Creates a new platform-specific <see cref="PresentationSource"/>.
        /// </summary>
        /// <returns>
        /// The newly created presentation source.
        /// </returns>
        public abstract PlatformPresentationSource CreatePresentationSource();

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
