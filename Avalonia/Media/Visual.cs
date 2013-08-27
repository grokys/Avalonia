// -----------------------------------------------------------------------
// <copyright file="Visual.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for objects in the visual tree.
    /// </summary>
    public class Visual : DependencyObject
    {
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
            child.VisualParent = this;
            child.OnVisualParentChanged(oldParent);
        }

        /// <summary>
        /// Called when the visual parent changes.
        /// </summary>
        /// <param name="oldParent">The old visual parent.</param>
        protected internal virtual void OnVisualParentChanged(DependencyObject oldParent)
        {
        }

        /// <summary>
        /// Removes a child <see cref="Visual"/> from the visual tree.
        /// </summary>
        /// <param name="child">The child visual.</param>
        protected internal void RemoveVisualChild(Visual child)
        {
            child.VisualParent = this;
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
