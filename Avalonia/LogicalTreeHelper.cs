namespace Avalonia
{
    using System;
    using System.Collections;
    using System.Linq;

    public static class LogicalTreeHelper
    {
        public static DependencyObject FindLogicalNode(DependencyObject logicalTreeNode, string elementName)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable GetChildren(DependencyObject current)
        {
            FrameworkElement fe = current as FrameworkElement;
            
            if (fe == null)
            {
                throw new InvalidOperationException("Object is not a FrameworkElement.");
            }

            return GetChildren(fe);
        }

        public static IEnumerable GetChildren(FrameworkElement current)
        {
            IEnumerator i = current.LogicalChildren;

            while (i.MoveNext())
            {
                yield return i.Current;
            }
        }

        public static DependencyObject GetParent(DependencyObject current)
        {
            FrameworkElement fe = current as FrameworkElement;
            return (fe != null) ? fe.Parent : null;
        }
    }
}
