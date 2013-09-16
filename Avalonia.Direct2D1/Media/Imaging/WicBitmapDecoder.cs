// -----------------------------------------------------------------------
// <copyright file="WicBitmapDecoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Avalonia.Platform;
    using SharpDX.WIC;

    public class WicBitmapDecoder : IPlatformBitmapDecoder
    {
        private static readonly Guid[] FormatGuids = new[]
        {
            ContainerFormatGuids.Bmp,
            ContainerFormatGuids.Png,
            ContainerFormatGuids.Ico,
            ContainerFormatGuids.Jpeg,
            ContainerFormatGuids.Tiff,
            ContainerFormatGuids.Gif,
            ContainerFormatGuids.Wmp,
        };

        private ImagingFactory factory;

        private BitmapDecoder wicImpl;

        public WicBitmapDecoder(ImagingFactory factory, BitmapContainerFormat format)
        {
            this.factory = factory;
            this.wicImpl = new BitmapDecoder(factory, FormatGuids[(int)format]);
        }

        public WicBitmapDecoder(ImagingFactory factory, Stream stream, DecodeOptions metadataOptions)
        {
            this.factory = factory;
            this.wicImpl = new BitmapDecoder(factory, stream, metadataOptions);
        }

        public BitmapContainerFormat ContainerFormat
        {
            get 
            { 
                return (BitmapContainerFormat)Array.IndexOf(
                    FormatGuids, 
                    this.wicImpl.ContainerFormat);
            }
        }

        public IEnumerable<IPlatformBitmapSource> Frames
        {
            get
            {
                int count = this.wicImpl.FrameCount;

                for (int i = 0; i < count; ++i)
                {
                    yield return new WicBitmapSource(this.factory, this.wicImpl.GetFrame(i));
                }
            }
        }
    }
}
