// -----------------------------------------------------------------------
// <copyright file="ICollectionView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections;
    using System.Collections.Specialized;

    public interface ICollectionView : IEnumerable, INotifyCollectionChanged
    {
    }
}
