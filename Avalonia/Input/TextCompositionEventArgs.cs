// -----------------------------------------------------------------------
// <copyright file="TextCompositionEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    public delegate void TextCompositionEventHandler(object sender, TextCompositionEventArgs e);

    public class TextCompositionEventArgs : InputEventArgs
    {
        public TextCompositionEventArgs(InputDevice inputDevice, TextComposition composition)
            : base(inputDevice, 0)
        {
            this.Text = composition.Text;
        }

        public string Text { get; private set; }
    }
}
