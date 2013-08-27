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

        private static void PreProcessKeyboardInput(object sender, PreProcessInputEventArgs e)
        {
            if (e.Input.Device == Keyboard.PrimaryDevice)
            {
                KeyEventArgs keyEventArgs = e.Input as KeyEventArgs;

                if (keyEventArgs != null)
                {
                    string text = keyEventArgs.KeyboardDevice.KeyToString(keyEventArgs.Key);

                    if (text != string.Empty && !char.IsControl(text[0]))
                    {
                        TextComposition composition = new TextComposition(
                            InputManager.Current,
                            keyEventArgs.Device.Target,
                            text);

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
}
