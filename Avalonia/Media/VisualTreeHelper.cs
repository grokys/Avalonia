namespace Avalonia.Media
{
    using System;

    public static class VisualTreeHelper
    {
        public static DependencyObject GetChild(DependencyObject reference, int childIndex)
        {
            return GetVisual(reference).GetVisualChild(childIndex);
        }

        public static int GetChildrenCount(DependencyObject reference)
        {
            return GetVisual(reference).VisualChildrenCount;
        }

        public static Vector GetOffset(Visual reference)
        {
            return GetVisual(reference).VisualOffset;
        }

        public static DependencyObject GetParent(DependencyObject reference)
        {
            return GetVisual(reference).VisualParent;
        }

        public static Transform GetTransform(Visual reference)
        {
            return GetVisual(reference).VisualTransform;
        }

        private static Visual GetVisual(DependencyObject reference)
        {
            Visual visual = reference as Visual;

            if (visual == null)
            {
                throw new InvalidOperationException("Object is not a Visual.");
            }

            return visual;
        }
    }
}
