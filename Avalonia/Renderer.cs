// -----------------------------------------------------------------------
// <copyright file="Renderer.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using Avalonia.Media;

    internal class Renderer
    {
        public void Render(DrawingContext drawingContext, DependencyObject o)
        {
            Visual visual = o as Visual;
            UIElement uiElement = o as UIElement;
            int popCount = 0;

            if (uiElement != null)
            {
                TranslateTransform translate = new TranslateTransform(uiElement.VisualOffset);
                drawingContext.PushTransform(translate);
                ++popCount;

                if (uiElement.Opacity != 1)
                {
                    drawingContext.PushOpacity(uiElement.Opacity);
                    ++popCount;
                }

                uiElement.OnRender(drawingContext);
            }

            if (visual != null)
            {
                for (int i = 0; i < visual.VisualChildrenCount; ++i)
                {
                    this.Render(drawingContext, visual.GetVisualChild(i));
                }
            }

            for (int i = 0; i < popCount; ++i)
            {
                drawingContext.Pop();
            }
        }
    }
}
