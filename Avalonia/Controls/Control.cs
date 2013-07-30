namespace Avalonia.Controls
{
    using System.Collections.Generic;
    using Avalonia.Media;

    public class Control : FrameworkElement
    {
        private List<Visual> visualChildren;

        public Control()
        {
            this.visualChildren = new List<Visual>();
            this.Background = new SolidColorBrush(Colors.White);
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(Control));

        public Brush Background 
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public ControlTemplate Template { get; set; }

        public override bool ApplyTemplate()
        {
            if (this.visualChildren.Count == 0)
            {
                this.visualChildren.Add(this.Template.CreateVisualTree(this));
                return true;
            }
            else
            {
                return false;
            }
        }

        protected internal override int VisualChildrenCount
        {
            get { return this.visualChildren.Count; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }
    }
}
