// -----------------------------------------------------------------------
// <copyright file="Visual.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;

    /// <summary>
    /// Base class for objects in the visual tree.
    /// </summary>
    public class Visual : DependencyObject
    {
        internal event EventHandler VisualParentChanged;

        /// <summary>
        /// Gets the number of child controls in the visual tree.
        /// </summary>
        protected internal virtual int VisualChildrenCount
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the position of the visual within it's parent visual.
        /// </summary>
        protected internal Vector VisualOffset
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the parent control in the visual tree.
        /// </summary>
        protected internal DependencyObject VisualParent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a transform that can be applied to the visual.
        /// </summary>
        protected internal Transform VisualTransform
        {
            get;
            protected set;
        }

        /// <summary>
        /// Converts a point in this Visual to screen coordinates.
        /// </summary>
        /// <param name="point">The client coordinate.</param>
        /// <returns>The screen coordinate.</returns>
        public Point PointToScreen(Point point)
        {
            foreach (Visual v in VisualTreeHelper.GetAncestors(this))
            {
                point += v.VisualOffset;

                ITopLevelWindow window = v as ITopLevelWindow;

                if (window != null)
                {
                    point += (Vector)window.PresentationSource.PointToScreen(new Point(0, 0));
                }
            }

            return point;
        }

        /// <summary>
        /// When overridden in a derived class, returns the bounds of the control for use in hit testing.
        /// </summary>
        /// <returns>The hit test bounds.</returns>
        internal virtual Rect GetHitTestBounds()
        {
            return new Rect();
        }

        /// <summary>
        /// Gets the child in the visual tree with the requested index.
        /// </summary>
        /// <param name="index">The index. Should be from 0 to <see cref="VisualChildrenCount"/>.</param>
        /// <returns>The child visual.</returns>
        protected internal virtual Visual GetVisualChild(int index)
        {
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Adds a child <see cref="Visual"/> to the visual tree.
        /// </summary>
        /// <param name="child">The child visual.</param>
        protected internal void AddVisualChild(Visual child)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child");
            }

            DependencyObject oldParent = child.VisualParent;
            child.DependencyParent = this;
            child.VisualParent = this;
            child.OnVisualParentChanged(oldParent);
        }

        /// <summary>
        /// Called when the visual parent changes.
        /// </summary>
        /// <param name="oldParent">The old visual parent.</param>
        protected internal virtual void OnVisualParentChanged(DependencyObject oldParent)
        {
            if (this.VisualParentChanged != null)
            {
                this.VisualParentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes a child <see cref="Visual"/> from the visual tree.
        /// </summary>
        /// <param name="child">The child visual.</param>
        protected internal void RemoveVisualChild(Visual child)
        {
            child.VisualParent = null;
            child.DependencyParent = null;
        }

        /// <summary>
        /// Determines whether a point is within the bounds of the visual.
        /// </summary>
        /// <param name="hitTestParameters">The point to test.</param>
        /// <returns>
        /// A <see cref="PointHitTestResult"/> if the point was within the bounds; otherwise null.
        /// </returns>
        protected virtual HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return this.GetHitTestBounds().Contains(hitTestParameters.HitPoint) ?
                new PointHitTestResult(this, hitTestParameters.HitPoint) : null;
        }
    }
}
