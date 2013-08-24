// -----------------------------------------------------------------------
// <copyright file="ButtonBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using Avalonia.Input;

    public class ButtonBase : ContentControl
    {
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register(
                "IsPressed",
                typeof(bool),
                typeof(ButtonBase));

        public bool IsPressed 
        {
            get { return (bool)this.GetValue(IsPressedProperty); }
            protected set { this.SetValue(IsPressedProperty, value); }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.IsPressed = true;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.IsPressed = false;
            e.Handled = true;
        }
    }
}
