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

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ButtonBase));

        public ButtonBase()
        {
        }

        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickEvent, value); }
            remove { this.RemoveHandler(ClickEvent, value); }
        }

        public bool IsPressed
        {
            get { return (bool)this.GetValue(IsPressedProperty); }
            protected set { this.SetValue(IsPressedProperty, value); }
        }

        protected virtual void OnClick()
        {
            this.RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.IsPressed = true;
            e.Handled = true;
            this.CaptureMouse();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.ReleaseMouseCapture();

            if (this.IsPressed)
            {
                this.IsPressed = false;
                this.OnClick();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.IsPressed = this.WithinBounds(e.GetPosition(this));
            }
        }

        private bool WithinBounds(Point p)
        {
            return p.X > 0 && p.X < this.ActualWidth && p.Y > 0 && p.Y < this.ActualHeight;
        }
    }
}
