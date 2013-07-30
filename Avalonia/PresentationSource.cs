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
