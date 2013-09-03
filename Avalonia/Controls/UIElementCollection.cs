// -----------------------------------------------------------------------
// <copyright file="UIElementCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class UIElementCollection : AvaloniaCollection<UIElement>
    {
        private UIElement visualParent;
        
        private FrameworkElement logicalParent;

        public UIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
        {
            this.visualParent = visualParent;
            this.logicalParent = logicalParent;
        }

        protected void ClearLogicalParent(UIElement element)
        {
            if (this.logicalParent != null)
            {
                this.logicalParent.RemoveLogicalChild(element);
            }

            if (this.visualParent != null)
            {
                this.visualParent.RemoveVisualChild(element);
            }
        }

        protected void SetLogicalParent(UIElement element)
        {
            if (this.logicalParent != null)
            {
                this.logicalParent.AddLogicalChild(element);
            }

            if (this.visualParent != null)
            {
                this.logicalParent.AddVisualChild(element);
            }
        }

        protected override void OnClearing()
        {
            foreach (UIElement item in this)
            {
                this.ClearLogicalParent(item);
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (UIElement element in e.NewItems)
                    {
                        this.SetLogicalParent(element);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (UIElement element in e.OldItems)
                    {
                        this.ClearLogicalParent(element);
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (UIElement element in e.OldItems)
                    {
                        this.ClearLogicalParent(element);
                    }

                    foreach (UIElement element in e.NewItems)
                    {
                        this.SetLogicalParent(element);
                    }

                    break;
            }

            base.OnItemsChanged(e);
        }
    }
}
