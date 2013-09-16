// -----------------------------------------------------------------------
// <copyright file="VisualTreeHelper.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;

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

        [AvaloniaSpecific]
        public static IEnumerable<DependencyObject> GetChildren(DependencyObject reference)
        {
            int count = GetChildrenCount(reference);

            for (int i = 0; i < count; ++i)
            {
                yield return GetChild(reference, i);
            }
        }

        public static Vector GetOffset(Visual reference)
        {
            return GetVisual(reference).VisualOffset;
        }

        public static DependencyObject GetParent(DependencyObject reference)
        {
            return GetVisual(reference).VisualParent;
        }

        [AvaloniaSpecific]
        public static T GetAncestor<T>(DependencyObject target) where T : DependencyObject
        {
            DependencyObject o = GetParent(target);

            while (o != null)
            {
                if (o is T)
                {
                    return (T)o;
                }
                else
                {
                    o = GetParent(o);
                }
            }

            return null;
        }

        [AvaloniaSpecific]
        public static IEnumerable<DependencyObject> GetAncestors(DependencyObject dependencyObject)
        {
            dependencyObject = GetParent(dependencyObject);

            while (dependencyObject != null)
            {
                yield return dependencyObject;
                dependencyObject = GetParent(dependencyObject);
            }
        }

        [AvaloniaSpecific]
        public static IEnumerable<T> GetDescendents<T>(DependencyObject reference) where T : DependencyObject
        {
            foreach (DependencyObject child in GetChildren(reference))
            {
                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (T descendent in GetDescendents<T>(child))
                {
                    yield return descendent;
                }
            }
        }

        public static Transform GetTransform(Visual reference)
        {
            return GetVisual(reference).VisualTransform;
        }

        internal static ITopLevelWindow GetTopLevelWindow(DependencyObject target)
        {
            DependencyObject o = VisualTreeHelper.GetParent(target);

            while (o != null)
            {
                if (o is ITopLevelWindow)
                {
                    return (ITopLevelWindow)o;
                }
                else
                {
                    o = VisualTreeHelper.GetParent(o);
                }
            }

            return null;
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
