// -----------------------------------------------------------------------
// <copyright file="ITopLevelWindow.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using Avalonia.Platform;

    internal interface ITopLevelWindow
    {
        PlatformPresentationSource PresentationSource { get; }

        void DoLayoutPass();
    }
}
