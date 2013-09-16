// -----------------------------------------------------------------------
// <copyright file="ITopLevelWindow.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

using Avalonia.Platform;
namespace Avalonia
{
    internal interface ITopLevelWindow
    {
        PlatformPresentationSource PresentationSource { get; }

        void DoLayoutPass();
    }
}
