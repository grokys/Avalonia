namespace Avalonia.Controls
{
    using Avalonia.Media;

    public class TextBlock : FrameworkElement
    {
        public TextBlock()
        {
            this.FontSize = 12;
        }

        public double FontSize { get; set; }
        public string Text { get; set; }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawText(
                new FormattedText { Text = this.Text },
                new Point());
        }
    }
}
