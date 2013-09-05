// -----------------------------------------------------------------------
// <copyright file="StreamResourceInfo.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Resources
{
    using System.IO;

    public class StreamResourceInfo
    {
        public StreamResourceInfo()
        {
        }

        public StreamResourceInfo(Stream stream, string contentType)
        {
            this.ContentType = contentType;
            this.Stream = stream;
        }

        public string ContentType { get; private set; }

        public Stream Stream { get; private set; }
    }
}
