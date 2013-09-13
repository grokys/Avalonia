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
        private IPlatformStreamGeometry impl;

        public StreamGeometry()
        {
            this.impl = PlatformInterface.Instance.CreateStreamGeometry();
        }

        public StreamGeometryContext Open()
        {
            return this.impl.Open();
        }
    }
}
