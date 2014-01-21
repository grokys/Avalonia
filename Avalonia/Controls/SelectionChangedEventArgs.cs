// -----------------------------------------------------------------------
// <copyright file="SelectionChangedEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;

    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

    public class SelectionChangedEventArgs : RoutedEventArgs
    {
        public SelectionChangedEventArgs(RoutedEvent id, IList removedItems, IList addedItems)
            : base(id)
        {
            this.AddedItems = addedItems;
            this.RemovedItems = removedItems;
        }

        public IList AddedItems { get; private set; }

        public IList RemovedItems { get; private set; }
    }
}
