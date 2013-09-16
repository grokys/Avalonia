// -----------------------------------------------------------------------
// <copyright file="WicBitmapEncoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Direct2D1.Media.Imaging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Avalonia.Media.Imaging;
    using Avalonia.Platform;
    using SharpDX.WIC;

    public class WicBitmapEncoder : IPlatformBitmapEncoder
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

        private SharpDX.WIC.BitmapEncoder wicImpl;

        public WicBitmapEncoder(ImagingFactory factory, BitmapContainerFormat format)
        {
            this.wicImpl = new SharpDX.WIC.BitmapEncoder(factory, FormatGuids[(int)format]);
        }

        public void Save(IEnumerable<BitmapFrame> frames, Stream stream)
        {
            this.wicImpl.Initialize(stream);

            foreach (WicBitmapSource source in frames.Select(x => x.PlatformImpl))
            {
                BitmapFrameEncode frame = new BitmapFrameEncode(this.wicImpl);
                frame.Initialize();
                frame.WriteSource(source.WicImpl);
                frame.Commit();
            }

            this.wicImpl.Commit();
        }
    }
}
