// -----------------------------------------------------------------------
// <copyright file="EnumerableCollectionView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections;

    public class EnumerableCollectionView : CollectionView
    {
        public EnumerableCollectionView(IEnumerable collection)
            : base(collection)
        {
        }
    }
}
