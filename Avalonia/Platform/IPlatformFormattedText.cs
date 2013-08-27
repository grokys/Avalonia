// -----------------------------------------------------------------------
// <copyright file="IPlatformFormattedText.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;

    public interface IPlatformFormattedText
    {
        double Width { get; }

        double Height { get; }

        int GetCaretIndex(Point p);

        Point GetCaretPosition(int caretIndex);
    }
}
