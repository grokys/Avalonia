// -----------------------------------------------------------------------
// <copyright file="JpegBitmapDecoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using System;
    using System.IO;
    using Avalonia.Platform;

    public sealed class JpegBitmapDecoder : BitmapDecoder
    {
        public JpegBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
            : base(null)
        {
            throw new NotImplementedException();
        }
        
        public JpegBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
            : base(null)
        {
            throw new NotImplementedException();
        }

        internal JpegBitmapDecoder(IPlatformBitmapDecoder platformImpl)
            : base(platformImpl)
        {
        }
    }
}
