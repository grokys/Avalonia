// -----------------------------------------------------------------------
// <copyright file="TextComposition.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using Avalonia.Threading;

    public class TextComposition : DispatcherObject
    {
        public TextComposition(InputManager inputManager, IInputElement source, string resultText)
        {
            this.Text = resultText;
        }

        public string Text { get; set; }
    }
}
