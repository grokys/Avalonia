namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia.Controls;
    using Avalonia.Data;
    using Avalonia.Input;
    using Avalonia.Media;

    public class Window : ContentControl
    {
        private AvaloniaPresentationSource presentationSource;
        private bool isShown;

        public Window()
        {
            this.presentationSource = (AvaloniaPresentationSource)
                Activator.CreateInstance(Application.Current.PresentationSourceType);
            
            this.presentationSource.Closed += (s, e) => this.OnClosed(EventArgs.Empty);
            this.presentationSource.MouseLeftButtonDown += (s, e) => this.OnMouseLeftButtonDown(e);
            this.presentationSource.Resized += (s, e) => this.DoMeasureArrange();

            this.Background = new SolidColorBrush(Colors.White);
            this.Template = new WindowTemplate();
        }

        public double Width
        {
            get 
            { 
                return this.presentationSource.BoundingRect.Width; 
            }

            set 
            {
                Rect rect = this.presentationSource.BoundingRect;
                rect.Width = value;
                this.presentationSource.BoundingRect = rect;
            }
        }

        public double Height
        {
            get 
            { 
                return this.presentationSource.BoundingRect.Height; 
            }

            set 
            {
                Rect rect = this.presentationSource.BoundingRect;
                rect.Height = value;
                this.presentationSource.BoundingRect = rect;
            }
        }

        public event EventHandler Closed;

        public void Show()
        {
            this.presentationSource.Show();
            this.isShown = true;
            this.DoMeasureArrange();
        }

        protected virtual void OnClosed(EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, e);
            }
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
        }

        // HACK to work around the fact we don't have xaml support.
        private class WindowTemplate : ControlTemplate
        {
            public override Visual CreateVisualTree(DependencyObject parent)
            {
                Window window = parent as Window;

                Border border = new Border
                {
                    TemplatedParent = parent,
                    Child = new ContentPresenter(window)
                };

                Binding binding = new Binding("Background");
                binding.Source = parent;
                BindingOperations.SetBinding(border, Border.BackgroundProperty, binding);

                return border;
            }
        }

        public void DoMeasureArrange()
        {
            if (this.isShown)
            {
                Size clientSize = this.presentationSource.ClientSize;
                this.Measure(clientSize);
                this.Arrange(new Rect(new Point(), clientSize));
                this.DoRender();
            }
        }

        private void DoRender()
        {
            using (DrawingContext drawingContext = this.presentationSource.CreateDrawingContext())
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
                if (uiElement.VisualTransform != null)
                {
                    drawingContext.PushTransform(uiElement.VisualTransform);
                    performPop = true;
                }

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
