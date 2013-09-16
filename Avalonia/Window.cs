// -----------------------------------------------------------------------
// <copyright file="Window.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Controls;
    using Avalonia.Data;
    using Avalonia.Input;
    using Avalonia.Media;
    using Avalonia.Platform;

    public class Window : ContentControl, ITopLevelWindow
    {
        private bool isShown;

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
            WidthProperty.OverrideMetadata(typeof(Window), new PropertyMetadata(WidthChanged));
            HeightProperty.OverrideMetadata(typeof(Window), new PropertyMetadata(HeightChanged));
        }

        public Window()
        {
            this.PresentationSource = PlatformInterface.Instance.CreatePresentationSource();
            this.PresentationSource.RootVisual = this;
            this.PresentationSource.Closed += (s, e) => this.OnClosed(EventArgs.Empty);
            this.PresentationSource.Resized += (s, e) => ((ITopLevelWindow)this).DoLayoutPass();

            this.Background = new SolidColorBrush(Colors.White);
            this.Width = this.PresentationSource.BoundingRect.Width;
            this.Height = this.PresentationSource.BoundingRect.Height;
        }

        public event EventHandler Closed;

        public double Left
        {
            get { return this.PresentationSource.BoundingRect.Left; }
        }

        public double Top
        {
            get { return this.PresentationSource.BoundingRect.Top; }
        }

        public PlatformPresentationSource PresentationSource
        {
            get;
            private set;
        }

        public void Show()
        {
            this.PresentationSource.Show();
            this.isShown = true;
            ((ITopLevelWindow)this).DoLayoutPass();
        }

        void ITopLevelWindow.DoLayoutPass()
        {
            if (this.isShown)
            {
                Size clientSize = this.PresentationSource.ClientSize;
                this.Measure(clientSize);
                this.Arrange(new Rect(clientSize));

                using (DrawingContext drawingContext = this.PresentationSource.CreateDrawingContext())
                {
                    Renderer renderer = new Renderer();
                    renderer.Render(drawingContext, this);
                }
            }
        }

        protected virtual void OnClosed(EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, e);
            }
        }

        private static void WidthChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Window window = (Window)sender;
            Rect rect = window.PresentationSource.BoundingRect;
            rect.Width = (double)e.NewValue;
            window.PresentationSource.BoundingRect = rect;
        }

        private static void HeightChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Window window = (Window)sender;
            Rect rect = window.PresentationSource.BoundingRect;
            rect.Height = (double)e.NewValue;
            window.PresentationSource.BoundingRect = rect;
        }
    }
}
