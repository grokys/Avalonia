// -----------------------------------------------------------------------
// <copyright file="PopupRoot.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Media;
    using Avalonia.Platform;

    public class PopupRoot : Decorator, ITopLevelWindow
    {
        public PopupRoot(Point location, UIElement child)
        {
            this.PresentationSource = PlatformInterface.Instance.CreatePopupPresentationSource();
            this.PresentationSource.RootVisual = this;
            this.Child = child;

            Size clientSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            child.Measure(clientSize);
            child.Arrange(new Rect(child.DesiredSize));

            this.PresentationSource.BoundingRect = new Rect(location, child.RenderSize);

            this.PresentationSource.Show();
            child.InvalidateVisual();
        }

        public PlatformPresentationSource PresentationSource
        {
            get;
            private set;
        }
        
        public void Show()
        {
            this.PresentationSource.Show();
            this.InvalidateMeasure();
        }

        public void Hide()
        {
            this.PresentationSource.Hide();
        }

        public void DoLayoutPass()
        {
            Size clientSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            this.Child.Measure(clientSize);
            this.Child.Arrange(new Rect(this.Child.DesiredSize));

            using (DrawingContext drawingContext = this.PresentationSource.CreateDrawingContext())
            {
                Renderer renderer = new Renderer();
                renderer.Render(drawingContext, this.Child);
            }
        }
    }
}
