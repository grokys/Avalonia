// -----------------------------------------------------------------------
// <copyright file="BitmapDecoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using Avalonia.Platform;
    using Avalonia.Threading;

    public enum BitmapCacheOption
    {
        OnDemand = 0,
        Default = 0,
        OnLoad = 1,
        None = 2,
    }

    [Flags]
    public enum BitmapCreateOptions
    {
        None = 0,
        PreservePixelFormat = 1,
        DelayCreation = 2,
        IgnoreColorProfile = 4,
        IgnoreImageCache = 8,
    }

    public abstract class BitmapDecoder : DispatcherObject
    {
        private IPlatformBitmapDecoder impl;
        private ReadOnlyCollection<BitmapFrame> frames;

        internal BitmapDecoder(IPlatformBitmapDecoder platformImpl)
        {
            this.impl = platformImpl;
        }

        public virtual ReadOnlyCollection<BitmapFrame> Frames 
        { 
            get
            {
                if (this.frames == null)
                {
                    var e = this.impl.Frames.Select(x => new BitmapFrame(x));
                    this.frames = new ReadOnlyCollection<BitmapFrame>(e.ToList());
                }

                return this.frames;
            }
        }

        public static BitmapDecoder Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
        {
            Stream stream;

            if (!bitmapUri.IsAbsoluteUri)
            {
                stream = Application.GetResourceStream(bitmapUri).Stream;
            }
            else if (bitmapUri.IsFile)
            {
                stream = new FileStream(
                    Uri.UnescapeDataString(bitmapUri.AbsolutePath), 
                    FileMode.Open, 
                    FileAccess.Read);
            }
            else
            {
                throw new NotSupportedException("URI not yet supported.");
            }

            using (stream)
            {
                IPlatformBitmapDecoder platformImpl = PlatformInterface.Instance.CreateBitmapDecoder(
                    stream,
                    createOptions,
                    cacheOption);

                switch (platformImpl.ContainerFormat)
                {
                    case BitmapContainerFormat.Jpeg:
                        return new JpegBitmapDecoder(platformImpl);
                    case BitmapContainerFormat.Png:
                        return new PngBitmapDecoder(platformImpl);
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
