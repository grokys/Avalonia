// -----------------------------------------------------------------------
// <copyright file="BitmapEncoder.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media.Imaging
{
    using System.Collections.Generic;
    using System.IO;
    using Avalonia.Platform;
    using Avalonia.Threading;

    public abstract class BitmapEncoder : DispatcherObject
    {
        private IPlatformBitmapEncoder platformImpl;
        
        protected BitmapEncoder(IPlatformBitmapEncoder platformImpl)
        {
            this.platformImpl = platformImpl;
            this.Frames = new List<BitmapFrame>();
        }

        public virtual IList<BitmapFrame> Frames 
        { 
            get; 
            set;
        }

        public virtual void Save(Stream stream)
        {
            this.platformImpl.Save(this.Frames, stream);
        }
    }
}
