// -----------------------------------------------------------------------
// <copyright file="TextCompositionManager.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Input
{
    public static class TextCompositionManager
    {
        public static readonly RoutedEvent TextInputEvent =
            EventManager.RegisterRoutedEvent(
                "TextInput",
                RoutingStrategy.Bubble,
                typeof(TextCompositionEventHandler),
                typeof(TextCompositionManager));

        static TextCompositionManager()
        {
            InputManager.Current.PreProcessInput += PreProcessKeyboardInput;
        }

        static void PreProcessKeyboardInput(object sender, PreProcessInputEventArgs e)
        {
            if (e.Input.Device == Keyboard.PrimaryDevice)
            {
                KeyEventArgs keyEventArgs = e.Input as KeyEventArgs;

                if (keyEventArgs != null)
                {
                    TextComposition composition = TextComposition.FromKey(
                        InputManager.Current, 
                        keyEventArgs.Device.Target,
                        keyEventArgs.Key);

                    TextCompositionEventArgs ev = new TextCompositionEventArgs(
                        keyEventArgs.Device,
                        composition);
                    ev.RoutedEvent = TextInputEvent;

                    InputManager.Current.ProcessInput(ev);
                }
            }
        }
    }
}
