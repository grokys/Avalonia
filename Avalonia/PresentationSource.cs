// -----------------------------------------------------------------------
// <copyright file="PresentationSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using Avalonia.Media;
    using Avalonia.Threading;

    public abstract class PresentationSource : DispatcherObject
    {
        public abstract Visual RootVisual { get; set; }
    }
}
