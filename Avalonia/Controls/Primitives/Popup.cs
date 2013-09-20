// -----------------------------------------------------------------------
// <copyright file="Popup.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System.Windows.Markup;
    using Avalonia.Input;
    using Avalonia.Media;
    using Avalonia.Platform;

    [ContentProperty("Child")]
    public class Popup : FrameworkElement
    {
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register(
                "Child",
                typeof(UIElement),
                typeof(Popup));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                "IsOpen",
                typeof(bool),
                typeof(Popup),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    IsOpenChanged));

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register(
                "StaysOpen",
                typeof(bool),
                typeof(Popup));

        private PopupRoot popupRoot;

        public UIElement Child
        {
            get { return (UIElement)this.GetValue(ChildProperty); }
            set { this.SetValue(ChildProperty, value); }
        }

        public bool IsOpen
        {
            get { return (bool)this.GetValue(IsOpenProperty); }
            set { this.SetValue(IsOpenProperty, value); }
        }

        public bool StaysOpen
        {
            get { return (bool)this.GetValue(StaysOpenProperty); }
            set { this.SetValue(StaysOpenProperty, value); }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // TODO: This should be listening to the Mouse.MouseDown event for all mouse clicks.
            // TODO: Should not close popup when click is on popup.
            this.IsOpen = false;
        }

        private static void IsOpenChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Popup popup = (Popup)sender;

            if (popup.IsOpen)
            {
                if (popup.popupRoot == null)
                {
                    popup.popupRoot = new PopupRoot(popup.GetLocation(), popup.Child);
                }
                else
                {
                    popup.popupRoot.Show();
                }

                if (!popup.StaysOpen)
                {
                    popup.CaptureMouse();
                }
            }
            else if (popup.popupRoot != null)
            {
                popup.popupRoot.Hide();

                if (popup.IsMouseCaptured)
                {
                    popup.ReleaseMouseCapture();
                }
            }
        }

        private Point GetLocation()
        {
            FrameworkElement parent = this.VisualParent as FrameworkElement;

            if (parent != null)
            {
                return parent.PointToScreen(new Point(0, parent.ActualHeight));
            }
            else
            {
                return new Point();
            }
        }
    }
}
