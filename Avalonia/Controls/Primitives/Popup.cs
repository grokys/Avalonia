// -----------------------------------------------------------------------
// <copyright file="Popup.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System.Windows.Markup;
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

        private static void IsOpenChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Popup popup = (Popup)sender;

            if (popup.IsOpen && popup.popupRoot == null && popup.Child != null)
            {
                popup.popupRoot = new PopupRoot(popup.GetLocation(), popup.Child);
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
