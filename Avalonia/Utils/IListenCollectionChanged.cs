// -----------------------------------------------------------------------
// <copyright file="IListenCollectionChanged.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System.Collections.Specialized;

    internal interface IListenCollectionChanged
    {
        void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
    }
}
