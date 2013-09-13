// -----------------------------------------------------------------------
// <copyright file="StreamGeometry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using Avalonia.Platform;

    public sealed class StreamGeometry : Geometry
    {
        public StreamGeometry()
        {
            this.PlatformImpl = PlatformInterface.Instance.CreateStreamGeometry();
        }

        public IPlatformStreamGeometry PlatformImpl
        {
            get;
            private set;
        }

        public StreamGeometryContext Open()
        {
            return this.PlatformImpl.Open();
        }
    }
}
