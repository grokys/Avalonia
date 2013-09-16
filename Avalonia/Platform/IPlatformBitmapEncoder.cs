// -----------------------------------------------------------------------
// <copyright file="IPlatformBitmapEncoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System.Collections.Generic;
    using System.IO;
    using Avalonia.Media.Imaging;

    public interface IPlatformBitmapEncoder
    {
        void Save(IEnumerable<BitmapFrame> frames, Stream stream);
    }
}
