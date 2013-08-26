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

        internal static TextComposition FromKey(InputManager inputManager, IInputElement inputElement, Key key)
        {
            string text = string.Empty;

            if (key >= Key.D0 && key <= Key.D9)
            {
                text = ((char)(key - Key.D0)).ToString();
            }
            else if (key >= Key.A && key <= Key.Z)
            {
                text = key.ToString();
            }
            else
            {
                // TODO: Handle other keys here.
            }

            return new TextComposition(inputManager, inputElement, text);
        }
    }
}
