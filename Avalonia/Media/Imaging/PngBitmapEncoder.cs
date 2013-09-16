// -----------------------------------------------------------------------
// <copyright file="PngBitmapEncoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using Avalonia.Platform;
    using Avalonia.Threading;

    public sealed class PngBitmapEncoder : BitmapEncoder
    {
        public PngBitmapEncoder()
            : base(PlatformInterface.Instance.CreateBitmapEncoder(BitmapContainerFormat.Png))
        {
        }
    }
}
