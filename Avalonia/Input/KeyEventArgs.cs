// -----------------------------------------------------------------------
// <copyright file="KeyEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    public class KeyEventArgs : KeyboardEventArgs
    {
        public KeyEventArgs(
            KeyboardDevice keyboard, 
            PresentationSource inputSource, 
            int timestamp, 
            Key key)
            : base(keyboard, timestamp)
        {
            this.Key = key;
        }

        public Key Key
        {
            get;
            private set;
        }
    }
}
