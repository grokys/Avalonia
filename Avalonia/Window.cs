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

    public class Window : ContentControl
    {
        private bool isShown;

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            this.PresentationSource = PlatformFactory.Instance.CreatePresentationSource();
            this.PresentationSource.Closed += (s, e) => this.OnClosed(EventArgs.Empty);
            this.PresentationSource.MouseLeftButtonDown += (s, e) => this.OnMouseLeftButtonDown(e);
            this.PresentationSource.Resized += (s, e) => this.DoMeasureArrange();

            this.Background = new SolidColorBrush(Colors.White);
        }

        public event EventHandler Closed;

        public double Width
        {
            get
            {
                return this.PresentationSource.BoundingRect.Width;
            }

            set
            {
                Rect rect = this.PresentationSource.BoundingRect;
                rect.Width = value;
                this.PresentationSource.BoundingRect = rect;
            }
        }

        public double Height
        {
            get
            {
                return this.PresentationSource.BoundingRect.Height;
            }

            set
            {
                Rect rect = this.PresentationSource.BoundingRect;
                rect.Height = value;
                this.PresentationSource.BoundingRect = rect;
            }
        }

        internal PlatformPresentationSource PresentationSource
        {
            get;
            private set;
        }

        public void Show()
        {
            this.PresentationSource.Show();
            this.isShown = true;
            this.DoMeasureArrange();
        }

        public void DoMeasureArrange()
        {
            if (this.isShown)
            {
                Size clientSize = this.PresentationSource.ClientSize;
                this.Measure(clientSize);
                this.Arrange(new Rect(new Point(), clientSize));
                this.DoRender();
            }
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
        }

        protected virtual void OnClosed(EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, e);
            }
        }

        private void DoRender()
        {
            using (DrawingContext drawingContext = this.PresentationSource.CreateDrawingContext())
            {
                this.DoRender(drawingContext, this);
            }
        }

        private void DoRender(DrawingContext drawingContext, DependencyObject o)
        {
            Visual visual = o as Visual;
            UIElement uiElement = o as UIElement;
            bool performPop = false;

            if (uiElement != null)
            {
                TranslateTransform translate = new TranslateTransform(uiElement.VisualOffset);
                drawingContext.PushTransform(translate);
                performPop = true;
                uiElement.OnRender(drawingContext);
            }

            if (visual != null)
            {
                for (int i = 0; i < visual.VisualChildrenCount; ++i)
                {
                    this.DoRender(drawingContext, visual.GetVisualChild(i));
                }
            }

            if (performPop)
            {
                drawingContext.Pop();
            }
        }
    }
}
