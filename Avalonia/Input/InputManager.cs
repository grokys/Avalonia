// -----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Media;
    using Avalonia.Platform;
    using Avalonia.Threading;

    public sealed class InputManager : DispatcherObject
    {
        private Stack<InputEventArgs> stack = new Stack<InputEventArgs>();

        private bool processing = false;

        static InputManager()
        {
            Current = new InputManager();
        }

        public event PreProcessInputEventHandler PreProcessInput;

        public static InputManager Current
        {
            get;
            private set;
        }

        public void ProcessInput(InputEventArgs input)
        {
            this.stack.Push(input);
            this.ProcessStack();
        }

        private void ProcessStack()
        {
            if (!processing)
            {
                processing = true;

                while (stack.Count > 0)
                {
                    InputEventArgs input = stack.Pop();
                    PreProcessInputEventArgs e = new PreProcessInputEventArgs(input);

                    input.OriginalSource = input.Device.Target;

                    if (this.PreProcessInput != null)
                    {
                        foreach (var handler in this.PreProcessInput.GetInvocationList().Reverse())
                        {
                            handler.DynamicInvoke(this, e);
                        }
                    }

                    if (!e.Canceled)
                    {
                        UIElement uiElement = input.OriginalSource as UIElement;

                        if (uiElement != null)
                        {
                            uiElement.RaiseEvent(input);
                        }
                    }
                }

                processing = false;
            }
        }
    }
}
