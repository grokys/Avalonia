// -----------------------------------------------------------------------
// <copyright file="PngBitmapDecoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using System;
    using System.IO;
    using Avalonia.Platform;

    public sealed class PngBitmapDecoder : BitmapDecoder
    {
        public PngBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
            : base(null)
        {
            throw new NotImplementedException();
        }
        
        public PngBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
            : base(null)
        {
            throw new NotImplementedException();
        }

        internal PngBitmapDecoder(IPlatformBitmapDecoder platformImpl)
            : base(platformImpl)
        {
        }
    }
}
